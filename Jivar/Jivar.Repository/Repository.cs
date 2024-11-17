using Jivar.DAO;
using Jivar.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Jivar.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BaseDAO<T> _dao;
        public Repository(BaseDAO<T> dao) { _dao = dao; }
        public IEnumerable<T> GetAllWithPagingAndSorting(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int pageNumber = 1,
            int pageSize = 10) => _dao.GetAllWithPagingAndSorting(filter, includeProperties, orderBy, pageNumber, pageSize);
        public bool Add(T entity) => _dao.Add(entity);

        public bool AddRange(IEnumerable<T> entities) => _dao.AddRange(entities);

        public int Count(Expression<Func<T, bool>>? filter) => _dao.Count(filter);

        public bool Delete(T entity) => _dao.Delete(entity);

        public bool DeleteRange(IEnumerable<T> entities) => _dao.DeleteRange(entities);

        public T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null) => _dao.Get(filter, includeProperties);

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null) => _dao.GetAll(filter, includeProperties);

        public bool Update(T entity) => _dao.Update(entity);

        public bool UpdateRange(IEnumerable<T> entities) => _dao.UpdateRange(entities);

        public async Task<bool> AddAsync(T entity) => await _dao.AddAsync(entity);

        public async Task<bool> AddRangeAsync(IEnumerable<T> entities) => await _dao.AddRangeAsync(entities);

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filter) => await _dao.CountAsync(filter);

        public async Task<bool> DeleteAsync(T entity) => await _dao.DeleteAsync(entity);

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities) => await _dao.DeleteRangeAsync(entities);

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
            => await _dao.GetAsync(filter, includeProperties);

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
            => await _dao.GetAllAsync(filter, includeProperties);

        public async Task<bool> UpdateAsync(T entity) => await _dao.UpdateAsync(entity);

        public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities) => await _dao.UpdateRangeAsync(entities);
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dao.BeginTransactionAsync();
        }
    }
}
