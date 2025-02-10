export interface AppAttendeeBase {
    id: number;
    paymentType: string;
    description: string;
    type: 'individual' | 'business';
}

export interface IndividualAttendee extends AppAttendeeBase {
    type: 'individual';
    firstName: string;
    lastName: string;
    personalIdCode: string;
}

export interface BusinessAttendee extends AppAttendeeBase {
    type: 'business';
    legalName: string;
    registrationCode: string;
    numberOfAttendees: number;
}

export type AppAttendee = IndividualAttendee | BusinessAttendee;