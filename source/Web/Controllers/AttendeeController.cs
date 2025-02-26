namespace EventNet.Web
{
    [ApiController]
    [Route("api/events/{eventId:long}/attendees")]
    public sealed class AttendeeController : BaseController
    {
        // POST api/events/{eventId}/attendees
        [HttpPost]
        public IActionResult Add(long eventId, AddAttendeeRequest request)
        {
            var fullRequest = request with { EventId = eventId };
            return Mediator.HandleAsync<AddAttendeeRequest, long>(fullRequest).ApiResult();
        }

        // GET api/events/{eventId}/attendees/{attendeeId}
        [HttpGet("{attendeeId:long}")]
        public IActionResult Get(long eventId, long attendeeId)
        {
            return Mediator.HandleAsync<GetAttendeeRequest, AttendeeModel>(new GetAttendeeRequest(attendeeId)).ApiResult();
        }

        // PUT api/events/{eventId}/attendees/{attendeeId}
        [HttpPut("{attendeeId:long}")]
        public IActionResult Update(long eventId, long attendeeId, UpdateAttendeeRequest request)
        {
            var fullRequest = request with { Id = attendeeId };
            return Mediator.HandleAsync<UpdateAttendeeRequest>(fullRequest).ApiResult();
        }

        // DELETE api/events/{eventId}/attendees/{attendeeId}
        [HttpDelete("{attendeeId:long}")]
        public IActionResult Delete(long eventId, long attendeeId)
        {
            return Mediator.HandleAsync(new DeleteAttendeeRequest(eventId, attendeeId)).ApiResult();
        }
    }
}
