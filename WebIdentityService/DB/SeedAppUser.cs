using Microsoft.AspNetCore.Identity;
using WebIdentityService.Entity;

namespace WebIdentityService.DB
{
    public static class SeedAppUser 
    {
        public static void SeedData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                       
            userManager.CreateAsync(new AppUser
            {
                UserName = "mattcoder",
                Email = "mattcoder2013@yahoo.com",
                EmailConfirmed = true,
                DisplayName = "admin",
            
                Address = new Address
                {
                    State = "admin",
                    City = "admin",
                    PostalCode = "admin",
                    DetailAddress = "admin"
                }
            }, "Asdf1@34").Wait();
            }

        }
    }
}
