import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AppAttendee, BusinessAttendee } from '../../models/attendee';
import { AppAttendeeService } from '../../services/attendee.service';

@Component({
    selector: 'app-business-attendee-form',
    templateUrl: './business-attendee-form.component.html',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule]
})
export default class BusinessAttendeeFormComponent implements OnInit {
    @Input() eventId!: number;
    @Input() attendee!: AppAttendee; // Optional input for existing attendee data
    businessForm!: FormGroup;

    constructor(private fb: FormBuilder, private attendeeService: AppAttendeeService, public router: Router) { }

    ngOnInit(): void {

        const businessAttendee = this.attendee as BusinessAttendee;

        this.businessForm = this.fb.group({
            id: [this.attendee ? businessAttendee.id : 0],
            eventId: [this.eventId, Validators.required],
            legalName: [this.attendee ? businessAttendee.legalName : '', Validators.required],
            registrationCode: [this.attendee ? businessAttendee.registrationCode : '', Validators.required],
            numberOfAttendees: [this.attendee ? businessAttendee.numberOfAttendees : 1, Validators.required],
            paymentType: [this.attendee ? businessAttendee.paymentType : 'Cash', Validators.required],
            description: [this.attendee ? businessAttendee.description : '', Validators.maxLength(5000)]
        });
    }

    onSubmit(): void {
        if (this.businessForm.valid) {
            const formData = this.businessForm.value;
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