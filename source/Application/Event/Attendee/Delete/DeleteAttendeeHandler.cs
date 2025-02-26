using EventNet.Infrastructure;

namespace EventNet.Application
{
    public sealed record DeleteAttendeeHandler(
        IAttendeeRepository AttendeeRepository,
        IEventRepository eventRepository,
        IUnitOfWork UnitOfWork
    ) : IHandler<DeleteAttendeeRequest>
    {
        public async Task<Result> HandleAsync(DeleteAttendeeRequest request)
        {
            var eventEntity = await eventRepository.GetAsync(request.EventId);

            if (eventEntity == null)
            {
                return new Result(NotFound);
            }
            eventEntity.RemoveAttendee(request.AttendeeId);

            await eventRepository.UpdateAsync(eventEntity);
            await UnitOfWork.SaveChangesAsync();

            return new Result(NoContent);
        }
    }
}
