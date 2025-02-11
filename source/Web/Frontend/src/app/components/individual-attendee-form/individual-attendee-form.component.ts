import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AppAttendeeService } from '../../services/attendee.service';

@Component({
    selector: 'app-individual-attendee-form',
    templateUrl: './individual-attendee-form.component.html',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule]
})
export default class IndividualAttendeeFormComponent implements OnInit {
    @Input() eventId!: number;
    individualForm!: FormGroup;

    constructor(private fb: FormBuilder, public router: Router, private attendeeService: AppAttendeeService) { }

    ngOnInit(): void {
        this.individualForm = this.fb.group({
            id: [0],
            eventId: [this.eventId, Validators.required],
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            personalIdCode: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
            paymentType: ['Cash', Validators.required],
            description: ['']
        });
    }

    onSubmit(): void {
        if (this.individualForm.valid) {
            const formData = this.individualForm.value;
            console.log('Submitting individual attendee:', formData);
            this.attendeeService.add(this.eventId, formData).subscribe({
                next: () => {
                    window.location.reload();
                },
                error: (err) => console.error('Error adding attendee:', err)
            });
        }
    }
}
