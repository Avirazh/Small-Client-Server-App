using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RTSimTestTaskServerApplication.Models;
using RTSimTestTaskServerApplication.Models.DataAccess;
using RTSimTestTaskServerApplication.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RTSimTestTaskServerApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private ApplicationContext _dbContext;
        private IConfiguration _configuration;
       
        public UserController(ApplicationContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Users.ToListAsync());
        }

        [HttpGet("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var isLoginExisting = await _dbContext.Users.Where(u => u.Login == user.Login).AnyAsync();

            if (isLoginExisting) 
            {
                return BadRequest("user with that login already exists");
            }

            User userToAdd = new()
            {
                Login = user.Login,
                PasswordHash = PasswordHashService.HashPassword(user.PasswordHash),
                Company = _dbContext.Companies.Where(comp => comp.Id == user.Company.Id).First(),
                ProfileType = "Студент"
            };
           
            _dbContext.Users.Add(userToAdd);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            User userToLogin = new()
            {
                Login = user.Login,
                PasswordHash = PasswordHashService.HashPassword(user.PasswordHash),
            };

            User? userFromDb =  await _dbContext.Users.Where(u => u.Login == userToLogin.Login && u.PasswordHash == userToLogin.PasswordHash).FirstOrDefaultAsync();
            if(userFromDb != null)
            {
                user.ProfileType = userFromDb.ProfileType;
                var token = JwtService.CreateAccessToken(user, _configuration.GetSection("JwtSettings:Key").Value);
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}