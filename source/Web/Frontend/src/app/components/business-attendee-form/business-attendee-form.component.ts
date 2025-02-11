import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'app-business-attendee-form',
    templateUrl: './business-attendee-form.component.html',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule]
})
export default class BusinessAttendeeFormComponent implements OnInit {
    @Input() eventId!: number;
    businessForm!: FormGroup;

    constructor(private fb: FormBuilder, public router: Router) { }

    ngOnInit(): void {
        this.businessForm = this.fb.group({
            id: [0],
            eventId: [this.eventId, Validators.required],
            legalName: ['', Validators.required],
            registrationCode: ['', Validators.required],
            numberOfAttendees: [1, [Validators.required, Validators.min(1)]],
            paymentType: ['Cash', Validators.required],
            extraInformation: ['']
        });
    }

    onSubmit(): void {
        if (this.businessForm.valid) {
            const formData = this.businessForm.value;
            console.log('Submitting business attendee:', formData);
            // TODO: Call service to add/update the business attendee.
 
        }
    }
}
