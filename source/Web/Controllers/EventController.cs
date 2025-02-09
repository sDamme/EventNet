namespace EventNet.Web
{
    [ApiController]
    [Route("api/events")]
    public sealed class EventController : BaseController
    {
        [HttpPost]
        public IActionResult Add(AddEventRequest request) => Mediator.HandleAsync<AddEventRequest, long>(request).ApiResult();

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id) => Mediator.HandleAsync(new DeleteEventRequest(id)).ApiResult();

        [HttpGet("{id:long}")]
        public IActionResult Get(long id) => Mediator.HandleAsync<GetEventRequest, EventModel>(new GetEventRequest(id)).ApiResult();

        [HttpGet]
        public IActionResult List() => Mediator.HandleAsync<ListEventRequest, IEnumerable<EventModel>>(new ListEventRequest()).ApiResult();
    }
}
