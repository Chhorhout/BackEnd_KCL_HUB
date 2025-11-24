using AMS.Api.Data;
using AMS.Api.Dtos;
using AMS.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AMS.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceRecordController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public MaintenanceRecordController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceRecordResponseDto>>> GetAllMaintenanceRecords(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.MaintenanceRecords
                .Include(mr => mr.Asset)
                .Include(mr => mr.Maintainer)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(mr => mr.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(mr => mr.Name.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var maintenanceRecords = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<MaintenanceRecordResponseDto>>(maintenanceRecords));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceRecordResponseDto>> GetMaintenanceRecordById(Guid id)
        {
            var maintenanceRecord = await _context.MaintenanceRecords
                .Include(mr => mr.Asset)
                .Include(mr => mr.Maintainer)
                .FirstOrDefaultAsync(mr => mr.Id == id);
            
            if (maintenanceRecord == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<MaintenanceRecordResponseDto>(maintenanceRecord));
        }

        [HttpPost]
        public async Task<ActionResult<MaintenanceRecordResponseDto>> CreateMaintenanceRecord(MaintenanceRecordCreateDto maintenanceRecordDto)
        {
            var maintenanceRecord = _mapper.Map<MaintenanceRecord>(maintenanceRecordDto);
            _context.MaintenanceRecords.Add(maintenanceRecord);
            await _context.SaveChangesAsync();
            
            // Reload the entity with related data for the response
            var createdRecord = await _context.MaintenanceRecords
                .Include(mr => mr.Asset)
                .Include(mr => mr.Maintainer)
                .FirstOrDefaultAsync(mr => mr.Id == maintenanceRecord.Id);
            
            return CreatedAtAction(nameof(GetMaintenanceRecordById), new { id = maintenanceRecord.Id }, _mapper.Map<MaintenanceRecordResponseDto>(createdRecord));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenanceRecord(Guid id, MaintenanceRecordCreateDto maintenanceRecordDto)
        {
            var maintenanceRecord = await _context.MaintenanceRecords.FindAsync(id);
            if (maintenanceRecord == null)
            {
                return NotFound();
            }
            
            _mapper.Map(maintenanceRecordDto, maintenanceRecord);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceRecord(Guid id)
        {
            var maintenanceRecord = await _context.MaintenanceRecords.FindAsync(id);
            if (maintenanceRecord == null)
            {
                return NotFound();
            }
            _context.MaintenanceRecords.Remove(maintenanceRecord);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}