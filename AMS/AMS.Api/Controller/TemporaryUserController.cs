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
    public class TemporaryUserController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public TemporaryUserController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemporaryUserResponseDto>>> GetTemporaryUsers(
        int? page,
        string? searchTerm,
        string? searchBy = "name"
    )
    {
        int pageNumber = page ?? 1;
        var query = _context.TemporaryUsers.AsNoTracking().Include(x => x.TemporaryUsedRecords).AsQueryable();
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
        .Select(x => _mapper.Map<TemporaryUserResponseDto>(x))
        .ToListAsync();
    Response.Headers.Append("X-Total-Count", totalItems.ToString());
    Response.Headers.Append("X-Total-Pages", totalPages.ToString());
    Response.Headers.Append("X-Current-Page", pageNumber.ToString());
    Response.Headers.Append("X-Page-Size", PageSize.ToString());
    return Ok(items);
}
[HttpGet("{id}")]
public async Task<ActionResult<TemporaryUserResponseDto>> GetTemporaryUser(Guid id)
{
    var temporaryUser = await _context.TemporaryUsers.Include(x => x.TemporaryUsedRecords).FirstOrDefaultAsync(x => x.Id == id);
    if (temporaryUser == null)
    {
        return NotFound();
    }
    return Ok(_mapper.Map<TemporaryUserResponseDto>(temporaryUser));
}
[HttpPost]
public async Task<ActionResult<TemporaryUserResponseDto>> CreateTemporaryUser(TemporaryUserCreateDto temporaryUserDto)
{
    var temporaryUser = _mapper.Map<TemporaryUser>(temporaryUserDto);
    await _context.TemporaryUsers.AddAsync(temporaryUser);
    await _context.SaveChangesAsync();
    return Ok(_mapper.Map<TemporaryUserResponseDto>(temporaryUser));
}
[HttpPut("{id}")]
public async Task<ActionResult<TemporaryUserResponseDto>> UpdateTemporaryUser(Guid id, TemporaryUserCreateDto temporaryUserDto)
{
    var temporaryUser = await _context.TemporaryUsers.Include(x => x.TemporaryUsedRecords).FirstOrDefaultAsync(x => x.Id == id);
    if (temporaryUser == null)
    {
        return NotFound();
    }
    _mapper.Map(temporaryUserDto, temporaryUser);
    await _context.SaveChangesAsync();
    return Ok(_mapper.Map<TemporaryUserResponseDto>(temporaryUser));
}
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteTemporaryUser(Guid id)
{
    var temporaryUser = await _context.TemporaryUsers.Include(x => x.TemporaryUsedRecords).FirstOrDefaultAsync(x => x.Id == id);
    if (temporaryUser == null)
    {
        return NotFound();
    }
    _context.TemporaryUsers.Remove(temporaryUser);
    await _context.SaveChangesAsync();
    return NoContent();
}
}
}
