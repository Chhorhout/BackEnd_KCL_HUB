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
    public class OwnerTypeController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public OwnerTypeController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        [HttpGet]
    public async Task<ActionResult<IEnumerable<OwnerTypeResponseDto>>> GetOwnerTypes(
        int? page,
        string? searchTerm,
        string? searchBy = "name"
    )
    {
        int pageNumber = page ?? 1;
        var query = _context.OwnerTypes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            switch (searchBy?.ToLower())
            {
                case "name":
                default:
                    query = query.Where(o => o.Name.ToLower().Contains(searchTerm));
                    break;
            }
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

        var ownerTypes = await query
            .Skip((pageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        Response.Headers.Append("X-Total-Count", totalItems.ToString());
        Response.Headers.Append("X-Total-Pages", totalPages.ToString());
        Response.Headers.Append("X-Current-Page", pageNumber.ToString());
        Response.Headers.Append("X-Page-Size", PageSize.ToString());

        var ownerTypesDto = _mapper.Map<List<OwnerTypeResponseDto>>(ownerTypes);
        return Ok(ownerTypesDto);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOwnerTypeById(Guid id)
    {
        var ownerType = await _context.OwnerTypes.FindAsync(id);
        if (ownerType == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<OwnerTypeResponseDto>(ownerType));
    }
    [HttpPost]
    public async Task<IActionResult> CreateOwnerType(OwnerTypeCreateDto ownerTypeDto)
    {
        var ownerType = _mapper.Map<OwnerType>(ownerTypeDto);
        await _context.OwnerTypes.AddAsync(ownerType);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOwnerTypeById), new { id = ownerType.Id }, ownerTypeDto);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOwnerType(Guid id, OwnerTypeCreateDto ownerTypeDto)
    {
        var ownerType = await _context.OwnerTypes.FindAsync(id);
        if (ownerType == null)
        {
            return NotFound();
        }
        _mapper.Map(ownerTypeDto, ownerType);
        await _context.SaveChangesAsync();
        return Ok(ownerTypeDto);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOwnerType(Guid id)
    {
        var ownerType = await _context.OwnerTypes.FindAsync(id);
        if (ownerType == null)
        {
            return NotFound();
        }
        _context.OwnerTypes.Remove(ownerType);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
}