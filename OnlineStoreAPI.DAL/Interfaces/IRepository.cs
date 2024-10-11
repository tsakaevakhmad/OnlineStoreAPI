namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IRepository<Entity, Id> where Entity : class
    {
        public Task<Entity> GetAsync(Id id);
        public Task<IEnumerable<Entity>> GetAsync();
        public Task<Entity> UpdateAsync(Entity data);
        public Task<Entity> DeleteAsync(Id id);
        public Task<Entity> CreateAsync(Entity data);
    }
}
