
using System;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using AirPortWebApi.Data.Application;
using AirPortWebApi.Data.DbContext.Identity;
using AirPortWebApi.Data.DbContext.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AirPortWebApi.Data.Application
{
    public sealed class ApplicationDbContextConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public ApplicationDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = @"ApplicationDbContextMigrations";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            try
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

                IdentityResult result = null;

                var user = userManager.FindByName("admin");
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "SomeEmail@gmail.com",
                        EmailConfirmed = true,
                        JoinDate = DateTime.Now.AddYears(-3)
                    };

                    var clients = new[]
                    {
                        new UserClient
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Javascript",
                            IsActive = true,
                            AllowedOrigin = "*",
                            RefreshTokenLifeTime = (int) TimeSpan.FromMinutes(15).TotalSeconds,
                            UserId = user.Id
                        },
                        new UserClient
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "WP8.1",
                            IsActive = true,
                            AllowedOrigin = "*",
                            RefreshTokenLifeTime = (int) TimeSpan.FromMinutes(15).TotalSeconds,
                            Secret = "secret",
                            UserId = user.Id
                        }
                    };
                    user.UserClients = clients;
                    context.UserClients.AddRange(clients);
                    result = userManager.Create(user, "GVtYXMubWljc123!");
                }

                var role = roleManager.FindByName("Admin");
                if (result != null && result.Succeeded)
                {
                    if (role == null)
                    {
                        role = new ApplicationRole("Admin");
                        roleManager.Create(role);

                        if (!userManager.IsInRole(user.Id, "Admin"))
                        {
                            userManager.AddToRole(user.Id, role.Name);
                        }
                    }
                }

#if DEBUG
                user = userManager.FindByName("user1");
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "user1",
                        Email = "anton@allenco.com",
                        EmailConfirmed = true,
                        JoinDate = DateTime.Now.AddYears(-3),
                        UserUId = 1,
                    };
                    var clients = new[]
                    {
                        new UserClient
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Javascript",
                            IsActive = true,
                            AllowedOrigin = "*",
                            RefreshTokenLifeTime = (int) TimeSpan.FromMinutes(15).TotalSeconds,
                            UserId = user.Id,
                            User = user
                        }
                    };

                    user.UserClients = clients;
                    context.UserClients.AddRange(clients);
                    result = userManager.Create(user, "A11en!C0");
                }

                if (result != null && result.Succeeded)
                {
                    role = roleManager.FindByName("User");
                    if (role == null)
                    {
                        role = new ApplicationRole("User");
                        roleManager.Create(role);
                    }

                    if (!userManager.IsInRole(user.Id, "User"))
                    {
                        userManager.AddToRole(user.Id, role.Name);
                    }
                }

                user = userManager.FindByName("director");
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = "director",
                        Email = "cdc@allenco.com",
                        EmailConfirmed = true,
                        JoinDate = DateTime.Now.AddYears(-3),
                        UserUId = 2,
                    };

                    var clients = new[]
                    {
                        new UserClient
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "Javascript",
                            IsActive = true,
                            AllowedOrigin = "*",
                            RefreshTokenLifeTime = (int) TimeSpan.FromMinutes(15).TotalSeconds,
                            UserId = user.Id,
                            User = user
                        }
                    };
                    user.UserClients = clients;
                    context.UserClients.AddRange(clients);
                    result = userManager.Create(user, "A!!en1C0");
                    if (result != null && result.Succeeded)
                    {
                        role = roleManager.FindByName("Attendee");
                        if (!userManager.IsInRole(user.Id, "Attendee"))
                        {
                            userManager.AddToRole(user.Id, role.Name);
                        }
                    }

                }
#endif
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ex.Message,ex.InnerException);
            }
        }
    }
}
