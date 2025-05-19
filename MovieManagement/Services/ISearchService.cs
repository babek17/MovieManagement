using System.Linq.Expressions;

namespace MovieManagement.Services;

public interface ISearchService<T>
{
    Task<IEnumerable<T>> SearchAsync(string query);
}