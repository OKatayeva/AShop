using System;
using System.Linq;
using AShop_Models;
using AShop_Utility;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AShop_Data.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AshopDB _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(AshopDB context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                //check for unfinished migrations
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }


            if (!_roleManager.RoleExistsAsync(WC.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WC.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WC.CustomerRole)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FullName = "Administrator",
                PhoneNumber = "111111111111"
            }, "Admin123!").GetAwaiter().GetResult();

            ApplicationUser user = _context.ApplicationUser.FirstOrDefault(u => u.Email == "admin@admin.com");
            _userManager.AddToRoleAsync(user, WC.AdminRole).GetAwaiter().GetResult();


        }
    }

}
