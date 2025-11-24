using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AMS.Api.Dtos;
using AMS.Api.Models;
using AMS.Api.Data;
using Microsoft.EntityFrameworkCore;
namespace AMS.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTypeController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;
        public AssetTypeController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetTypeResponseDto>>> GetAssetTypes(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.AssetTypes.Include(a => a.Category).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            switch (searchBy?.ToLower())
            {
                case "name":
                default:
                    query = query.Where(a => a.Name.ToLower().Contains(searchTerm));
                    break;
            }
        }
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
        var assetTypes = await query
            .Skip((pageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
        Response.Headers.Append("X-Total-Count", totalItems.ToString());
        Response.Headers.Append("X-Total-Pages", totalPages.ToString());
        Response.Headers.Append("X-Current-Page", pageNumber.ToString());
        Response.Headers.Append("X-Page-Size", PageSize.ToString());
        var assetTypesDto = _mapper.Map<List<AssetTypeResponseDto>>(assetTypes);
        return Ok(assetTypesDto);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<AssetTypeResponseDto>> GetAssetType(Guid id)
    {
        var assetType = await _context.AssetTypes.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);
        if (assetType == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<AssetTypeResponseDto>(assetType));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAssetType(AssetTypeCreateDto assetTypeDto)
    {
            var assetType = _mapper.Map<AssetType>(assetTypeDto);
            await _context.AssetTypes.AddAsync(assetType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAssetType), new { id = assetType.Id }, assetTypeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetType(Guid id)
        {
            var assetType = await _context.AssetTypes.FindAsync(id);
            if (assetType == null)
            {
                return NotFound();
            }
            _context.AssetTypes.Remove(assetType);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}