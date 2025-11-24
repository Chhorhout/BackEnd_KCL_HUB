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
    public class AssetStatusController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public AssetStatusController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetStatusResponseDto>>> GetAssetStatuses(
            int? page,
            string? searchTerm,
            string? searchBy = "status"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.AssetStatuses
                .Include(a => a.Asset)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "status":
                        query = query.Where(a => a.Status.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(a => a.Status.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var assetStatuses = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<AssetStatusResponseDto>>(assetStatuses));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetStatusResponseDto>> GetAssetStatus(Guid id)
        {
            var assetStatus = await _context.AssetStatuses.Include(a => a.Asset).FirstOrDefaultAsync(a => a.Id == id);
            if (assetStatus == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AssetStatusResponseDto>(assetStatus));
        }

        [HttpPost]
        public async Task<ActionResult<AssetStatusResponseDto>> CreateAssetStatus(AssetStatusCreateDto dto)
        {
            var assetStatus = _mapper.Map<AssetStatus>(dto);
            _context.AssetStatuses.Add(assetStatus);
            await _context.SaveChangesAsync();
            
            // Reload the entity with related Asset for the response
            var createdAssetStatus = await _context.AssetStatuses
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(a => a.Id == assetStatus.Id);
            
            return CreatedAtAction(nameof(GetAssetStatus), new { id = assetStatus.Id }, _mapper.Map<AssetStatusResponseDto>(createdAssetStatus));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetStatus(Guid id, AssetStatusCreateDto dto)
        {
            var assetStatus = await _context.AssetStatuses.Include(a => a.Asset).FirstOrDefaultAsync(a => a.Id == id);
            if (assetStatus == null)
            {
                return NotFound();
            }
            _mapper.Map(dto, assetStatus);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetStatus(Guid id)
        {
            var assetStatus = await _context.AssetStatuses.Include(a => a.Asset).FirstOrDefaultAsync(a => a.Id == id);
            if (assetStatus == null)
            {
                return NotFound();
            }
            _context.AssetStatuses.Remove(assetStatus);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
