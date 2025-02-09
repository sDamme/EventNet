#nullable enable
namespace EventNet.Application
{
    public sealed record UpdateAttendeeRequest(
        long AttendeeId,
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