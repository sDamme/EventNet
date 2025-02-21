import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AppAttendeeService } from 'src/app/services/attendee.service';
import { AppAttendee, AttendeeType } from '../../models/attendee';
import IndividualAttendeeFormComponent from 'src/app/components/individual-attendee-form/individual-attendee-form.component';
import BusinessAttendeeFormComponent from 'src/app/components/business-attendee-form/business-attendee-form.component';

@Component({
    selector: 'app-edit-attendee',
    templateUrl: './edit-attendee.component.html',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        IndividualAttendeeFormComponent,
        BusinessAttendeeFormComponent
    ]
})
export default class AppEditAttendeeComponent implements OnInit {
    eventId!: number;
    attendeeId!: number;
    attendee!: AppAttendee;
    AttendeeType = AttendeeType;
    title: string = 'Osavõtja info';

    constructor(private route: ActivatedRoute, private attendeeService: AppAttendeeService) { }

    ngOnInit(): void {
        this.eventId = +this.route.snapshot.paramMap.get('eventId')!;
        this.attendeeId = +this.route.snapshot.paramMap.get('attendeeId')!;

        this.attendeeService.get(this.eventId, this.attendeeId).subscribe(attendee => {
            this.attendee = attendee;
        });
    }
}
