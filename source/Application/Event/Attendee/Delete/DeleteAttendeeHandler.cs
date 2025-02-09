namespace EventNet.Application
{
    public sealed record DeleteAttendeeHandler(
        IAttendeeRepository AttendeeRepository,
        IUnitOfWork UnitOfWork
    ) : IHandler<DeleteAttendeeRequest>
    {
        public async Task<Result> HandleAsync(DeleteAttendeeRequest request)
        {
            await AttendeeRepository.DeleteAsync(request.Id);

            await UnitOfWork.SaveChangesAsync();

            return new Result(NoContent);
        }
    }
}