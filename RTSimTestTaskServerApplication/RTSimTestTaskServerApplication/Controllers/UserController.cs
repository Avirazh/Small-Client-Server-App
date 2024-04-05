using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTSimTestTaskServerApplication.Models;
using RTSimTestTaskServerApplication.Models.DataAccess;
using RTSimTestTaskServerApplication.Services;

namespace RTSimTestTaskServerApplication.Controllers
{
    public class UserController : Controller
    {
        private ApplicationContext _dbContext;

        public UserController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Users.ToListAsync());
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
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
            };
           
            _dbContext.Users.Add(userToAdd);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            User userToLogin = new()
            {
                Login = user.Login,
                PasswordHash = PasswordHashService.HashPassword(user.PasswordHash),
            };

            bool checkIfExists =  await _dbContext.Users.Where(u => u.Login == userToLogin.Login && u.PasswordHash == userToLogin.PasswordHash).AnyAsync();
            
            return checkIfExists? Ok() : BadRequest();
        }
    }
}