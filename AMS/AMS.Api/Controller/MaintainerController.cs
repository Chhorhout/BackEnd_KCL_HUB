using Microsoft.AspNetCore.Mvc;
using AMS.Api.Dtos;
using AMS.Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AMS.Api.Data;
namespace AMS.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintainerController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public MaintainerController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintainerResponseDto>>> GetMaintainers(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Maintainers.Include(m => m.MaintainerType).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(m => m.Name.ToLower().Contains(searchTerm));
                        break;
                    case "email":
                        query = query.Where(m => m.Email.ToLower().Contains(searchTerm));
                        break;
                    case "phone":
                        query = query.Where(m => m.Phone.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(m => m.Name.ToLower().Contains(searchTerm) ||
                                    m.Email.ToLower().Contains(searchTerm) ||
                                    m.Phone.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var maintainers = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<MaintainerResponseDto>>(maintainers));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MaintainerResponseDto>> GetMaintainer(Guid id)
        {
            var maintainer = await _context.Maintainers.Include(m => m.MaintainerType).FirstOrDefaultAsync(m => m.Id == id);
            if (maintainer == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MaintainerResponseDto>(maintainer));
        }
        [HttpPost]
        public async Task<ActionResult<MaintainerResponseDto>> CreateMaintainer(MaintainerCreateDto dto)
        {
            var maintainer = _mapper.Map<Maintainer>(dto);
            await _context.Maintainers.AddAsync(maintainer);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<MaintainerResponseDto>(maintainer));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintainer(Guid id, MaintainerCreateDto dto)
        {
            var maintainer = await _context.Maintainers.FindAsync(id);
            if (maintainer == null)
            {
                return NotFound();
            }
            _mapper.Map(dto, maintainer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintainer(Guid id)
        {
            var maintainer = await _context.Maintainers.FindAsync(id);
            if (maintainer == null)
            {
                return NotFound();
            }
            _context.Maintainers.Remove(maintainer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
