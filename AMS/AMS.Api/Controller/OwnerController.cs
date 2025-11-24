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
    public class OwnerController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 5;

        public OwnerController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerResponseDto>>> GetOwners(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Owners.Include(o => o.OwnerType).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(o => o.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(o => o.Name.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var owners = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            var ownersDto = _mapper.Map<List<OwnerResponseDto>>(owners);
            return Ok(ownersDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var owner = await _context.Owners.Include(o => o.OwnerType).FirstOrDefaultAsync(o => o.Id == id);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OwnerResponseDto>(owner));
        }
        [HttpPost]
        public async Task<ActionResult<OwnerResponseDto>> CreateOwner(OwnerCreateDto ownerDto)
        {
            var owner = _mapper.Map<Owner>(ownerDto);
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
            
            // Reload with related entity for proper mapping
            await _context.Entry(owner).Reference(o => o.OwnerType).LoadAsync();
            
            return CreatedAtAction(nameof(GetOwnerById), new { id = owner.Id }, _mapper.Map<OwnerResponseDto>(owner));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, OwnerCreateDto ownerDto)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            
            _mapper.Map(ownerDto, owner);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}