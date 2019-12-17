using System.Data.Entity;
using NorthwindWeb.Models;

namespace NorthwindWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        public UnitOfWork()
        {
            Context = new NorthwindDb();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }
    }
}