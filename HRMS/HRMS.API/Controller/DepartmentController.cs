using Microsoft.AspNetCore.Mvc;
using HRMS.API.Models;
using HRMS.API.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HRMS.API.Dtos;
namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 5;
        public DepartmentController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentResponseDto>>> GetDepartments(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Departments.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(d => d.Name.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(d => d.Name.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var departments = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<DepartmentResponseDto>>(departments));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentResponseDto>> GetDepartment(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var departmentDto = _mapper.Map<DepartmentResponseDto>(department);
            return Ok(departmentDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentCreateDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var department = _mapper.Map<Department>(departmentDto);
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            var departmentResponseDto = _mapper.Map<DepartmentResponseDto>(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, departmentResponseDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, DepartmentCreateDto departmentDto)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _mapper.Map(departmentDto, department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, departmentDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return Ok("Department deleted successfully");
        }
    }
}
