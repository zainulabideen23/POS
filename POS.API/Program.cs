using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using POS.Identity.Domain.Entities;
using POS.Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register Identity Module
//builder.Services.AddIdentityApplication();
builder.Services.AddIdentityInfrastructure(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//swagger
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
