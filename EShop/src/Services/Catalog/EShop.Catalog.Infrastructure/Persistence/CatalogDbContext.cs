using EShop.Catalog.Domain.Aggregates;
using EShop.Shared.SharedLibrary.Domain;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        private readonly IMediator _mediator;

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //1. ilgili entity'lerdeki olay içeren nesneleri al
            var domainEntities = ChangeTracker.Entries<IAggregateRoot>()
                                              .Where(x => x.Entity.DomainEvents != null
                                                       && x.Entity.DomainEvents.Any()
                                              );
            //2. ilgili olayları al
            var domainEvents = domainEntities
                               .SelectMany(x => x.Entity.DomainEvents)
                               .ToList();

            //3. ilgili olayları yayınla

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }



            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Description).HasMaxLength(250);

            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(150).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).HasMaxLength(250);
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(p => p.Stock).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ImageUrl).HasMaxLength(250);
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).IsRequired(false);

            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(p => p.CategoryId).IsRequired(false);


            modelBuilder.Entity<Category>().HasData(
                new Category(1, "Elektronik", "Elektronik Ürünler"),
                new Category(2, "Giyim", "Giyim Ürünleri"),
                new Category(3, "Kozmetik", "Kozmetik Ürünler")


             );

            modelBuilder.Entity<Product>().HasData(
                new Product("Iphone 12", "Apple Iphone 12", 10000, 100, 1),
                new Product("Samsung S21", "Samsung S21", 9000, 100, 1),
                new Product("Gömlek", "LCW ", 750, 150, 2),
                new Product("Pantolon", "DeFacto", 1000, 150, 2)


                );

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();







        } 
    }
}
