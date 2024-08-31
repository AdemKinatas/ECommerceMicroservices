using MongoDB.Driver;
using Order.Domain.Common;
using Order.Domain.Interfaces;
using System.Linq.Expressions;

namespace Order.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : MongoBaseModel, new()
{
    protected readonly IMongoCollection<T> _collection;

    public GenericRepository(IMongoClient mongoClient, string databaseName, string collectionName)
    {
        var database = mongoClient.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    private string GetKeyPropertyName()
    {
        return typeof(T).GetProperty("Id")?.GetValue(null)?.ToString() ?? "Id"; 
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        var keyProperty = GetKeyPropertyName();
        var filter = Builders<T>.Filter.Eq(keyProperty, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        var keyProperty = GetKeyPropertyName();
        var keyValue = entity.GetType().GetProperty(keyProperty)?.GetValue(entity);
        var filter = Builders<T>.Filter.Eq(keyProperty, keyValue);
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(T entity)
    {
        var keyProperty = GetKeyPropertyName();
        var keyValue = entity.GetType().GetProperty(keyProperty)?.GetValue(entity);
        var filter = Builders<T>.Filter.Eq(keyProperty, keyValue);
        await _collection.DeleteOneAsync(filter);
    }
}
