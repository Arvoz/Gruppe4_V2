using Backend.Core.Domain;
using Backend.Core.Ports;
using Backend.Core.Service;
using Backend.Infrastructure.FileStorage;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Registrer repositories for devices og groups
builder.Services.AddScoped<IDeviceRepository>(provider => new DeviceRepository("devices.json"));
builder.Services.AddScoped<IGroupRepository>(provider => new GroupRepository("groups.json"));

// Registrer tjenester (services)
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IGroupService, GroupService>();



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
