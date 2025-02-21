namespace EventNet.Application
{
    public sealed record DeleteEventHandler(
        IEventRepository eventRepository,
        IUnitOfWork unitOfWork)
        : IHandler<DeleteEventRequest>
    {
        public async Task<Result> HandleAsync(DeleteEventRequest request)
        {
            await eventRepository.DeleteAsync(request.Id);

            await unitOfWork.SaveChangesAsync();

            return new Result(NoContent);
        }
    }
}
