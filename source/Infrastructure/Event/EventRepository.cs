namespace EventNet.Infrastructure
{
    public sealed class EventRepository(Context context) : EFRepository<Event>(context), IEventRepository
    {
        public static Expression<Func<Event, EventModel>> Model => entity => new EventModel {
            Id = entity.Id,
            Name = entity.Name,
            EventDate = entity.EventDate,
            Location = entity.Location,
            Description = entity.Description,
        };

        public async Task<EventModel> GetModelAsync(long id)
        {
            // First, retrieve the event with its attendees from the database.
            var entity = await Queryable
                .Where(e => e.Id == id)
                .Include(e => e.Attendees)
                .SingleOrDefaultAsync();

            if (entity == null)
                return null;

            // Map the event entity to your EventModel.
            var model = new EventModel
            {
                Id = entity.Id,
                Name = entity.Name,
                EventDate = entity.EventDate,
                Location = entity.Location,
                Description = entity.Description,
                // Reuse your mapping method for each attendee.
                Attendees = entity.Attendees.Select(MapToModel).ToList()
            };

            // Compute the total attendee count:
            // - Each IndividualAttendee counts as 1.
            // - Each BusinessAttendee counts as its NumberOfAttendees.
            model.AttendeeCount = entity.Attendees.Sum(a =>
                a is BusinessAttendee business ? business.NumberOfAttendees : 1);

            return model;
        }

        public Task<Grid<EventModel>> GridAsync(GridParameters parameters) => Queryable.Select(Model).GridAsync(parameters);
        
        public async Task<IEnumerable<EventModel>> ListModelAsync() => await Queryable.Select(Model).ToListAsync();
        
        // TODO: clean duplication of polymorphism mapping
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
