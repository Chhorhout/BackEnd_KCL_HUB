using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AMS.Api.Data;
using AMS.Api.Models;
using AMS.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AMS.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public CategoriesController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetCategories(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Categories.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(c => c.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(c => c.Name.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var categories = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<CategoryResponseDto>>(categories));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriesById(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<CategoryResponseDto>(category);
            return Ok(categoryDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            var createdCategoryDto = _mapper.Map<CategoryResponseDto>(category);
            return CreatedAtAction(nameof(GetCategoriesById), new { id = category.Id }, createdCategoryDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryCreateDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            _mapper.Map(categoryDto, category);
            await _context.SaveChangesAsync();
            return Ok(categoryDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
        

    }
}