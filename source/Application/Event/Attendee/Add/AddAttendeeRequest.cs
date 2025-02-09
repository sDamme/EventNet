#nullable enable
namespace EventNet.Application
{
    public sealed record AddAttendeeRequest(
        long EventId,
        PaymentType PaymentType,
        string? FirstName,
        string? LastName,
        string? PersonalIdCode,
        string? LegalName,
        string? RegistrationCode,
        int? NumberOfAttendees,
        string? Description
    );
}
