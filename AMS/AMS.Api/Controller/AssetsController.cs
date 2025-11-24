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
    public class AssetsController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public AssetsController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetResponseDto>>> GetAssets(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            if (pageNumber <= 0)
            {
                return BadRequest("Page number must be greater than 0");
            }

            var query = _context.Assets
                .Include(a => a.Supplier)
                .Include(a => a.Location)
                .Include(a => a.AssetType)
                .Include(a => a.Invoice)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy?.ToLower())
                {
                    case "name":
                        query = query.Where(a => a.Name.ToLower().Contains(searchTerm));
                        break;
                    case "serialnumber":
                        query = query.Where(a => a.SerialNumber.ToLower().Contains(searchTerm));
                        break;
                    case "warranty":
                        if (bool.TryParse(searchTerm, out bool hasWarranty))
                        {
                            query = query.Where(a => a.HasWarranty == hasWarranty);
                        }
                        break;
                    default:
                        query = query.Where(a => a.Name.ToLower().Contains(searchTerm) ||
                                        a.SerialNumber.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var assets = await query
                .OrderBy(a => a.Name)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<AssetResponseDto>>(assets));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssetResponseDto>> GetAsset(Guid id)
        {
            var asset = await _context.Assets
                .Include(a => a.Supplier)
                .Include(a => a.Location)
                .Include(a => a.AssetType)
                .Include(a => a.Invoice)
                .Include(a => a.AssetOwnerShips)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AssetResponseDto>(asset));
        }

        [HttpPost]
        public async Task<ActionResult<AssetResponseDto>> PostAsset(AssetCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Asset data is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asset = _mapper.Map<Asset>(dto);
            var supplier = await _context.Suppliers.FindAsync(dto.SupplierId);
            if (supplier == null)
            {
                return BadRequest("Supplier not found");
            }
            asset.Supplier = supplier;

            var location = await _context.Locations.FindAsync(dto.LocationId);
            if (location == null)
            {
                return BadRequest("Location not found");
            }
            asset.Location = location;

            var invoice = await _context.Invoices.FindAsync(dto.InvoiceId);
            if (invoice == null)
            {
                return BadRequest("Invoice not found");
            }
            asset.Invoice = invoice;

            var assetType = await _context.AssetTypes.FindAsync(dto.AssetTypeId);

            if (assetType == null)
            {
                return BadRequest("Asset type not found");
            }
            asset.AssetType = assetType;

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            // Reload with related entities for proper mapping
            await _context.Entry(asset)
                .Collection(a => a.AssetOwnerShips)
                .LoadAsync();
            await _context.Entry(asset)
                .Reference(a => a.Supplier)
                .LoadAsync();
            await _context.Entry(asset)
                .Reference(a => a.Location)
                .LoadAsync();
            await _context.Entry(asset)
                .Reference(a => a.AssetType)
                .LoadAsync();
            await _context.Entry(asset)
                .Reference(a => a.Invoice)
                .LoadAsync();

            return CreatedAtAction(
                nameof(GetAsset),
                new { id = asset.Id },
                _mapper.Map<AssetResponseDto>(asset));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(Guid id, AssetCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Asset data is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var asset = await _context.Assets
                .Include(a => a.Supplier)
                .Include(a => a.Location)
                .Include(a => a.AssetType)
                .Include(a => a.Invoice)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (asset == null)
            {
                return NotFound();
            }

            // Validate Supplier
            var supplier = await _context.Suppliers.FindAsync(dto.SupplierId);
            if (supplier == null)
            {
                return BadRequest("Supplier not found");
            }

            // Validate Location
            var location = await _context.Locations.FindAsync(dto.LocationId);
            if (location == null)
            {
                return BadRequest("Location not found");
            }

            // Validate Asset Type
            var assetType = await _context.AssetTypes.FindAsync(dto.AssetTypeId);
            if (assetType == null)
            {
                return BadRequest("Asset type not found");
            }

            // Validate Invoice
            var invoice = await _context.Invoices.FindAsync(dto.InvoiceId);
            if (invoice == null)
            {
                return BadRequest("Invoice not found");
            }

            _mapper.Map(dto, asset);
            asset.Supplier = supplier;
            asset.Location = location;
            asset.AssetType = assetType;
            asset.Invoice = invoice;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
