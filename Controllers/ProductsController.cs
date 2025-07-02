using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BakeryAPI.Data;
using BakeryAPI.DTOs;
using BakeryAPI.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BakeryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDTO>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.OriginCountry)
                .Include(p => p.ProductIngredients)
                    .ThenInclude(pi => pi.Ingredient)
                .ToListAsync();

            var productDtos = products.Select(p => new ProductResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CountryName = p.OriginCountry.Name,
                Ingredients = p.ProductIngredients.Select(pi => pi.Ingredient.Name).ToList()
            }).ToList();

            return Ok(productDtos);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Recipe = dto.Recipe,
                Price = dto.Price,
                OriginCountryId = dto.OriginCountryId
            };

            // Добавляем связи с ингредиентами
            if (dto.IngredientIds != null && dto.IngredientIds.Count > 0)
            {
                product.ProductIngredients = dto.IngredientIds
                    .Select(id => new ProductIngredient { IngredientId = id })
                    .ToList();
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Возвращаем созданный продукт как DTO
            var responseDto = _mapper.Map<ProductResponseDTO>(product);

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, responseDto);
        }
    }
}
