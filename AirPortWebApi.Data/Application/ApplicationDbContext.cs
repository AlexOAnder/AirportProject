using System.Data.Entity;
using AirPortWebApi.Data.DbContext;
using AirPortWebApi.Data.DbContext.Models;
using AirPortWebApi.Infrastructure.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;


namespace AirPortWebApi.Data.Application
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        public ApplicationDbContext()
            : base("AirportAdmin", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true; 
        }

        static ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserClient>()
                .Property(y => y.Name).HasMaxLength(100);
            modelBuilder.Entity<RefreshToken>()
                .Property(y => y.Subject).HasMaxLength(50);
            modelBuilder.Entity<RefreshToken>()
                .Property(y => y.ClientId).HasMaxLength(128);
            modelBuilder.Entity<RefreshToken>()
                .Property(y => y.ProtectedTicket).IsRequired();
            modelBuilder.Entity<RefreshToken>()
                .Property(y => y.Id);
            modelBuilder.Entity<UserClient>()
                .HasOptional(e => e.RefreshToken);

            modelBuilder.Entity<RefreshToken>()
                .HasRequired(e => e.UserClient)
                .WithMany().HasForeignKey(e => e.ClientId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.UserClients)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(true);


            base.OnModelCreating(modelBuilder);
        }
        
        public virtual DbSet<UserClient> UserClients { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}