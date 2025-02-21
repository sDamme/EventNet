import { AbstractControl, ValidationErrors } from '@angular/forms';

export function personalIdCodeChecksumValidator(control: AbstractControl): ValidationErrors | null {
    const value: string = control.value;
    if (!value || value.length !== 11) {
        // Let the minLength/maxLength validators handle this
        return null;
    }
    if (!/^\d{11}$/.test(value)) {
        return { personalIdCodeInvalid: 'Isikukood peab koosnema ainult numbritest.' };
    }

    const firstLevelWeights = [1, 2, 3, 4, 5, 6, 7, 8, 9, 1];
    const secondLevelWeights = [3, 4, 5, 6, 7, 8, 9, 1, 2, 3];

    let checksum = 0;
    for (let i = 0; i < 10; i++) {
        checksum += parseInt(value[i], 10) * firstLevelWeights[i];
    }
    let remainder = checksum % 11;
    if (remainder === 10) {
        checksum = 0;
        for (let i = 0; i < 10; i++) {
            checksum += parseInt(value[i], 10) * secondLevelWeights[i];
        }
        remainder = checksum % 11;
        if (remainder === 10) {
            remainder = 0;
        }
    }
    if (remainder !== parseInt(value[10], 10)) {
        return { personalIdCodeInvalid: 'Isikukood ei ole arvutuste kohaselt korrektne.' };
    }
    return null;
}
