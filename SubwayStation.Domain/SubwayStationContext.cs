using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SubwayStation.Domain.Entities;
using System.Security.Claims;

namespace SubwayStation.Domain
{
    public class SubwayStationContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubwayStationContext(DbContextOptions<SubwayStationContext> options, IHttpContextAccessor HttpContextAccessor) : base(options) 
        {
            _httpContextAccessor = HttpContextAccessor;
        }

        #region Tables
        public DbSet<Subways> Subways { get; set; }
        public DbSet<Geometric> Geometrics { get; set; }
        public DbSet<Frequently> Frequentlies { get; set; }
        #endregion


        #region Override Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Get Entities
            var EntityBaseSet = ChangeTracker.Entries<EntityBase>();
            Guid userId = Guid.Empty;

            Claim userClaim = _httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);

            if (userClaim != null)
            {
                userId = Guid.Parse(userClaim.Value);
            }

            if (EntityBaseSet.Any())
            {
                DateTime currentDate = DateTime.Now;

                // Fill audit fields
                foreach (var auditableEntity in EntityBaseSet.Where(c => c.State == EntityState.Added || c.State == EntityState.Modified))
                {
                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.UserId = userId;
                        auditableEntity.Entity.CreatedDate = currentDate;
                    }
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        #endregion
    }
}
