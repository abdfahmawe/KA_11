using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Uitils
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context , RoleManager<IdentityRole> manager , UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            _roleManager = manager;
            this._userManager = _userManager;
        }
        public async Task DataSeedingAsync()
        {
            if( (await _context.Database.GetPendingMigrationsAsync()).Any())
            {
              await  _context.Database.MigrateAsync();
            }
             if (!await _context.Categories.AnyAsync())
            {
             await   _context.Categories.AddRangeAsync(new List<Category>
                {
                    new Category { Name = "Electronics" },
                    new Category { Name = "Clothing" },
                    new Category { Name = "Home Appliances"}
                });
               
            }
            if (!await _context.Brands.AnyAsync())
            {
              await  _context.Brands.AddRangeAsync(new List<Brand>
                {
                    new Brand { Name = "Adidas" },
                    new Brand { Name = "Nike" },
                    new Brand { Name = "Puma"}
                });

            }
          await  _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
               await _roleManager.CreateAsync(new IdentityRole() { Name="Admin"});
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Super_Admin" });

                await _roleManager.CreateAsync(new IdentityRole() { Name = "Customer" });

            }

            if (! await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    FullName = "Tariq shreem",
                    UserName = "tariqshreem",
                    City = "Qalqilia ",
                    Email = "Tariq_shreem@gmail.com",
                };
                var user2 = new ApplicationUser()
                {
                    FullName = "afnan shreem",
                    UserName = "afnanshreem",
                    City = "Qalqilia ",
                    Email = "afnan_shreem@gmail.com",
                };
                var user3 = new ApplicationUser()
                {
                    FullName = "abd fahmawe",
                    UserName = "abdfahmawe",
                    City = "Tulkarm ",
                    Email = "abd_fahmawe@gmail.com",
                };
                await _userManager.CreateAsync(user1);
                await _userManager.CreateAsync(user2);
                await _userManager.CreateAsync(user3);
                // assign roles to users
                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "Super_Admin");
                await _userManager.AddToRoleAsync(user3, "Customer");
            }
        await  _context.SaveChangesAsync();
        }
    }
}
