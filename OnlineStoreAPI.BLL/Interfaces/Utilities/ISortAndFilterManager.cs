namespace OnlineStoreAPI.BLL.Interfaces.Utilities
{
    public interface ISortAndFilterManager
    {
        public IEnumerable<T> SortBy<T>(IEnumerable<T> result, string sortBy, string orderType = "DESC");
    }
}
