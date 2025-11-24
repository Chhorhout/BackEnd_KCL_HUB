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
    public class MaintainerTypeController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;
        

        public MaintainerTypeController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintainerTypeResponseDto>>> GetMaintainerTypes(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.MaintainerTypes.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(m => m.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(m => m.Name.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var maintainerTypes = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<MaintainerTypeResponseDto>>(maintainerTypes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaintainerTypeResponseDto>> GetMaintainerType(Guid id)
        {
            var maintainerType = await _context.MaintainerTypes.FindAsync(id);
            if (maintainerType == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MaintainerTypeResponseDto>(maintainerType));
        }

        [HttpPost]
        public async Task<ActionResult<MaintainerTypeResponseDto>> CreateMaintainerType(MaintainerTypeCreateDto maintainerTypeDto)
        {
            var maintainerType = _mapper.Map<MaintainerType>(maintainerTypeDto);
            _context.MaintainerTypes.Add(maintainerType);
            await _context.SaveChangesAsync();

            var createdMaintainerType = _mapper.Map<MaintainerTypeResponseDto>(maintainerType);
            return CreatedAtAction(nameof(GetMaintainerType), new { id = maintainerType.Id }, createdMaintainerType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MaintainerTypeResponseDto>> UpdateMaintainerType(Guid id, MaintainerTypeCreateDto maintainerTypeDto)
        {
            var maintainerType = await _context.MaintainerTypes.FindAsync(id);
            if (maintainerType == null)
            {
                return NotFound();
            }

            _mapper.Map(maintainerTypeDto, maintainerType);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<MaintainerTypeResponseDto>(maintainerType));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintainerType(Guid id)
        {
            var maintainerType = await _context.MaintainerTypes.FindAsync(id);
            if (maintainerType == null)
            {
                return NotFound();
            }

            _context.MaintainerTypes.Remove(maintainerType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
