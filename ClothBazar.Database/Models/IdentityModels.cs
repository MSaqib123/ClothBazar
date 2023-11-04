using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using ClothBazar.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClothBazar.Database.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ClothBazarConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }


        public System.Data.Entity.DbSet<ClothBazar.Entities.Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Conf> configuraton { get; set; }

        //______________ CheckOut ______________
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderUserInfo> orderUsers { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}