using WebIdentityService.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.Run();
