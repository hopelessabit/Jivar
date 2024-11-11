using System.Linq.Expressions;

namespace Jivar.DAO.Interface
{
    public interface IBaseDAO<T> where T : class
    {
        bool Add(T entity);
        bool AddRange(IEnumerable<T> entities);
        bool Update(T entity);
        bool UpdateRange(IEnumerable<T> entities);
        bool Delete(T entity);
        bool DeleteRange(IEnumerable<T> entities);
        T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        int Count(Expression<Func<T, bool>>? filter = null);

        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        Task<bool> UpdateAsync(T entity);
        Task<bool> UpdateRangeAsync(IEnumerable<T> entities);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
    }
}
