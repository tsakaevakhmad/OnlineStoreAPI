using OnlineStoreAPI.BLL.Interfaces.Utilities;
using System.Reflection;

namespace OnlineStoreAPI.BLL.Utilities
{
    public class SortAndFilterManager : ISortAndFilterManager
    {
        public IEnumerable<T> SortBy<T>(IEnumerable<T> result, string sortBy, string orderType = "DESC")
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var property = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (orderType.ToUpper() == "ASC")
                    return result.OrderBy(x => property.GetValue(x));
                return result.OrderByDescending(x => property.GetValue(x));
            }
            return result;
        }
    }
}
