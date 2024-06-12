using CQRS_Lib;
using CQRS_Lib.Data;
using CQRS_Lib.Repos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DBData>(x => x.UseSqlServer
(builder.Configuration.GetConnectionString("MyCon")));
builder.Services.AddScoped<IItems, ItemRepo>();
builder.Services.AddMediatR(typeof(MyLib).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
