

namespace EventNet.Infrastructure
{
    internal interface IAttendeeRepository : IRepository<Attendee>
    {
        Task<AttendeeModel> GetModelAsync(long id);
        Task<IEnumerable<AttendeeModel>> ListModelAsync();
    }
}
