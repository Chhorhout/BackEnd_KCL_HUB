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
    public class MaintenacePartController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;
        

        public MaintenacePartController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenacePartResponseDto>>> GetMaintenaceParts(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.MaintenanceParts.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(m => m.Name.ToLower().Contains(searchTerm));
                        break;
                    case "description":
                        query = query.Where(m => m.Description.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(m => m.Name.ToLower().Contains(searchTerm) ||
                        m.Description.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var maintenaceParts = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<MaintenacePartResponseDto>>(maintenaceParts));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenacePartResponseDto>> GetMaintenacePart(Guid id)
        {
            var maintenacePart = await _context.MaintenanceParts.FindAsync(id);
            if (maintenacePart == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MaintenacePartResponseDto>(maintenacePart));
        }

        [HttpPost]
        public async Task<ActionResult<MaintenacePartResponseDto>> CreateMaintenacePart(MaintenacePartCreateDto maintenacePartDto)
        {
            // Validate MaintenanceRecord exists
            var maintenanceRecord = await _context.MaintenanceRecords.FindAsync(maintenacePartDto.MaintenanceRecordId);
            if (maintenanceRecord == null)
            {
                return BadRequest(new { message = "MaintenanceRecord not found" });
            }

            var maintenacePart = _mapper.Map<MaintenacePart>(maintenacePartDto);
            maintenacePart.MaintenaceRequestPartId = maintenacePartDto.MaintenanceRecordId;
            maintenacePart.MaintenanceRecord = maintenanceRecord;
            
            _context.MaintenanceParts.Add(maintenacePart);
            await _context.SaveChangesAsync();

            var createdMaintenacePart = _mapper.Map<MaintenacePartResponseDto>(maintenacePart);
            return CreatedAtAction(nameof(GetMaintenacePart), new { id = maintenacePart.Id }, createdMaintenacePart);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MaintenacePartResponseDto>> UpdateMaintenacePart(Guid id, MaintenacePartCreateDto maintenacePartDto)
        {
            var maintenacePart = await _context.MaintenanceParts.FindAsync(id);
            if (maintenacePart == null)
            {
                return NotFound();
            }

            // Validate MaintenanceRecord exists
            var maintenanceRecord = await _context.MaintenanceRecords.FindAsync(maintenacePartDto.MaintenanceRecordId);
            if (maintenanceRecord == null)
            {
                return BadRequest(new { message = "MaintenanceRecord not found" });
            }

            _mapper.Map(maintenacePartDto, maintenacePart);
            maintenacePart.MaintenaceRequestPartId = maintenacePartDto.MaintenanceRecordId;
            maintenacePart.MaintenanceRecord = maintenanceRecord;
            
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<MaintenacePartResponseDto>(maintenacePart));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenacePart(Guid id)
        {
            var maintenacePart = await _context.MaintenanceParts.FindAsync(id);
            if (maintenacePart == null)
            {
                return NotFound();
            }

            _context.MaintenanceParts.Remove(maintenacePart);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

