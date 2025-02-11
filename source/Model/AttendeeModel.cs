using System.Text.Json.Serialization;

namespace EventNet.Model
{
    [JsonDerivedType(typeof(IndividualAttendeeModel))]
    [JsonDerivedType(typeof(BusinessAttendeeModel))]
    public abstract record AttendeeModel
    {
        public long Id { get; init; }
        public string PaymentType { get; init; }
        public int AttendeeType { get; init; }

    }

    public sealed record IndividualAttendeeModel : AttendeeModel
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string PersonalIdCode { get; init; }
        public string Description { get; init; }
    }

    public sealed record BusinessAttendeeModel : AttendeeModel
    {
        public string LegalName { get; init; }
        public string RegistrationCode { get; init; }
        public int NumberOfAttendees { get; init; }
        public string Description { get; init; }
    }
}
