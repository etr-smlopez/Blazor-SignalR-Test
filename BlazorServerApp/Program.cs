using BlazorServerApp.Data;
using BlazorServerApp.Hubs;
using BlazorServerApp.Service;
using DataAccess;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.ResponseCompression;
 
using System;
using System.Threading;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
  
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddTransient<SampleDataAccess>();
builder.Services.AddSingleton<EmployeeService>();
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<AppDbContext>(); 

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<UpdateHub>("/updatehub");
app.MapHub<EmployeesHub>("/employeeshub");
app.MapFallbackToPage("/_Host");


//// Start Blazor Server app in a new thread
//var blazorThread = new Thread(() => app.Run());
//blazorThread.Start();

//// Run WinForms app
//Application.EnableVisualStyles();
//Application.SetCompatibleTextRenderingDefault(false);
//Application.Run(new Form1()); // Assuming EmployeeForm is your WinForms form

app.Run();
