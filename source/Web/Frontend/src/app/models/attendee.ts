export interface AppAttendeeBase {
    id: number;
    paymentType: string;
    description: string;
    attendeeType: AttendeeType;
}

export enum AttendeeType {
    Individual = 1,
    Business = 2
}

export interface IndividualAttendee extends AppAttendeeBase {
    firstName: string;
    lastName: string;
    personalIdCode: string;
}

export interface BusinessAttendee extends AppAttendeeBase {
    legalName: string;
    registrationCode: string;
    numberOfAttendees: number;
}

export type AppAttendee = IndividualAttendee | BusinessAttendee;