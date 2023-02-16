﻿using Microsoft.AspNetCore.Identity;
using Ruper.DAL.Constants;
using Ruper.DAL.Entities;

namespace Ruper.DAL.DataContext
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedEssentialsAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Administrator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));

            //Seed Default User
            var defaultUser = new ApplicationUser 
            { 
                UserName = Authorization.default_username,
                FirstName = Authorization.default_firstname,
                LastName = Authorization.default_lastname,
                Email = Authorization.default_email,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var users = userManager.Users.ToList();

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, Authorization.default_password);
                await userManager.AddToRoleAsync(defaultUser, Authorization.Roles.Administrator.ToString());
            }
        }
    }
}
