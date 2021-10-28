using FluentValidation.Results;
using GlassLewis.Core.Data.Interfaces;
using GlassLewis.Core.Mediator.Interface;
using GlassLewis.Core.Messages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using GlassLewis.Company.Infrastructure.Extension;

namespace GlassLewis.Company.Infrastructure.Data.Context
{
    public class CompanyContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CompanyContext(DbContextOptions<CompanyContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Domain.Entities.Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyContext).Assembly);
        }
        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (success)
                await _mediatorHandler.PublishEvents(this);

            return success;
        }
    }
}
