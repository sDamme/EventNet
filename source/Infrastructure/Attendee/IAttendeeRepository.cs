

namespace EventNet.Infrastructure
{
    public interface IAttendeeRepository : IRepository<Attendee>
    {
        Task<AttendeeModel> GetModelAsync(long id);
        Task<IEnumerable<AttendeeModel>> ListModelAsync();
    }
}
