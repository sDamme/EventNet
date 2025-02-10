import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import AppEvent from 'src/app/models/event';

@Injectable({
    providedIn: 'root'
})
export class AppEventService {
    constructor(private readonly http: HttpClient) { }

    /**
     * Creates a new event.
     * @param event - The event data to add.
     * @returns An Observable that emits the ID of the newly created event.
     */
    add(event: AppEvent): Observable<number> {
        return this.http.post<number>('api/events', event);
    }

    /**
     * Deletes an event by its ID.
     * @param id - The ID of the event to delete.
     * @returns An Observable that completes when the deletion is done.
     */
    delete(id: number): Observable<void> {
        return this.http.delete<void>(`api/events/${id}`);
    }

    /**
     * Retrieves an event by its ID.
     * @param id - The ID of the event.
     * @returns An Observable that emits the event details.
     */
    get(id: number): Observable<AppEvent> {
        return this.http.get<AppEvent>(`api/events/${id}`);
    }

    /**
     * Retrieves a list of events.
     * @returns An Observable that emits an array of events.
     */
    list(): Observable<AppEvent[]> {
        return this.http.get<AppEvent[]>('api/events');
    }

    /**
     * Updates an event.
     * @param event - The event data to update.
     * @returns An Observable that completes when the update is done.
     */
    update(event: AppEvent): Observable<void> {
        return this.http.put<void>(`api/events/${event.id}`, event);
    }
}
