using AutoMapper;
using BakeryAPI.Data;
using BakeryAPI.DTOs;
using BakeryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakeryAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllProducts()
        {
            var products = await _context.Products
                .Include(p => p.OriginCountry)
                .Include(p => p.Distributor)
                .Include(p => p.ProductIngredients)
                    .ThenInclude(pi => pi.Ingredient)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }

        public async Task<ProductResponseDTO> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.OriginCountry)
                .Include(p => p.Distributor)
                .Include(p => p.ProductIngredients)
                    .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return _mapper.Map<ProductResponseDTO>(product);
        }

        public async Task<ProductResponseDTO> CreateProduct(ProductCreateDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            if (productDto.IngredientIds?.Any() == true)
            {
                foreach (var ingredientId in productDto.IngredientIds)
                {
                    var pi = new ProductIngredient
                    {
                        ProductId = product.Id,
                        IngredientId = ingredientId,
                        Amount = 1,
                        Unit = "g"
                    };
                    _context.ProductIngredients.Add(pi);
                }
                await _context.SaveChangesAsync();
            }

            var productWithIngredients = await _context.Products
                .Include(p => p.OriginCountry)
                .Include(p => p.ProductIngredients)
                    .ThenInclude(pi => pi.Ingredient)
                .FirstOrDefaultAsync(p => p.Id == product.Id);

            return _mapper.Map<ProductResponseDTO>(productWithIngredients);
        }

        public async Task UpdateProduct(int id, ProductUpdateDTO productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            _mapper.Map(productDto, product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
