using HiLoGame.Domain;

namespace HiLoGame.Application;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(Guid Id);
}