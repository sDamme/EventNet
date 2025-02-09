namespace EventNet.Application
{
    public sealed record GetAttendeeHandler(
        IAttendeeRepository attendeeRepository,
        IUnitOfWork unitOfWork)
        : IHandler<GetAttendeeRequest, AttendeeModel>
    {
        public async Task<Result<AttendeeModel>> HandleAsync(GetAttendeeRequest request)
        {
            var model = await attendeeRepository.GetModelAsync(request.Id);

            return new Result<AttendeeModel>(model is null ? NotFound : OK, model);
        }
    }
}
