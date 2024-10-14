using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bredinin.TestProject.DataContext.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
    internal class UnitOfWork
    {
    }
}
