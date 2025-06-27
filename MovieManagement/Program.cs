using Microsoft.EntityFrameworkCore;
using MovieManagement.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MongoDB.Driver;
using MovieManagement.Data;
using MovieManagement.Entities;
using MovieManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<MovieManagement.Data.MongoDB>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = builder.Configuration.GetSection("MongoDBSettings").Get<MovieManagement.Data.MongoDB>();
    return new MongoClient(settings.ConnectionString);
});
builder.Services.AddDbContext<MovieManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(t=>t.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<MovieManagementDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddRazorPages();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // ✅ default for Identity framework
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // optional
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedRolesAndAdminAsync(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();