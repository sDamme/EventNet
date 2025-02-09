namespace EventNet.Application
{
    public sealed record AddEventHandler(
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork)
        : IHandler<AddEventRequest, long>
    {
        public async Task<Result<long>> HandleAsync(AddEventRequest request)
        {
            var entity = new Event(request.Name, request.Time, request.Location, request.Description);

            await eventRepository.AddAsync(entity);

            await unitOfWork.SaveChangesAsync();

            return new Result<long>(Created, entity.Id);
        }
    }
}
