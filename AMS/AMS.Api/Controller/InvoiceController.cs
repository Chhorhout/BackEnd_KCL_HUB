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
    public class InvoiceController : ControllerBase
    {
        private readonly ApplicationDbcontext _context;
        private readonly IMapper _mapper;
        private const int PageSize = 5;

        public InvoiceController(ApplicationDbcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceResponseDto>>> GetInvoices(
            int? page,
            string? searchTerm,
            string? searchBy = "number"
        )
        {
            int pageNumber = page ?? 1;
            var query = _context.Invoices.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                switch (searchBy.ToLower())
                {
                    case "number":
                        query = query.Where(i => i.Number.ToLower().Contains(searchTerm));
                        break;
                    case "description":
                        query = query.Where(i => i.Description.ToLower().Contains(searchTerm));
                        break;
                    case "totalamount":
                        query = query.Where(i => i.TotalAmount.ToLower().Contains(searchTerm));
                        break;
                    default:
                        query = query.Where(i => i.Number.ToLower().Contains(searchTerm) ||
                                               i.Description.ToLower().Contains(searchTerm) ||
                                               i.TotalAmount.ToLower().Contains(searchTerm));
                        break;
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            var invoices = await query
                .Skip((pageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            Response.Headers.Append("X-Total-Count", totalItems.ToString());
            Response.Headers.Append("X-Total-Pages", totalPages.ToString());
            Response.Headers.Append("X-Current-Page", pageNumber.ToString());
            Response.Headers.Append("X-Page-Size", PageSize.ToString());

            return Ok(_mapper.Map<IEnumerable<InvoiceResponseDto>>(invoices));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceResponseDto>> GetInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            return Ok(_mapper.Map<InvoiceResponseDto>(invoice));
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceResponseDto>> CreateInvoice(InvoiceCreateDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<InvoiceResponseDto>(invoice));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InvoiceResponseDto>> UpdateInvoice(Guid id, InvoiceCreateDto dto)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            _mapper.Map(dto, invoice);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<InvoiceResponseDto>(invoice));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
