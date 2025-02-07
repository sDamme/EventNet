using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventNet.Domain
{
    public sealed class Event : Entity
    {
        public string Name { get; private set; } = null!;
        public DateTime EventDate { get; private set; }
        public string Location { get; private set; }

        [StringLength(1000)]
        public string Description { get; private set; }
        private readonly List<Attendee> _attendees = [];
        public IReadOnlyCollection<Attendee> Attendees => _attendees.AsReadOnly();

        public Event(string name, DateTime eventDate, string location, string description)
        {
            SetName(name);
            SetEventDate(eventDate);
            SetLocation(location);
            SetDetails(description);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event name cannot be empty.");

            Name = name;
        }

        private void SetEventDate(DateTime date)
        {
            EventDate = date;
        }

        private void SetLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be empty.");

            Location = location;
        }

        private void SetDetails(string description)
        {
            if (!string.IsNullOrWhiteSpace(description) && description.Length > 1000)
                throw new ArgumentException($"Details cannot exceed 1000 characters.");

            Description = description;
        }

        public void Update(string name, DateTime eventDate, string location, string description)
        {
            SetName(name);
            SetEventDate(eventDate);
            SetLocation(location);
            SetDetails(description);
        }

        public void AddAttendee(Attendee attendee)
        {
            ArgumentNullException.ThrowIfNull(attendee);

            if (EventDate <= DateTime.UtcNow)
                throw new InvalidOperationException("Cannot add attendees to a past event.");

            if (_attendees.Any(attendee.IsDuplicateOf))
            {
                throw new InvalidOperationException("A duplicate attendee already exists for this event.");
            }

            _attendees.Add(attendee);
        }

        public void UpdateAttendee(long attendeeId, Action<Attendee> updateAction)
        {
            var attendee = _attendees.FirstOrDefault(a => a.Id == attendeeId);
            if (attendee == null)
                throw new InvalidOperationException("Attendee not found.");

            updateAction(attendee);
        }

        public void RemoveAttendee(long attendeeId)
        {
            var attendee = _attendees.FirstOrDefault(a => a.Id == attendeeId);
            if (attendee == null)
                throw new InvalidOperationException("Attendee not found.");

            _attendees.Remove(attendee);
        }
    }
}
