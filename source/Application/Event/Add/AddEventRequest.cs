namespace EventNet.Application
{
    public sealed record AddEventRequest(string Name, DateTime EventDate, string Location, string Description);
}
