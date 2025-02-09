namespace EventNet.Application
{
    public sealed record AddAttendeeHandler(
        IEventRepository eventRepository, 
        IUnitOfWork unitOfWork)
        : IHandler<AddAttendeeRequest, long>
    {
        public async Task<Result<long>> HandleAsync(AddAttendeeRequest request)
        {
            var eventEntity = await eventRepository.GetAsync(request.EventId);
            if (eventEntity is null)
            {
                return new Result<long>(NotFound, 0);
            }

            Attendee attendee;

            if (!string.IsNullOrEmpty(request.FirstName) &&
                !string.IsNullOrEmpty(request.LastName) &&
                !string.IsNullOrEmpty(request.PersonalIdCode))
            {
                attendee = new IndividualAttendee(
                    paymentType: request.PaymentType,
                    eventId: request.EventId,
                    firstName: request.FirstName,
                    lastName: request.LastName,
                    personalIdCode: request.PersonalIdCode,
                    description: request.Description ?? string.Empty
                );
            }
            else if (!string.IsNullOrEmpty(request.LegalName) &&
                     !string.IsNullOrEmpty(request.RegistrationCode) &&
                     request.NumberOfAttendees.HasValue)
            {
                attendee = new BusinessAttendee(
                    paymentType: request.PaymentType,
                    eventId: request.EventId,
                    legalName: request.LegalName,
                    registrationCode: request.RegistrationCode,
                    numberOfAttendees: request.NumberOfAttendees.Value,
                    description: request.Description ?? string.Empty
                );
            }
            else
            {
                throw new ArgumentException("Invalid attendee details. Provide either individual or business attendee parameters.");
            }

            eventEntity.AddAttendee(attendee);
            
            await eventRepository.UpdateAsync(eventEntity);
            await unitOfWork.SaveChangesAsync();

            return new Result<long>(Created, attendee.Id);
        }
    }
}