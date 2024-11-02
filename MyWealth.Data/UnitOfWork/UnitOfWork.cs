using Microsoft.EntityFrameworkCore.Storage;
using MyWealth.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyWealthDbContext _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(MyWealthDbContext db)
        {
            _db = db;
        }

        // begin transaction
        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        // commit changes to databa
        public async Task CommitTransaction()
        {
            await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose();

            // Garbage Collector'a sen bunu silebilirsin izni verir
            // Hemen silinmez silinebilir yapıyor

            // GC.Collect();
            // GC.WaitForPendingFinalizers();
            // Bu kodlar GC yi direkt çalıştırır
        }

        // rollback changes
        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        // Changes transferred to database
        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
