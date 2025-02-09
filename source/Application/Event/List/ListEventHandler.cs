namespace EventNet.Application
{
    public sealed record ListEventHandler(
        IEventRepository eventRepository, 
        IUnitOfWork unitOfWork) 
        : IHandler<ListEventRequest, IEnumerable<EventModel>>
    {
        public async Task<Result<IEnumerable<EventModel>>> HandleAsync(ListEventRequest request)
        {
            var list = await eventRepository.ListModelAsync();

            return new Result<IEnumerable<EventModel>>(list is null ? NotFound : OK, list);
        }
    }
}
