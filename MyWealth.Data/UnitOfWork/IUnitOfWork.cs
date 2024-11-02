using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(); // Changes transferred to database

        Task BeginTransaction(); // begin transaction

        Task CommitTransaction(); // commit changes to database

        Task RollBackTransaction(); // rollback changes
    }
}
