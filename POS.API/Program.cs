using POS.Shared.Infrastructure;
using POS.Identity.Infrastructure;
using POS.Identity.Application;
using POS.Product.Application;
using POS.Product.Infrastructure;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddProductApplication();
builder.Services.AddProductInfrastructure(builder.Configuration);

//Identity
builder.Services.AddIdentityApplication();
builder.Services.AddIdentityInfrastructure(builder.Configuration);

//Shared
builder.Services.AddSharedInfrastructure();
//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
