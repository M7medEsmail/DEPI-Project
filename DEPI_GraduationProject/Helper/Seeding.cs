using DEPI_DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using static System.Formats.Asn1.AsnWriter;

namespace DEPI_GraduationProject.Helper
{
    public  class Seeding
    {
       public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var signManger = scope.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();


                // Define your roles here
                string[] roleNames = { Roles.Admin.ToString(), Roles.User.ToString()};

                foreach (var roleName in roleNames)
                {
                    var roleExists = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        // Create the role if it doesn't exist
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
               // var user = new ApplicationUser()
               // {
               //     FirstName = "Mohamed",
               //     LastName = "Esmail",
               //     Email = "m7med.esmail22@gmail.com",
               //     PasswordHash = "+DEZH/Ftr843NlJlwEtoxytRxoISYo96WIQVedU4qZWZw==",
               //     PicturalUrl = "/Images/Users/1714051271244_22bcaa62-8cff-4bdb-b399-9dea920c8ac4.jpg",
               //     PhoneNumber = "+201062838535"
               // };
               //await userManger.CreateAsync(user);
               //await userManger.AddToRoleAsync(user,"Admin");

            }

          

            
        }
    }
}
