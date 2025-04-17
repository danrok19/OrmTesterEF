using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Application.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMediatR(x => 
    x.RegisterServicesFromAssemblyContaining<GetUserList.Handler>());

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
