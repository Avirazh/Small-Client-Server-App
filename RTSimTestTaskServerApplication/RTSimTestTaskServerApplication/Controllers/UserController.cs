using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTSimTestTaskServerApplication.Models;
using RTSimTestTaskServerApplication.Models.DataAccess;

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

            User userToAdd = new ()
            {
                Login = user.Login,
                PasswordHash = user.PasswordHash,
                Company = _dbContext.Companies.FirstOrDefault(x => x.Id == user.Company.Id),
            };
           
            _dbContext.Users.Add(userToAdd);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            throw new NotImplementedException();
        }
    }
}