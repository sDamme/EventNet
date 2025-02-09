namespace EventNet.Application
{
    public sealed record UpdateAttendeeHandler(
        IAttendeeRepository attendeeRepository, 
        IUnitOfWork unitOfWork) 
        : IHandler<UpdateAttendeeRequest>
    {
        public async Task<Result> HandleAsync(UpdateAttendeeRequest request)
        {
            var attendee = await attendeeRepository.GetAsync(request.AttendeeId);
            if (attendee is null)
                return new Result(NotFound);

            if (attendee is IndividualAttendee individual)
            {
                individual.UpdateIndividualAttendee(
                    paymentType: request.PaymentType,
                    firstName: request.FirstName,
                    lastName: request.LastName,
                    personalIdCode: request.PersonalIdCode,
                    details: request.Description ?? string.Empty
                );
            }
            else if (attendee is BusinessAttendee business)
            {
                business.UpdateBusinessAttendee(
                    paymentType: request.PaymentType,
                    legalName: request.LegalName,
                    registrationCode: request.RegistrationCode,
                    numberOfAttendees: request.NumberOfAttendees!.Value,
                    description: request.Description ?? string.Empty
                );
            }
            else
            {
                throw new InvalidOperationException("Unknown attendee type.");
            }

            await attendeeRepository.UpdateAsync(attendee);
            await unitOfWork.SaveChangesAsync();

            return new Result(NoContent);
        }
    }
}
