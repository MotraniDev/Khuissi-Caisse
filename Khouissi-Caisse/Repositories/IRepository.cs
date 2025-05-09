using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Khouissi_Caisse.Repositories;

/// <summary>
/// Generic repository interface for database operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Get entities that match the specified condition
    /// </summary>
    /// <param name="predicate">Filter condition</param>
    /// <returns>List of matching entities</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get a specific entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Entity with matching ID or null if not found</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Add a new entity
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>Added entity</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Update an existing entity
    /// </summary>
    /// <param name="entity">Entity with updated values</param>
    /// <returns>Updated entity</returns>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Delete an entity
    /// </summary>
    /// <param name="entity">Entity to delete</param>
    /// <returns>Task representing the async operation</returns>
    Task DeleteAsync(T entity);

    /// <summary>
    /// Delete an entity by ID
    /// </summary>
    /// <param name="id">ID of entity to delete</param>
    /// <returns>Task representing the async operation</returns>
    Task DeleteByIdAsync(int id);

    /// <summary>
    /// Check if any entity matches the specified condition
    /// </summary>
    /// <param name="predicate">Condition to check</param>
    /// <returns>True if any entity matches, otherwise false</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get count of entities that match the specified condition
    /// </summary>
    /// <param name="predicate">Filter condition</param>
    /// <returns>Count of matching entities</returns>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
}
