using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using AirPortWebApi.Infrastructure.Interfaces;

namespace AirPortWebApi.Data.Application
{
    public class ApplicationLogsDbContext : System.Data.Entity.DbContext, IDbContext
    {
        public ApplicationLogsDbContext()
            : base("ApplicationLogModelsConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
        }

        static ApplicationLogsDbContext()
        {
        }

        public virtual DbSet<ApplicationLogModel> ApplicationLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationLogModel>().HasKey(x => x.Id);

            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.Id)
                .HasColumnName("Id").IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.HttpMessageType)
                .HasColumnName("HttpMessageType")
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.ServerDateTime)
                .HasColumnName("ServerDateTime")
                .IsRequired()
                .HasColumnType("datetime");
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.CorrelationId).HasColumnName("CorrelationId").IsRequired().HasColumnType("uniqueidentifier");
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.RequestUri)
                .HasColumnName("RequestUri")
                .IsOptional()
                .HasColumnType("nvarchar")
                .HasMaxLength(500);
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.Method)
                .HasColumnName("Method")
                .IsOptional()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.StatusCode)
                .HasColumnName("StatusCode")
                .IsOptional()
                .HasColumnType("int");
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.IpAddress)
                .HasColumnName("IpAddress")
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.UserName)
                .HasColumnName("UserName")
                .IsOptional()
                .HasColumnType("nvarchar")
                .HasMaxLength(100);
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.AttendeeId)
                .HasColumnName("AttendeeId")
                .IsOptional()
                .HasColumnType("int");
            modelBuilder.Entity<ApplicationLogModel>()
                .Property(x => x.RawData)
                .HasColumnName("RawData")
                .IsOptional()
                .HasColumnType("nvarchar(MAX)")
                .IsMaxLength();
            base.OnModelCreating(modelBuilder);
        }
    }
}
