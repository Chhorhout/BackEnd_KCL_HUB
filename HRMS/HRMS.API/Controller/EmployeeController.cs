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
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 5;

        public EmployeeController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetEmployees(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Employees.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                var searchByKey = (searchBy ?? "name").ToLower();
                switch (searchByKey)
                {
                    case "firstname":
                        query = query.Where(e => e.FirstName.ToLower().Contains(searchTerm));
                        break;
                    case "lastname":
                        query = query.Where(e => e.LastName.ToLower().Contains(searchTerm));
                        break;
                    case "email":
                        query = query.Where(e => e.Email.ToLower().Contains(searchTerm));
                        break;
                    case "phone":
                        query = query.Where(e => e.PhoneNumber.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(e => e.FirstName.ToLower().Contains(searchTerm) || 
                                            e.LastName.ToLower().Contains(searchTerm) ||
                                            e.Email.ToLower().Contains(searchTerm) ||
                                            e.PhoneNumber.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var employees = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<EmployeeResponseDto>>(employees));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDto = _mapper.Map<EmployeeResponseDto>(employee);
            return Ok(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == employeeDto.DepartmentId);
            if (!departmentExists)
            {
                return BadRequest($"Invalid DepartmentId: {employeeDto.DepartmentId}");
            }
            var employee = _mapper.Map<Employee>(employeeDto);
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            var employeeResponseDto = _mapper.Map<EmployeeResponseDto>(employee);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employeeResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeCreateDto employeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == employeeDto.DepartmentId);
            if (!departmentExists)
            {
                return BadRequest($"Invalid DepartmentId: {employeeDto.DepartmentId}");
            }
            _mapper.Map(employeeDto, employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employeeDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok("Employee deleted successfully");
        }
        
        
        
}
}
