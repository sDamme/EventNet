import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { AppEventService } from "../../services/event.service";
import AppEvent from "../../models/event";

@Component({
    selector: "app-home",
    templateUrl: "./home.component.html",
    imports: [
        CommonModule
    ]
})
export default class AppHomeComponent implements OnInit {
    events!: AppEvent[];
    upcomingEvents: AppEvent[] = [];
    pastEvents: AppEvent[] = [];
    attendeeType: string = 'Individual';
    title: string = 'Osavõtjad';

    constructor(
        private eventService: AppEventService,

    ) { }

    ngOnInit(): void {
        this.eventService.list().subscribe(ev => {
            this.events = ev;
            const now = new Date();

            this.upcomingEvents = ev.filter(event => new Date(event.eventDate) >= now)
                .sort((a, b) => new Date(a.eventDate).getTime() - new Date(b.eventDate).getTime());

            this.pastEvents = ev.filter(event => new Date(event.eventDate) < now)
                .sort((a, b) => new Date(b.eventDate).getTime() - new Date(a.eventDate).getTime());
        });
    }

    deleteEvent(eventId: number): void {
        this.eventService.delete(eventId).subscribe(() => {
            this.events = this.events.filter(g => g.id !== eventId);
        });
    }
}
