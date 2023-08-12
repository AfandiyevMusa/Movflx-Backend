using Microsoft.EntityFrameworkCore;
using Final_Project.Data;
using System;
using Final_Project.Models;
using Microsoft.AspNetCore.Identity;
using Final_Project.Services;
using Final_Project.Helpers;
using Final_Project.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = true;
    opt.SignIn.RequireConfirmedEmail = true;

    opt.User.RequireUniqueEmail = true;

    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.AllowedForNewUsers = true;
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ICategoryServices, CategoryService>();
builder.Services.AddScoped<IEpisodeService, EpisodeService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IResolutionService, ResolutionService>();
builder.Services.AddScoped<ISeasonService, SeasonService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IStreamingService, StreamingService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IServiceOptionService, ServiceOptionService>();
builder.Services.AddScoped<IBlogAuthorService, BlogAuthorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();