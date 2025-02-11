import { AppAttendee } from "./attendee";

export default interface AppEvent {
    id: number;
    name: string;
    eventDate: Date;
    location: string;
    description: string;
    attendeeCount: number;
    attendees: AppAttendee[];
}