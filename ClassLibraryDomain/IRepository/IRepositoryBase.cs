using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ClassLibraryDomain.Entity;
using ClassLibraryDomain.IRepository;
using ClassLibraryDomain.Models;






namespace ClassLibraryDomain.IRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification);
    }
}