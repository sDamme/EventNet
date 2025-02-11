namespace EventNet.Infrastructure
{
    public static class ContextSeed
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Event>().HasData(new
            {
                Id = 1L,
                Name = "Annual Developer Conference",
                EventDate = new DateTime(2025, 02, 07, 12, 0, 0, DateTimeKind.Utc),
                Location = "Convention Center, City",
                Description = "A conference for software developers to network and learn."
            });

            builder.Entity<BusinessAttendee>().HasData(new
            {
                Id = 1L,
                PaymentType = PaymentType.BankTransfer,
                EventId = 1L,
                LegalName = "Tech Corp Ltd",
                RegistrationCode = "TC123456",
                NumberOfAttendees = 5,
                Description = "Tech Corp's delegation attending the conference."
            });

            builder.Entity<IndividualAttendee>().HasData(new
            {
                Id = 2L,
                PaymentType = PaymentType.Cash,
                EventId = 1L,
                FirstName = "John",
                LastName = "Doe",
                PersonalIdCode = new PersonalIdCode("39506036025"),
                Description = "John Doe attending as an individual."
            });
        }
    }
}
