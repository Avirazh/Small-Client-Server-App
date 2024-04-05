using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTSimTestTaskServerApplication.Models;
using RTSimTestTaskServerApplication.Models.DataAccess;

namespace RTSimTestTaskServerApplication.Controllers
{
    public class CompanyController : Controller
    {
        private ApplicationContext _dbContext;

        public CompanyController(ApplicationContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Company> companies = await _dbContext.Companies.ToListAsync();

            return Json(companies);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersForCompany([FromQuery] int companyId)
        {
            var users = await _dbContext.Users.Where(c => c.Company.Id == companyId).ToListAsync();

            return Ok(users);
        }
    }
}