using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sample.Data.Models;
using Sample.Data.Models.DTOs;

namespace Sample.Data
{
    public class SampleDbContext : IdentityDbContext
    {

        private static IConfigurationRoot _configuration;
        private const string _systemUserId = "34206063-c1d2-422c-b2f5-b395f731c255";


        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryDetail> CategoryDetails { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GetItemsForListingDto> ItemsForListing { get; set; }
        public DbSet<AllItemsPipeDelimitedStringDto> AllItemsOutput { get; set; }
        public DbSet<GetItemsTotalValueDto> GetItemsTotalValues { get; set; }
        public DbSet<FullItemDetailDto> FullItemDetailDtos { get; set; }


        public SampleDbContext()
        {
        }


        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                _configuration = builder.Build();
                var cnstr = _configuration.GetConnectionString("SampleDB");
                optionsBuilder.UseSqlServer(cnstr);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                        .HasMany(x => x.Players)
                        .WithMany(p => p.Items)
                        .UsingEntity<Dictionary<string, object>>(
                            "ItemPlayers",
                            ip => ip.HasOne<Player>()
                                    .WithMany()
                                    .HasForeignKey("PlayerId")
                                    .HasConstraintName("FK_ItemPlayer_Players_PlayerId")
                                    .OnDelete(DeleteBehavior.Cascade),
                            ip => ip.HasOne<Item>()
                                    .WithMany()
                                    .HasForeignKey("ItemId")
                                    .HasConstraintName("FK_PlayerItem_Items_ItemId")
                                    .OnDelete(DeleteBehavior.ClientCascade)
                        );

            modelBuilder.Entity<GetItemsForListingDto>(x =>
            {
                x.HasNoKey();
                x.ToView("ItemsForListing");
            });

            modelBuilder.Entity<AllItemsPipeDelimitedStringDto>(x => {
                x.HasNoKey();
                x.ToView("AllItemsOutput");
            });

            modelBuilder.Entity<GetItemsTotalValueDto>(x =>
            {
                x.HasNoKey();
                x.ToView("GetItemsTotalValues");
            });

            var genreCreateDate = new DateTime(2021, 01, 01);
            modelBuilder.Entity<Genre>(x =>
            {
                x.HasData(
                    new Genre() { Id = 1, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Fantasy" },
                    new Genre() { Id = 2, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Sci/Fi" },
                    new Genre() { Id = 3, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Horror" },
                    new Genre() { Id = 4, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Comedy" },
                    new Genre() { Id = 5, CreatedDate = genreCreateDate, IsActive = true, IsDeleted = false, Name = "Drama" }
                );
            });

            modelBuilder.Entity<FullItemDetailDto>(x =>
            {
                x.HasNoKey();
                x.ToView("FullItemDetailDtos");
            });
            base.OnModelCreating(modelBuilder);
        }

        private void SaveChanagesHandler(ChangeTracker tracker)
        {
            foreach (var entry in tracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        if (entry?.Entity is ISoftDeletable)
                        {
                            ((ISoftDeletable)entry.Entity).IsDeleted = true;
                            if (entry.Entity is IAuditedModel)
                            {
                                var entity = entry.Entity as IAuditedModel;
                                if (string.IsNullOrWhiteSpace(entity.LastModifiedUserId))
                                    entity.LastModifiedUserId = _systemUserId;
                                    entity.LastModifiedDate = DateTime.Now;
                            }
                            entry.State = EntityState.Modified;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Entity is IAuditedModel)
                        {
                            var entity = entry.Entity as IAuditedModel;
                            if (string.IsNullOrWhiteSpace(entity.LastModifiedUserId))
                                entity.LastModifiedUserId = _systemUserId;
                            entity.LastModifiedDate = DateTime.Now;
                        }
                        break;
                    case EntityState.Added:
                        if (entry.Entity is IAuditedModel)
                        {
                            var entity = entry.Entity as IAuditedModel;
                            if (string.IsNullOrWhiteSpace(entity.CreatedByUserId))
                                entity.CreatedByUserId = _systemUserId;
                            entity.CreatedDate = DateTime.Now;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public override int SaveChanges()
        {
            SaveChanagesHandler(ChangeTracker);
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SaveChanagesHandler(ChangeTracker);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SaveChanagesHandler(ChangeTracker);
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SaveChanagesHandler(ChangeTracker);
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }        
    }
}
