namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IRepository<Entity> where Entity : class
    {
        public Task<Entity> GetAsync(int id);
        public Task<IEnumerable<Entity>> GetAsync();
        public Task<Entity> UpdateAsync(Entity data);
        public Task<Entity> DeleteAsync(int id);
        public Task<Entity> CreateAsync(Entity data);
    }
}
