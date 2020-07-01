using AuditableEntities.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditableEntities.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        private const string appUser = "SampleApplication";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor = null) : base(options)
        {
            if (httpContextAccessor != null)
            {
                _httpContextAccessor = httpContextAccessor;
            }
        }

        public DbSet<Book> Books { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddAuditInfo()
        {
            var entities = ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var utcNow = DateTime.UtcNow;
            var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? appUser;
            var ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedOnUtc = utcNow;
                    entity.Entity.CreatedBy = user;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.LastModifiedOnUtc = utcNow;
                    entity.Entity.LastModifiedBy = user;
                }

                entity.Entity.IPAddress = ipAddress;
            }
        }
    }
}
