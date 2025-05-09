﻿

namespace EventNet.Infrastructure
{
    public sealed class AttendeeRepository(Context context) : EFRepository<Attendee>(context), IAttendeeRepository
    {

        public async Task<IEnumerable<AttendeeModel>> ListModelAsync()
        {
            var entities = await Queryable.ToListAsync();
            return entities.Select(MapToModel);
        }

        public async Task<AttendeeModel> GetModelAsync(long id)
        {
            var entity = await Queryable.SingleOrDefaultAsync(e => e.Id == id);
            return MapToModel(entity);
        }

        private AttendeeModel MapToModel(Attendee entity)
        {
            if (entity is IndividualAttendee individual)
            {
                return new IndividualAttendeeModel
                {
                    Id = individual.Id,
                    AttendeeType = individual.AttendeeType,
                    PaymentType = individual.PaymentType.ToString(),
                    Description = individual.Description,
                    FirstName = individual.FirstName,
                    LastName = individual.LastName,
                    PersonalIdCode = individual.PersonalIdCode.ToString()
                };
            }
            else if (entity is BusinessAttendee business)
            {
                return new BusinessAttendeeModel
                {
                    Id = business.Id,
                    AttendeeType = business.AttendeeType,
                    PaymentType = business.PaymentType.ToString(),
                    Description = business.Description,
                    LegalName = business.LegalName,
                    RegistrationCode = business.RegistrationCode,
                    NumberOfAttendees = business.NumberOfAttendees
                };
            }

            throw new InvalidOperationException("Unknown attendee type.");
        }
    }
}
