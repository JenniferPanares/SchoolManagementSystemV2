using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Data;
using FluentValidation.AspNetCore;
using SchoolManagementSystem.Services;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.SignIn.RequireConfirmedAccount = false; // For development
}).AddEntityFrameworkStores<ApplicationDbContext>();

// Register FluentValidation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<StudentValidator>();

// Google Maps API Key Configuration
var googleApiKey = builder.Configuration["APIKeys:GoogleMaps"];
if (string.IsNullOrEmpty(googleApiKey))
{
    throw new InvalidOperationException("Google API Key is missing. Please add it to appsettings.json under 'APIKeys:GoogleMaps'.");
}

// Register GoogleAerialViewService with the API key
builder.Services.AddSingleton<GoogleAerialViewService>(sp =>
    new GoogleAerialViewService(googleApiKey));

var app = builder.Build();

// Drop and recreate the database during application startup (development only)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureDeleted(); // Drop the database
        context.Database.EnsureCreated(); // Recreate the database
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while recreating the database.");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map Routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
