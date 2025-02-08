namespace EventNet.Infrastructure
{
    public sealed class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.ToTable(nameof(Attendee), nameof(Attendee));

            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(a => a.PaymentType)
                   .IsRequired();

            builder.Property(a => a.EventId)
                   .IsRequired();

            builder.HasDiscriminator<int>("AttendeeType")
                .HasValue<IndividualAttendee>(1)
                .HasValue<BusinessAttendee>(2);
        }

        public sealed class BusinessAttendeeConfiguration : IEntityTypeConfiguration<BusinessAttendee>
        {
            public void Configure(EntityTypeBuilder<BusinessAttendee> builder)
            {
                builder.Property(b => b.LegalName)
                       .IsRequired();

                builder.Property(b => b.RegistrationCode)
                       .IsRequired();

                builder.Property(b => b.NumberOfAttendees)
                       .IsRequired();

                builder.Property(b => b.Description)
                       .HasMaxLength(5000);
            }
        }

        public sealed class IndividualAttendeeConfiguration : IEntityTypeConfiguration<IndividualAttendee>
        {
            public void Configure(EntityTypeBuilder<IndividualAttendee> builder)
            {
                builder.Property(i => i.FirstName)
                       .IsRequired();

                builder.Property(i => i.LastName)
                       .IsRequired();

                builder.Property(i => i.PersonalIdCode)
                       .IsRequired();

                builder.Property(i => i.Description)
                       .HasMaxLength(1500)
                       .HasColumnName("IndividualDescription");
            }
        }
    }
}
