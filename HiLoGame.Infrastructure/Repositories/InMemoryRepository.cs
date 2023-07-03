using HiLoGame.Application;
using HiLoGame.Domain;

namespace HiLoGame.Infrastructure;

public class InMemoryRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ICollection<TEntity> _inMemoryData;
    public InMemoryRepository() => this._inMemoryData = new List<TEntity>();
    public Task<TEntity> CreateAsync(TEntity entity)
    {
        _inMemoryData.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<TEntity> GetByIdAsync(Guid Id)
    {
        var entity = _inMemoryData.FirstOrDefault(x => x.Id == Id);

        if (entity is null)
            throw new Exception("Entity not found");

        return Task.FromResult(entity);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        var previousState = _inMemoryData.FirstOrDefault(x => x.Id == entity.Id);

        if (previousState is not null)
            _inMemoryData.Remove(previousState);

        _inMemoryData.Add(entity);
        return Task.FromResult(entity);
    }
}