using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.Context
{
    public interface IDatabaseContext //: IDisposable
    {
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbQuery<TQuery> Query<TQuery>() where TQuery : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        private readonly string _connectionString;
        private readonly bool _lazyLoadingEnabled;

        public DatabaseContext(string connectionString, bool lazyLoadingEnabled = false)
        {
            _connectionString = connectionString;

            _lazyLoadingEnabled = lazyLoadingEnabled;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            optionsBuilder.UseLazyLoadingProxies(_lazyLoadingEnabled);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);
        }

        //public override void Dispose()
        //{
        //    base.Dispose();
        //}
    }
}
