using System.ComponentModel.DataAnnotations;

namespace EventNet.Domain
{
    public abstract class Attendee : Entity
    {
        public PaymentType PaymentType { get; protected set; }
        public long EventId { get; protected set; }

        protected Attendee(PaymentType paymentType, long eventId)
        {
            PaymentType = paymentType;
            EventId = eventId;
        }

        public abstract bool IsDuplicateOf(Attendee other);

        public void UpdatePaymentType(PaymentType newPaymentType)
        {
            PaymentType = newPaymentType;
        }
    }

    public class BusinessAttendee : Attendee
    {

        public string LegalName { get; private set; }
        public string RegistrationCode { get; private set; }
        public int NumberOfAttendees { get; private set; }
        [StringLength(5000)]
        public string Description { get; private set; }

        public BusinessAttendee(
            PaymentType paymentType,
            long eventId,
            string legalName,
            string registrationCode,
            int numberOfAttendees,
            string description
        ) : base(paymentType, eventId)
        {
            SetLegalName(legalName);
            SetRegistrationCode(registrationCode);
            SetNumberOfAttendees(numberOfAttendees);
            SetDetails(description);
        }

        public override bool IsDuplicateOf(Attendee other)
        {
            return other is BusinessAttendee business && RegistrationCode == business.RegistrationCode;
        }
        private void SetLegalName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Legal name cannot be empty.");

            LegalName = name;
        }

        private void SetRegistrationCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Registration code cannot be empty.");

            RegistrationCode = code;
        }

        private void SetNumberOfAttendees(int count)
        {
            if (count < 1)
                throw new ArgumentException("Number of attendees must be at least 1.");

            NumberOfAttendees = count;
        }

        private void SetDetails(string description)
        {
            if (!string.IsNullOrWhiteSpace(description) && description.Length > 5000)
                throw new ArgumentException($"Details cannot exceed 5000 characters.");

            Description = description;
        }

        public void UpdateBusinessAttendee(
            PaymentType paymentType,
            string legalName,
            string registrationCode,
            int numberOfAttendees,
            string description
        )
        {
            UpdatePaymentType(paymentType);
            SetLegalName(legalName);
            SetRegistrationCode(registrationCode);
            SetNumberOfAttendees(numberOfAttendees);
            SetDetails(description);
        }
    }



    public class IndividualAttendee : Attendee
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public PersonalIdCode PersonalIdCode { get; private set; }
        [StringLength(1500)]
        public string Description { get; private set; }

        public IndividualAttendee(
            PaymentType paymentType,
            long eventId,
            string firstName,
            string lastName,
            string personalIdCode,
            string description
        ) : base(paymentType, eventId)
        {
            SetNames(firstName, lastName);
            PersonalIdCode = new PersonalIdCode(personalIdCode);
            SetDetails(description);
        }

        public override bool IsDuplicateOf(Attendee other)
        {
            return other is IndividualAttendee individual &&
                   PersonalIdCode.Equals(individual.PersonalIdCode);
        }

        private void SetNames(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");

            FirstName = firstName;
            LastName = lastName;
        }

        private void SetDetails(string description)
        {
            if (!string.IsNullOrWhiteSpace(description) && description.Length > 1500)
                throw new ArgumentException($"Details cannot exceed 1500 characters.");

            Description = description;
        }

        public void UpdateIndividualAttendee(
            PaymentType paymentType,
            string firstName,
            string lastName,
            string personalIdCode,
            string details
        )
        {
            UpdatePaymentType(paymentType);
            SetNames(firstName, lastName);
            PersonalIdCode = new PersonalIdCode(personalIdCode);
            SetDetails(details);
        }
    }
}
