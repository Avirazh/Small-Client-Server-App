using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _dbContext.Users.Add(user);

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