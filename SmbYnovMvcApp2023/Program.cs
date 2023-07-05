using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using SmbYnovMvcApp2023.Data;
using SmbYnovMvcApp2023.service;
using Ynov2023AppDemo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
var sqlConnection = configuration.GetConnectionString("DbSqlServeurConnectionString");
builder.Services.AddDbContext<AppYnovContext>( options => options.UseSqlServer(sqlConnection));

builder.Services.AddScoped<IDataRepository, DataRepository>();

builder.Services.AddTransient<ServiceStorageIntegration>();


builder.Services.AddHostedService<LoraWanHostedService>();
builder.Services.AddHostedService<MQTTHostedService>();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "values",
        pattern: "/values",
        defaults: new { controller = "Values", action = "Get" });
});

app.Run();
