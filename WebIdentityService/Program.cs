using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebIdentityService.DB;
using WebIdentityService.Entity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityService(builder.Configuration);
var dbcontext = builder.Services.BuildServiceProvider().GetRequiredService<AppIdentityDbContext>();
var userManager = builder.Services.BuildServiceProvider().GetRequiredService<UserManager<AppUser>>();

dbcontext.Database.EnsureCreated();
dbcontext.Database.MigrateAsync();
SeedAppUser.SeedData(userManager);


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Hello World!");


app.Run();
