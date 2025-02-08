namespace EventNet.Model
{
    public abstract record AttendeeModel
    {
        public long Id { get; init; }
        public PaymentType PaymentType { get; init; }
        public string Description { get; init; }
    }

    public sealed record InidividualAttendeeModel : AttendeeModel
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string PersonalIdCode { get; init; }
    }

    public sealed record BusinessAttendeeModel : AttendeeModel
    {
        public string LegalName { get; init; }
        public string RegistrationCode { get; init; }
        public int NumberOfAttendees { get; init; }
    }
}
