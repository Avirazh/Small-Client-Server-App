using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTSimTestTaskServerApplication.Models;
using RTSimTestTaskServerApplication.Models.DataAccess;

namespace RTSimTestTaskServerApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(string contents)
        {
            /*_dbContext.Companies.Add( new Company() { Name = "Компания 1" });
            _dbContext.Companies.Add(new Company() { Name = "Компания 2" });
            _dbContext.Companies.Add(new Company() { Name = "Компания 3" });
            await _dbContext.SaveChangesAsync();*/

            List<Company> companies = await _dbContext.Companies.ToListAsync();

            return Json(companies);
        }

        [HttpGet("getusersforcompany")]
        public async Task<IActionResult> GetUsersForCompany([FromQuery] int companyId)
        {
            var users = await _dbContext.Users.Where(c => c.Company.Id == companyId).ToListAsync();

            return Ok(users);
        }
    }
}