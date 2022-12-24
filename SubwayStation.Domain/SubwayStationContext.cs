using Microsoft.EntityFrameworkCore;
using SubwayStation.Domain.Entities;

namespace SubwayStation.Domain
{
    public class SubwayStationContext : DbContext
    {
        public SubwayStationContext(DbContextOptions options) : base(options) { }

        #region Tables

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

            if (EntityBaseSet.Any())
            {
                DateTime currentDate = DateTime.Now;

                // Fill audit fields
                foreach (var auditableEntity in EntityBaseSet.Where(c => c.State == EntityState.Added || c.State == EntityState.Modified))
                {
                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.IsActive = true;
                        auditableEntity.Entity.CreatedDate = currentDate;
                    }

                    if (auditableEntity.State == EntityState.Modified || auditableEntity.State == EntityState.Deleted)
                    {
                        auditableEntity.Entity.CreatedDate = currentDate;
                    }
                }
            }

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        #endregion
    }
}
