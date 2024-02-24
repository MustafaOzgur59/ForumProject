using BloggApp.Data.Concrete.EfCore;
using ForumProject.Data.Concrete;
using ForumProject.Entity;
using ForumProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sql_connection"));
});

builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccesDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var app = builder.Build();

SeedData.TestVerileriniDoldur(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
