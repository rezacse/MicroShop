using MicroShop.Services.EmailAPI.Data;
using MicroShop.Services.EmailAPI.Messaging;
using MicroShop.Services.EmailAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EmailDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("EmailConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();
builder.Services.AddSingleton<IEmailService, EmailSerivce>();

var app = builder.Build();

app.UseServiceBusConsumer();

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
