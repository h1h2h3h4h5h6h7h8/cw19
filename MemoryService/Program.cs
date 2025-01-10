using App.Domain.AppServices.Memory.AppService;
using App.Domain.Core.Memory.Contract.AppServices;
using App.Domain.Core.Memory.Contract.Repositories;
using App.Domain.Core.Memory.Contract.Services;
using App.Domain.Services.Memory.Service;
using App.Infra.Data.Db.SqlServer.Ef.Memory;
using App.Infra.Data.Repos.Ef.Memory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContaxt>();

builder.Services.AddScoped<IMemberAppService, MemberAppService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IMemberService, MemberService>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
