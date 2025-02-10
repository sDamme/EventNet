import { AppAttendee } from "./attendee";

export default interface AppEvent {
    id: number;
    name: string;
    eventdate: Date;
    location: string;
    description: string;
    attendeecount: number;
    attendees: AppAttendee[];
}