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
//builder.Services.AddScoped<IDeviceRepository>(provider => new DeviceRepository("devices.json"));
builder.Services.AddScoped<IJsonFileHandler<Group>>(provider => new JsonFileHandler<Group>("groups.json"));
builder.Services.AddScoped<IJsonFileHandler<Device>>(provider => new JsonFileHandler<Device>("devices.json"));

// Registrer tjenester (services)
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();


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
