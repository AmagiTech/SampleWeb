using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sample.Business;
using Sample.Business.Interfaces;
using Sample.Data;
using Sample.Data.Uow;
using Sample.Data.Uow.Interfaces;
using Sample.WebUI.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SampleDB");
builder.Services.AddDbContext<SampleDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthentication(q =>
{
    q.DefaultScheme = IdentityConstants.ApplicationScheme;
    q.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies(q => { });


builder.Services.AddIdentityCore<IdentityUser>(o =>
{
    o.Stores.MaxLengthForKeys = 128;
    o.Password.RequireUppercase = true;
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = true;
    o.Password.RequireNonAlphanumeric = true;
    o.Password.RequiredLength = 8;
    o.User.RequireUniqueEmail = true;
}).AddSignInManager()
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<SampleDbContext>();


builder.Services.AddAutoMapper(typeof(SampleMapper));

builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<IItemsRepo, ItemsRepo>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IItemsService, ItemsService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
