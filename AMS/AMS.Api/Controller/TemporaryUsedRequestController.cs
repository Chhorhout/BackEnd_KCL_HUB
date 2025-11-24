using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AMS.Api.Data;
using AMS.Api.Dtos;
using AMS.Api.Models;

namespace AMS.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporaryUsedRequestController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private const int PageSize = 10;

        public TemporaryUsedRequestController(ApplicationDbcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemporaryUsedRequestResponseDto>>> GetTemporaryUsedRequests(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.TemporaryUsedRequests
                .AsNoTracking()
                .Include(x => x.TemporaryUsedRecord)
                .Include(x => x.Asset)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy?.ToLower())
                {
                    case "name":
                        query = query.Where(x => x.Name.ToLower().Contains(searchTerm));
                        break;
                    case "record":
                    case "temporaryusedrecordname":
                        query = query.Where(x => x.TemporaryUsedRecord.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(x =>
                            x.Name.ToLower().Contains(searchTerm) ||
                            (x.Description != null && x.Description.ToLower().Contains(searchTerm)) ||
                            x.TemporaryUsedRecord.Name.ToLower().Contains(searchTerm)
                        );
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var items = await query
                .OrderBy(x => x.Name)
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .Select(x => new TemporaryUsedRequestResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    TemporaryUsedRecordName = x.TemporaryUsedRecord.Name,
                    AssetName = x.Asset != null ? x.Asset.Name : string.Empty
                })
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemporaryUsedRequestResponseDto>> GetTemporaryUsedRequest(Guid id)
        {
            var entity = await _context.TemporaryUsedRequests
                .AsNoTracking()
                .Include(x => x.TemporaryUsedRecord)
                .Include(x => x.Asset)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = new TemporaryUsedRequestResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                TemporaryUsedRecordName = entity.TemporaryUsedRecord.Name,
                AssetName = entity.Asset != null ? entity.Asset.Name : string.Empty
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<TemporaryUsedRequestResponseDto>> CreateTemporaryUsedRequest(TemporaryUsedRequestCreateDto dto)
        {
            // Validate TemporaryUsedRecord exists
            var temporaryUsedRecord = await _context.TemporaryUsedRecords.FindAsync(dto.TemporaryUsedRecordId);
            if (temporaryUsedRecord == null)
            {
                return BadRequest(new { message = "TemporaryUsedRecord not found" });
            }

            // Validate Asset exists
            var asset = await _context.Assets.FindAsync(dto.AssetId);
            if (asset == null)
            {
                return BadRequest(new { message = "Asset not found" });
            }

            var entity = new TemporaryUsedRequest
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                TemporaryUsedRecordId = dto.TemporaryUsedRecordId,
                AssetId = dto.AssetId
            };

            _context.TemporaryUsedRequests.Add(entity);
            await _context.SaveChangesAsync();

            var response = await _context.TemporaryUsedRequests
                .AsNoTracking()
                .Include(x => x.TemporaryUsedRecord)
                .Include(x => x.Asset)
                .Where(x => x.Id == entity.Id)
                .Select(x => new TemporaryUsedRequestResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    TemporaryUsedRecordName = x.TemporaryUsedRecord.Name,
                    AssetName = x.Asset != null ? x.Asset.Name : string.Empty
                })
                .FirstAsync();

            return CreatedAtAction(nameof(GetTemporaryUsedRequest), new { id = entity.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TemporaryUsedRequestResponseDto>> UpdateTemporaryUsedRequest(Guid id, TemporaryUsedRequestCreateDto dto)
        {
            var entity = await _context.TemporaryUsedRequests.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            // Validate TemporaryUsedRecord exists
            var temporaryUsedRecord = await _context.TemporaryUsedRecords.FindAsync(dto.TemporaryUsedRecordId);
            if (temporaryUsedRecord == null)
            {
                return BadRequest(new { message = "TemporaryUsedRecord not found" });
            }

            // Validate Asset exists
            var asset = await _context.Assets.FindAsync(dto.AssetId);
            if (asset == null)
            {
                return BadRequest(new { message = "Asset not found" });
            }

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.TemporaryUsedRecordId = dto.TemporaryUsedRecordId;
            entity.AssetId = dto.AssetId;
            await _context.SaveChangesAsync();

            var response = await _context.TemporaryUsedRequests
                .AsNoTracking()
                .Include(x => x.TemporaryUsedRecord)
                .Include(x => x.Asset)
                .Where(x => x.Id == entity.Id)
                .Select(x => new TemporaryUsedRequestResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    TemporaryUsedRecordName = x.TemporaryUsedRecord.Name,
                    AssetName = x.Asset != null ? x.Asset.Name : string.Empty
                })
                .FirstAsync();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemporaryUsedRequest(Guid id)
        {
            var entity = await _context.TemporaryUsedRequests.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.TemporaryUsedRequests.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
