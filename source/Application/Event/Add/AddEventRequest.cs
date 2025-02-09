namespace EventNet.Application
{
    public sealed record AddEventRequest(string Name, DateTime Time, string Location, string Description);
}
