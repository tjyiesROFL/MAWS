using MAWS.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace MAWS.Services
{

    /// <summary>
    /// 
    /// Class to initalise AspNetRoles, Admin AspNetUsers, AuditTriggers
    /// 
    /// Source: http://www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx
    /// 
    /// Jack
    /// </summary>

    public class UserDataInitializer
    {
        public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<UserRole> roleManager)
        {

            SeedRoles(roleManager);
            await SeedUsers(userManager);

        }

        public static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "administrator@MAWS.com",
                    FullName = "Charlie Sheen",
                    RoleTitle = "Administrator"
                };

                IdentityResult result = userManager.CreateAsync
                (user, "#DontLook11").Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                    await userManager.AddToRoleAsync(user, "Contracts Administrator");
                    await userManager.AddToRoleAsync(user, "Head Of Discipline");
                    await userManager.AddToRoleAsync(user, "Unit Coordinator");
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }

            if (userManager.FindByNameAsync("contracts_admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "contracts_admin",
                    Email = "contracts_admin@maws.com",
                    FullName = "Toad Stool",
                    RoleTitle = "Contracts Administrator"
                };

                IdentityResult result = userManager.CreateAsync
                (user, "#Winning111").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Contracts Administrator").Wait();
                }
            }

            //if (userManager.FindByNameAsync("head_of_discipline").Result == null)
            //{
            //    ApplicationUser user = new ApplicationUser
            //    {
            //        UserName = "head_of_discipline",
            //        Email = "head_of_discipline@maws.com",
            //        FullName = "Dark Horse",
            //        RoleTitle = "Head Of Discipline"
            //    };

            //    IdentityResult result = userManager.CreateAsync
            //    (user, "#DontLook11").Result;

            //    if (result.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user, "Head Of Discipline").Wait();
            //    }
            //}

            //if (userManager.FindByNameAsync("unit_coordinator").Result == null)
            //{
            //    ApplicationUser user = new ApplicationUser
            //    {
            //        UserName = "head_of_discipline",
            //        Email = "head_of_discipline@maws.com",
            //        FullName = "Dark Horse",
            //        RoleTitle = "Head Of Discipline"
            //    };

            //    IdentityResult result = userManager.CreateAsync
            //    (user, "#DontLook11").Result;

            //    if (result.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user, "Head Of Discipline").Wait();
            //    }
            //}
        }

        public static void SeedRoles(RoleManager<UserRole> roleManager)
        {

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                UserRole role = new UserRole
                {
                    Name = "Administrator",
                    Description = "Administrator Role"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                if (roleResult.Succeeded) { Console.WriteLine("Administrator Role Successfully Created"); }
            }

            if (!roleManager.RoleExistsAsync("Contracts Administrator").Result)
            {
                UserRole role = new UserRole
                {
                    Name = "Contracts Administrator",
                    Description = "Contracts Administrator Role"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                if (roleResult.Succeeded) { Console.WriteLine("Contracts Administrator Role Successfully Created"); }
            }

            if (!roleManager.RoleExistsAsync("Head Of Discipline").Result)
            {
                UserRole role = new UserRole
                {
                    Name = "Head Of Discipline",
                    Description = "Head Of Discipline Role"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                if (roleResult.Succeeded) { Console.WriteLine("Head Of Discipline Role Successfully Created"); }
            }

            if (!roleManager.RoleExistsAsync("Unit Coordinator").Result)
            {
                UserRole role = new UserRole
                {
                    Name = "Unit Coordinator",
                    Description = "Unit Coordinator Role"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                if (roleResult.Succeeded) { Console.WriteLine("Unit Coordinator Role Successfully Created"); }
            }

            if (!roleManager.RoleExistsAsync("Academic Staff").Result)
            {
                UserRole role = new UserRole
                {
                    Name = "Academic Staff",
                    Description = "Academic Staff Role"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                if (roleResult.Succeeded) { Console.WriteLine("Academic Staff Role Successfully Created"); }
            }
        }
    }
}
