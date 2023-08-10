namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IRepositoryCacheServices 
    {
        public Task<bool> OnCreateAsync<T>(string key, T value, int minutes);
        public Task<T> OnUpdateAsync<T>(string key, T value, int minutes);
        public Task OnDeleteAsync(string key);
        public Task<T> OnGetAsync<T>(string key);
        public Task<bool> AddAsync<T>(string key, T value, int minutes);
    }
}
