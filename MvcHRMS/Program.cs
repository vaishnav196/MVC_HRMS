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

builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IOfferLetterRepository, OfferLetterRepository>();
builder.Services.AddScoped<OfferLetterService>();


builder.Services.AddScoped<IEmpRepository, EmpRepository>();
builder.Services.AddScoped<IEmpService, EmpService>();
builder.Services.AddScoped<IPaySlipService, PaySlipService>();

builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();

builder.Services.AddScoped<ILeaveService, LeaveService>();


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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
