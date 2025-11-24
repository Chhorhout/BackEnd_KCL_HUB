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
    public class TemporaryUsedRecordsController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;
    
        public TemporaryUsedRecordsController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemporaryUsedRecordResponseDto>>> GetTemporaryUsedRecords(
        int? page,
        string? searchTerm,
        string? searchBy = "name"
    )
    {
        int pageNumber = page ?? 1;
    
    
        var query = _context.TemporaryUsedRecords.AsNoTracking()
            .Include(x => x.Asset)
            .Include(x => x.TemporaryUser)
            .Include(x => x.TemporaryUsedRequests)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            switch (searchBy?.ToLower())
            {
                case "name":
                    query = query.Where(x => x.Name.ToLower().Contains(searchTerm));
                    break;
                default:
                    query = query.Where(x => x.Name.ToLower().Contains(searchTerm));
                    break;
            }
        }
    
    var totalItems = await query.CountAsync();
    var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
    var items = await query
        .OrderBy(x => x.Name)
        .Skip((pageNumber - 1) * PageSize)
        .Take(PageSize)
        .Select(x => _mapper.Map<TemporaryUsedRecordResponseDto>(x))
        .ToListAsync();
    Response.Headers.Append("X-Total-Count", totalItems.ToString());
    Response.Headers.Append("X-Total-Pages", totalPages.ToString());
    Response.Headers.Append("X-Current-Page", pageNumber.ToString());
    Response.Headers.Append("X-Page-Size", PageSize.ToString());
    return Ok(items);
}
[HttpGet("{id}")]
public async Task<ActionResult<TemporaryUsedRecordResponseDto>> GetTemporaryUsedRecord(Guid id)
{
    var temporaryUsedRecord = await _context.TemporaryUsedRecords
        .Include(x => x.Asset)
        .Include(x => x.TemporaryUser)
        .Include(x => x.TemporaryUsedRequests)
        .FirstOrDefaultAsync(x => x.Id == id);
    if (temporaryUsedRecord == null)
    {
        return NotFound();
    }
    return Ok(_mapper.Map<TemporaryUsedRecordResponseDto>(temporaryUsedRecord));
}
[HttpPost]
public async Task<ActionResult<TemporaryUsedRecordResponseDto>> CreateTemporaryUsedRecord(TemporaryUsedRecordCreateDto temporaryUsedRecordDto)
{
    // Validate Asset exists
    var asset = await _context.Assets.FindAsync(temporaryUsedRecordDto.AssetId);
    if (asset == null)
    {
        return BadRequest(new { message = "Asset not found" });
    }

    // Validate TemporaryUser exists
    var temporaryUser = await _context.TemporaryUsers.FindAsync(temporaryUsedRecordDto.TemporaryUserId);
    if (temporaryUser == null)
    {
        return BadRequest(new { message = "TemporaryUser not found" });
    }

    var temporaryUsedRecord = _mapper.Map<TemporaryUsedRecord>(temporaryUsedRecordDto);
    temporaryUsedRecord.Asset = asset;
    temporaryUsedRecord.TemporaryUser = temporaryUser;
    
    await _context.TemporaryUsedRecords.AddAsync(temporaryUsedRecord);
    await _context.SaveChangesAsync();

    // Reload with Asset and TemporaryUser included
    var createdRecord = await _context.TemporaryUsedRecords
        .Include(x => x.Asset)
        .Include(x => x.TemporaryUser)
        .FirstOrDefaultAsync(x => x.Id == temporaryUsedRecord.Id);
    
    return Ok(_mapper.Map<TemporaryUsedRecordResponseDto>(createdRecord));
}
[HttpPut("{id}")]
public async Task<ActionResult<TemporaryUsedRecordResponseDto>> UpdateTemporaryUsedRecord(Guid id, TemporaryUsedRecordCreateDto temporaryUsedRecordDto)
{
    var temporaryUsedRecord = await _context.TemporaryUsedRecords
        .Include(x => x.Asset)
        .Include(x => x.TemporaryUser)
        .Include(x => x.TemporaryUsedRequests)
        .FirstOrDefaultAsync(x => x.Id == id);
    if (temporaryUsedRecord == null)
    {
        return NotFound();
    }

    // Validate Asset exists
    var asset = await _context.Assets.FindAsync(temporaryUsedRecordDto.AssetId);
    if (asset == null)
    {
        return BadRequest(new { message = "Asset not found" });
    }

    // Validate TemporaryUser exists
    var temporaryUser = await _context.TemporaryUsers.FindAsync(temporaryUsedRecordDto.TemporaryUserId);
    if (temporaryUser == null)
    {
        return BadRequest(new { message = "TemporaryUser not found" });
    }

    _mapper.Map(temporaryUsedRecordDto, temporaryUsedRecord);
    temporaryUsedRecord.Asset = asset;
    temporaryUsedRecord.TemporaryUser = temporaryUser;
    
    await _context.SaveChangesAsync();
    return Ok(_mapper.Map<TemporaryUsedRecordResponseDto>(temporaryUsedRecord));
}   

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTemporaryUsedRecord(Guid id)
{
    var temporaryUsedRecord = await _context.TemporaryUsedRecords
        .Include(x => x.TemporaryUsedRequests)
        .FirstOrDefaultAsync(x => x.Id == id);
    if (temporaryUsedRecord == null)
    {
        return NotFound();
    }

    // Delete related TemporaryUsedRequests first
    if (temporaryUsedRecord.TemporaryUsedRequests != null && temporaryUsedRecord.TemporaryUsedRequests.Any())
    {
        _context.TemporaryUsedRequests.RemoveRange(temporaryUsedRecord.TemporaryUsedRequests);
    }

    _context.TemporaryUsedRecords.Remove(temporaryUsedRecord);
    await _context.SaveChangesAsync();
    return NoContent();
}
}
}