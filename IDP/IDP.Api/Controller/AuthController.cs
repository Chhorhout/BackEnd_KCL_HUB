using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IDP.Api.Data;
using IDP.Api.Dtos;
using IDP.Api.Models;
using AutoMapper;

namespace IDP.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest(new { message = "Email already exists" });
            }

            // Check if role exists
            var role = await _context.Roles.FindAsync(registerDto.RoleId);
            if (role == null)
            {
                return BadRequest(new { message = "Invalid role ID" });
            }

            // Create new user (password will be hashed in service layer)
            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = registerDto.Password; // Temporary - should hash this

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Load user with role for response
            var createdUser = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            return Ok(_mapper.Map<UserResponseDto>(createdUser));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find user by email
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || user.PasswordHash != loginDto.Password)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(_mapper.Map<UserResponseDto>(user));
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<UserResponseDto>>(users));
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserResponseDto>(user));
        }
    }
}

