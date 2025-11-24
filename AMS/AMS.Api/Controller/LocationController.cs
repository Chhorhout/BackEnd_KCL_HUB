using Microsoft.AspNetCore.Mvc;
using AMS.Api.Data;
using AMS.Api.Models;
using AMS.Api.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AMS.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;

        public LocationController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationResponseDto>>> GetLocations(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Locations.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(l => l.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(l => l.Name.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var locations = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<LocationResponseDto>>(locations));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LocationResponseDto>(location));
        }
        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationCreateDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, _mapper.Map<LocationResponseDto>(location));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(Guid id, LocationCreateDto locationDto)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            _mapper.Map(locationDto, location);
            await _context.SaveChangesAsync();
            return Ok(locationDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return Ok(location);
        }
    }
}