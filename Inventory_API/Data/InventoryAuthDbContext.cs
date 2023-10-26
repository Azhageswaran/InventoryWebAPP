using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory_API.Data
{
    public class InventoryAuthDbContext : IdentityDbContext
    {
        public InventoryAuthDbContext(DbContextOptions<InventoryAuthDbContext> options): base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var staffRoleId = "72e31730-e144-4ef2-ae94-16255336362d";
            var adminRoleId = "6593c616-0d37-45fb-b921-a94260055cdc";

            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Id = staffRoleId,
                    ConcurrencyStamp = staffRoleId,
                    Name = "Staff",
                    NormalizedName = "Staff".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp=adminRoleId,
                    Name = "Admin",
                    NormalizedName="Admin".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
