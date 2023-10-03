namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IRepositoryCacheServices 
    {
        public Task OnCreateAsync<T>(string listKey, T value, int minutes);
        public Task<T> OnUpdateAsync<T>(string key, string listKey, T value, int minutes, Func<T, bool> predicate);
        public Task DeleteAsync(string key);
        public Task<T> OnGetAsync<T>(string key);
        public Task AddAsync<T>(string key, T value, int minutes);
        public Task OnDeleteAsync<T>(string key, string listKey, int minutes, Func<T, bool> predicate);
        public Task OnDeleteAsync<T>(string key, string listKey);
    }
}
