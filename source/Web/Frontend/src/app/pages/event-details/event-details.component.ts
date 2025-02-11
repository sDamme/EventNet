import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppEventService } from 'src/app/services/event.service';
import { AppAttendeeService } from 'src/app/services/attendee.service';
import AppEvent from 'src/app/models/event';
import { AppAttendee, IndividualAttendee, BusinessAttendee, AttendeeType } from 'src/app/models/attendee';
import IndividualAttendeeFormComponent from 'src/app/components/individual-attendee-form/individual-attendee-form.component';
import BusinessAttendeeFormComponent from 'src/app/components/business-attendee-form/business-attendee-form.component';

@Component({
    selector: 'app-event-details',
    templateUrl: './event-details.component.html',
    standalone: true,
    imports: [
        CommonModule,
        DatePipe,
        RouterModule,
        FormsModule,
        IndividualAttendeeFormComponent,
        BusinessAttendeeFormComponent
    ]
})
export default class AppEventDetailsComponent implements OnInit {
    event!: AppEvent;
    attendeeType: string = 'Individual';
    title: string = 'Osavõtjad';
    attendeeTypes = AttendeeType;

    constructor(
        private route: ActivatedRoute,
        private eventService: AppEventService,
        private attendeeService: AppAttendeeService
    ) { }

    ngOnInit(): void {
        const eventId = +this.route.snapshot.params['id'];
        this.eventService.get(eventId).subscribe(ev => {
            this.event = ev;
        });
    }

    deleteGuest(guestId: number): void {
        this.attendeeService.delete(this.event.id, guestId).subscribe(() => {
            this.event.attendees = this.event.attendees.filter(g => g.id !== guestId);
        });
    }

    toggleAttendeeForm(): void {}

    isIndividual(guest: AppAttendee): guest is IndividualAttendee {
        return guest.attendeeType === AttendeeType.Individual;
    }

    isBusiness(guest: AppAttendee): guest is BusinessAttendee {
        return guest.attendeeType === AttendeeType.Business;
    }

}
