using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Jivar.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAllWithPagingAndSorting(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int pageNumber = 1,
            int pageSize = 10);
        /// <summary>
        /// Adds a single entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>True if the entity was added successfully, false otherwise.</returns>
        bool Add(T entity);

        /// <summary>
        /// Adds a collection of entities to the database.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        /// <returns>True if the entities were added successfully, false otherwise.</returns>
        bool AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>True if the entity was updated successfully, false otherwise.</returns>
        bool Update(T entity);

        /// <summary>
        /// Updates a collection of entities in the database.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        /// <returns>True if the entities were updated successfully, false otherwise.</returns>
        bool UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>True if the entity was deleted successfully, false otherwise.</returns>
        bool Delete(T entity);

        /// <summary>
        /// Deletes a collection of entities from the database.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        /// <returns>True if the entities were deleted successfully, false otherwise.</returns>
        bool DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Counts the number of entities in the database that match a given filter.
        /// </summary>
        /// <param name="filter">An optional filter to apply to the count operation.</param>
        /// <returns>The count of entities matching the filter criteria.</returns>
        int Count(Expression<Func<T, bool>>? filter);

        /// <summary>
        /// Retrieves all entities that match a specified filter and includes related properties.
        /// </summary>
        /// <param name="filter">An optional filter to apply when retrieving entities.</param>
        /// <param name="includeProperties">Comma-separated list of related properties to include.</param>
        /// <returns>A collection of entities that match the filter and include specified properties.</returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        /// <summary>
        /// Retrieves a specific entity that matches a given filter and includes related properties.
        /// </summary>
        /// <param name="filter">Filter to apply when retrieving the entity.</param>
        /// <param name="includeProperties">Comma-separated list of related properties to include.</param>
        /// <returns>The first entity that matches the filter, or null if no match is found.</returns>
        T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        /// <summary>
        /// Asynchronously adds a single entity to the database.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the entity was added successfully.</returns>
        Task<bool> AddAsync(T entity);

        /// <summary>
        /// Asynchronously adds a collection of entities to the database.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the entities were added successfully.</returns>
        Task<bool> AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Asynchronously updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the entity was updated successfully.</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Asynchronously updates a collection of entities in the database.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the entities were updated successfully.</returns>
        Task<bool> UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Asynchronously deletes an entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the entity was deleted successfully.</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Asynchronously deletes a collection of entities from the database.
        /// </summary>
        /// <param name="entities">The collection of entities to delete.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the entities were deleted successfully.</returns>
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Asynchronously counts the number of entities in the database that match a given filter.
        /// </summary>
        /// <param name="filter">An optional filter to apply to the count operation.</param>
        /// <returns>A task that represents the asynchronous operation. Returns the count of entities matching the filter criteria.</returns>
        Task<int> CountAsync(Expression<Func<T, bool>>? filter);

        /// <summary>
        /// Asynchronously retrieves all entities that match a specified filter and includes related properties.
        /// </summary>
        /// <param name="filter">An optional filter to apply when retrieving entities.</param>
        /// <param name="includeProperties">Comma-separated list of related properties to include.</param>
        /// <returns>A task that represents the asynchronous operation. Returns a collection of entities that match the filter and include specified properties.</returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        /// <summary>
        /// Asynchronously retrieves a specific entity that matches a given filter and includes related properties.
        /// </summary>
        /// <param name="filter">Filter to apply when retrieving the entity.</param>
        /// <param name="includeProperties">Comma-separated list of related properties to include.</param>
        /// <returns>A task that represents the asynchronous operation. Returns the first entity that matches the filter, or null if no match is found.</returns>
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null);

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
