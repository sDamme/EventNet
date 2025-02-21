import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AppEventService } from 'src/app/services/event.service';

@Component({
    selector: 'app-add-event',
    templateUrl: './add-event.component.html',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule]
})
export default class AppAddEventComponent {
    eventForm!: FormGroup;
    title: string = "Ürituse lisamine";
    constructor(
        private fb: FormBuilder,
        private eventService: AppEventService,
        public router: Router
    ) { }

    ngOnInit(): void {
        this.eventForm = this.fb.group({
            name: ['', Validators.required],
            eventdate: ['', Validators.required],
            location: ['', Validators.required],
            description: ['']
        });
    }

    onSubmit(): void {
        if (this.eventForm.valid) {
            const newEvent = this.eventForm.value;
            newEvent.eventdate = new Date(newEvent.eventdate);
            this.eventService.add(newEvent).subscribe({
                next: () => this.router.navigate(['/home']),
                error: (error: any) => console.error('Error adding event:', error)
            });
        }
    }
}
