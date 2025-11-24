using Microsoft.AspNetCore.Mvc;
using AMS.Api.Dtos;
using AMS.Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using AMS.Api.Data;

namespace AMS.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 10;
        

        public SuppliersController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierResponseDto>>> GetSuppliers(
            int? page,
            string? searchTerm,
            string? searchBy = "name"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Suppliers.AsNoTracking();  

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "name":
                        query = query.Where(s => s.Name.ToLower().Contains(searchTerm));
                        break;
                    case "email":
                        query = query.Where(s => s.Email.ToLower().Contains(searchTerm));
                        break;
                    case "phone":
                        query = query.Where(s => s.Phone.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(s => s.Name.ToLower().Contains(searchTerm) ||
                        s.Email.ToLower().Contains(searchTerm) ||
                        s.Phone.ToLower().Contains(searchTerm) ||
                        s.Address.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var suppliers = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<SupplierResponseDto>>(suppliers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierResponseDto>> GetSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SupplierResponseDto>(supplier));
        }

        [HttpPost]
        public async Task<ActionResult<SupplierResponseDto>> CreateSupplier(SupplierCreateDto supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            var createdSupplier = _mapper.Map<SupplierResponseDto>(supplier);
            return CreatedAtAction(nameof(GetSupplier), new { id = supplier.Id }, createdSupplier);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierResponseDto>> UpdateSupplier(Guid id, SupplierCreateDto supplierDto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _mapper.Map(supplierDto, supplier);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<SupplierResponseDto>(supplier));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

