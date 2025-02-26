namespace EventNet.Application
{
    public sealed record DeleteAttendeeRequest(long EventId, long AttendeeId);
}
