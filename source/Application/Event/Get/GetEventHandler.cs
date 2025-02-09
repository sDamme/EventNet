namespace EventNet.Application
{
    public sealed record GetEventHandler(
        IEventRepository EventRepository, 
        IUnitOfWork unitOfWork)
        : IHandler<GetEventRequest, EventModel>
    {
        public async Task<Result<EventModel>> HandleAsync(GetEventRequest request)
        {
            var model = await EventRepository.GetModelAsync(request.Id);

            return new Result<EventModel>(model is null ? NotFound : OK, model);
        }
    }
}
