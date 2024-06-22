using Microsoft.EntityFrameworkCore;
using MvcHRMS.Data;
using MvcHRMS.Repository;
using MvcHRMS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(
       builder.Configuration.GetConnectionString("dbconn")

));
//builder.Services.AddScoped<EmpRepo, EmpService>();

builder.Services.AddScoped<IOfferLetterRepository, OfferLetterRepository>();
builder.Services.AddScoped<OfferLetterService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=GenerateOffer}/{action=Index}/{id?}");

app.Run();
