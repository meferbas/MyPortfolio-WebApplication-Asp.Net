using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MyPortfolio.DAL
{
    public class EfSeedData 
    {
        private readonly MyPortfolioContext _context;
         
        public EfSeedData(MyPortfolioContext context)
        {
            _context = context;
        }

        public void CreateAdminUser()
        {
            var adminNick = "*******";
            var adminPassword = HashPassword("********");
            var existingAdmin = _context.Admins.FirstOrDefault(u => u.NickName == adminNick);
            if (existingAdmin == null)
            {
                var adminUser = new Admin
                {
                    NickName = adminNick,
                    Password = adminPassword,
                    Role = "admin"
                };

                _context.Admins.Add(adminUser);
                _context.SaveChanges();
            }
        }
        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }

    }
}
