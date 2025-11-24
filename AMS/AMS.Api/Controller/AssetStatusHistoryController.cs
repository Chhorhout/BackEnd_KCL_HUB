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
    public class AssetStatusHistoryController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;
        

        public AssetStatusHistoryController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetStatusHistoryResponseDto>>> GetAssetStatusHistories(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.AssetStatusHistories.Include(a => a.Asset).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(a => a.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(a => a.Name.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var assetStatusHistories = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<AssetStatusHistoryResponseDto>>(assetStatusHistories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetStatusHistoryResponseDto>> GetAssetStatusHistory(Guid id)
        {
            var assetStatusHistory = await _context.AssetStatusHistories.Include(a => a.Asset).FirstOrDefaultAsync(a => a.Id == id);
            if (assetStatusHistory == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AssetStatusHistoryResponseDto>(assetStatusHistory));
        }

        [HttpPost]
        public async Task<ActionResult<AssetStatusHistoryResponseDto>> CreateAssetStatusHistory(AssetStatusHistoryCreateDto assetStatusHistoryDto)
        {
            var assetStatusHistory = _mapper.Map<AssetStatusHistory>(assetStatusHistoryDto);
            _context.AssetStatusHistories.Add(assetStatusHistory);
            await _context.SaveChangesAsync();

            // Reload the entity with related Asset for the response
            var createdAssetStatusHistory = await _context.AssetStatusHistories
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(a => a.Id == assetStatusHistory.Id);

            return CreatedAtAction(nameof(GetAssetStatusHistory), new { id = assetStatusHistory.Id }, _mapper.Map<AssetStatusHistoryResponseDto>(createdAssetStatusHistory));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AssetStatusHistoryResponseDto>> UpdateAssetStatusHistory(Guid id, AssetStatusHistoryCreateDto assetStatusHistoryDto)
        {
            var assetStatusHistory = await _context.AssetStatusHistories.Include(a => a.Asset).FirstOrDefaultAsync(a => a.Id == id);
            if (assetStatusHistory == null)
            {
                return NotFound();
            }

            _mapper.Map(assetStatusHistoryDto, assetStatusHistory);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<AssetStatusHistoryResponseDto>(assetStatusHistory));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetStatusHistory(Guid id)
        {
            var assetStatusHistory = await _context.AssetStatusHistories.FindAsync(id);
            if (assetStatusHistory == null)
            {
                return NotFound();
            }

            _context.AssetStatusHistories.Remove(assetStatusHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
