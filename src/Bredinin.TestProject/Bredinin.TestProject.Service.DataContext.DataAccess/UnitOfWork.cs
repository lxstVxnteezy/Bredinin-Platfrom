using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.DataContext;
using Bredinin.TestProject.Service.Domain.Base;

namespace Bredinin.TestProject.DataContext.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ServiceContext _serviceContext;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(ServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
            _repositories = new Dictionary<Type, object>();
        }
        
        public void Dispose()
        {
            _serviceContext.Dispose();
        }

        public Task SaveChangesAsync(CancellationToken ctn)
        {
            return _serviceContext.SaveChangesAsync(ctn);
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new BaseRepository<TEntity>(_serviceContext);
            _repositories.Add(typeof(TEntity),repository);
            return repository;
        }
    }
}
