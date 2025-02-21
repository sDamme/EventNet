import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AppAttendee, IndividualAttendee } from '../../models/attendee';
import { AppAttendeeService } from '../../services/attendee.service';
import { personalIdCodeChecksumValidator } from './personal-id-code.validator';
@Component({
    selector: 'app-individual-attendee-form',
    templateUrl: './individual-attendee-form.component.html',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule]
})
export default class IndividualAttendeeFormComponent implements OnInit {
    @Input() eventId!: number;
    @Input() attendee?: AppAttendee; // Optional input for existing attendee data
    individualForm!: FormGroup;

    constructor(private fb: FormBuilder, private attendeeService: AppAttendeeService, public router: Router) { }

    ngOnInit(): void {

        const individualAttendee = this.attendee as IndividualAttendee;

        this.individualForm = this.fb.group({
            id: [this.attendee ? individualAttendee.id : 0],
            eventId: [this.eventId, Validators.required],
            firstName: [this.attendee ? individualAttendee.firstName : '', Validators.required],
            lastName: [this.attendee ? individualAttendee.lastName : '', Validators.required],
            personalIdCode: [
                this.attendee ? individualAttendee.personalIdCode : '',
                [
                    Validators.required,
                    Validators.minLength(11),
                    Validators.maxLength(11),
                    Validators.pattern(/^\d+$/),
                    personalIdCodeChecksumValidator
                ]
            ],
            paymentType: [this.attendee ? individualAttendee.paymentType : 'Cash', Validators.required],
            description: [this.attendee ? individualAttendee.description : '', Validators.maxLength(1500)]
        });
    }

    onSubmit(): void {
        if (this.individualForm.valid) {
            const formData = this.individualForm.value;
            if (formData.id) {
                // Update existing attendee
                this.attendeeService.update(this.eventId, formData.id, formData).subscribe({
                    next: () => {
                        window.location.reload();
                    },
                    error: (err) => console.error('Error updating attendee:', err)
                });
            } else {
                // Add new attendee
                this.attendeeService.add(this.eventId, formData).subscribe({
                    next: () => {
                        window.location.reload();
                    },
                    error: (err) => console.error('Error adding attendee:', err)
                });
            }
        }
    }
}
