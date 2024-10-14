using Bredinin.TestProject.Service.DataContext;
using Bredinin.TestProject.Service.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Bredinin.TestProject.DataContext.DataAccess.Repositories
{
    internal class BaseRepository <TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ServiceContext Context;

        public BaseRepository(ServiceContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> Query => Context.Set<TEntity>();

        public virtual TEntity GetById(Guid id)
        {
            return Context.Set<TEntity>().Single(x => x.Id == id);
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id, CancellationToken ctn)
        {
            return Context.Set<TEntity>().SingleAsync(x => x.Id == id, ctn);
        }

        public TEntity? FoundById(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public Task<TEntity?> FoundByIdAsync(Guid id, CancellationToken ctn)
        {
            return Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id, ctn);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(TEntity[] entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void RemoveById(Guid id)
        {
            var entity = GetById(id);

            Context.Set<TEntity>().Remove(entity);
        }

        public Task SaveChanges(CancellationToken ctn)
        {
            return Context.SaveChangesAsync(ctn);
        }
    }
}
