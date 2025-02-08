namespace EventNet.Infrastructure
{
    public sealed class EventRepository(Context context) : EFRepository<Event>(context), IEventRepository
    {
        public static Expression<Func<Event, EventModel>> Model => entity => new EventModel { Id = entity.Id, Name = entity.Name };

        public Task<EventModel> GetModelAsync(long id) => Queryable.Where(entity => entity.Id == id).Select(Model).SingleOrDefaultAsync();

        public Task<Grid<EventModel>> GridAsync(GridParameters parameters) => Queryable.Select(Model).GridAsync(parameters);

        public async Task<IEnumerable<EventModel>> ListModelAsync() => await Queryable.Select(Model).ToListAsync();
    }
}
