using Server.DataAccess.Model;
using Server.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ForTestingPurposes
{
    public static class DataGenerator
    {
        public static void PopulateDbWithTestData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Company testCompany1 = new Company() { Name = "Company_One" };
                Company testCompany2 = new Company() { Name = "Company_Two" };
                Company testCompany3 = new Company() { Name = "Company_Three" };

                User user1 = new User() { Login = "user1", PasswordHash = "123", Company = testCompany1 };
                User user2 = new User() { Login = "user2", PasswordHash = "234", Company = testCompany2 };
                User user3 = new User() { Login = "user3", PasswordHash = "324", Company = testCompany2 };
                User user4 = new User() { Login = "user4", PasswordHash = "412", Company = testCompany3 };
                User user5 = new User() { Login = "user5", PasswordHash = "1244", Company = testCompany2 };

                db.Companies.AddRange(testCompany1, testCompany2, testCompany3);
                db.Users.AddRange(user1, user2, user3, user4, user5);
                db.SaveChanges();
            }
        }
    }
}
