using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventNet.Model
{
    public sealed record EventModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<AttendeeModel> Attendees { get; set; } = [];
    }
}
