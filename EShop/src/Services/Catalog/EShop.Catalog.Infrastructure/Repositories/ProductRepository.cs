using EShop.Catalog.Application.Contracts;
using EShop.Catalog.Domain.Aggregates;
using EShop.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Infrastructure.Repositories
{
    public class ProductRepository(CatalogDbContext catalogDbContext) : IProductRepository
    {


        public async Task<Product> AddAsync(Product entity)
        {
            await catalogDbContext.Products.AddAsync(entity);
            await catalogDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await GetByIdAsync(id);
            catalogDbContext.Products.Remove(product);

            await catalogDbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync()
        {
           return await catalogDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await catalogDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            return product;



        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
           return await catalogDbContext.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchByNameAsync(string name)
        {
            return await catalogDbContext.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            catalogDbContext.Products.Update(entity);
            await catalogDbContext.SaveChangesAsync();
        }
    }
}
