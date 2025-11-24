using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDP.Api.Data;
using IDP.Api.Dtos;
using AutoMapper;

namespace IDP.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RolesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponseDto>>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<RoleResponseDto>>(roles));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResponseDto>> GetRoleById(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            
            if (role == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RoleResponseDto>(role));
        }
    }
}

