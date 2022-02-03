using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Turbo.WebUI.Models.Entities.Membership;

namespace MotiCv.Models.DbContexts
{
    static public class TurboDbSeed
    {
        static public IApplicationBuilder SeedMembership(this IApplicationBuilder app)
        {
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var role = new TurboRole
                {
                    Name = "Superadmin"
                };
                var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<TurboRole>>();
                var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<TurboUser>>();
                
                if (rolemanager.RoleExistsAsync(role.Name).Result)
                {
                    role = rolemanager.FindByNameAsync(role.Name).Result;
                }
                else
                {
                   var rolecreateresult = rolemanager.CreateAsync(role).Result;
                    if (!rolecreateresult.Succeeded)
                        goto end;
                }
                string pasword = "123456";
                var user = new TurboUser
                {
                    Name = "Yusif",
                    SurName ="Huseynov",
                    Email="yusifhh@code.edu.az",
                    EmailConfirmed=true,
                    UserName="Yusif"
                };
                var foundeduser = usermanager.FindByEmailAsync(user.Email).Result;
                if (foundeduser!=null && !usermanager.IsInRoleAsync(foundeduser,role.Name).Result)
                {
                    usermanager.AddToRoleAsync(foundeduser, role.Name).Wait();
                }
                else if(foundeduser == null)
                {
                    var usercreateresult = usermanager.CreateAsync(user,pasword).Result;
                    if (!usercreateresult.Succeeded)
                        goto end;
                    usermanager.AddToRoleAsync(user, role.Name).Wait();
                }
            }
            end:
            return app;
        }
    }
}
