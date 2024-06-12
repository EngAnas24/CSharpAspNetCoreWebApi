using ApiApp.Controllers;
using ApiApp.Data;
using ApiApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
/*builder.Services.AddDbContext<DBData>(op =>
        op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));*/
builder.Services.AddDbContext<DBData>(op =>
        op.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<DBData>();

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable static file serving
app.UseRouting();
app.UseCors("AllowAllOrigins"); // Ensure CORS is configured if needed

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
