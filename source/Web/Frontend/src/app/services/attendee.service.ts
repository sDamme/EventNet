import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AppAttendee } from 'src/app/models/attendee';

@Injectable({
    providedIn: 'root'
})
export class AppAttendeeService {
    constructor(private readonly http: HttpClient) { }

    /**
     * Adds an attendee to a specific event.
     * @param eventId - The ID of the event.
     * @param attendee - The attendee data.
     * @returns The ID of the newly created attendee.
     */
    add(eventId: number, attendee: AppAttendee): Observable<number> {
        return this.http.post<number>(`api/events/${eventId}/attendees`, attendee);
    }

    /**
     * Retrieves an attendee from a specific event.
     * @param eventId - The ID of the event.
     * @param attendeeId - The ID of the attendee.
     * @returns The attendee details.
     */
    get(eventId: number, attendeeId: number): Observable<AppAttendee> {
        return this.http.get<AppAttendee>(`api/events/${eventId}/attendees/${attendeeId}`);
    }

    /**
     * Updates an attendee for a specific event.
     * @param eventId - The ID of the event.
     * @param attendeeId - The ID of the attendee.
     * @param attendee - The updated attendee data.
     * @returns An observable for the update operation.
     */
    update(eventId: number, attendeeId: number, attendee: AppAttendee): Observable<any> {
        return this.http.put(`api/events/${eventId}/attendees/${attendeeId}`, attendee);
    }

    /**
     * Deletes an attendee from a specific event.
     * @param eventId - The ID of the event.
     * @param attendeeId - The ID of the attendee.
     * @returns An observable for the delete operation.
     */
    delete(eventId: number, attendeeId: number): Observable<any> {
        return this.http.delete(`api/events/${eventId}/attendees/${attendeeId}`);
    }
}
