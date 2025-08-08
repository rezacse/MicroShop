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

builder.Services.AddTransient<ILoggerFactory, LoggerFactory>();
//builder.Services.AddSingleton<IServiceBusConsumer, ServiceBusConsumer>();

//var optionBuilder = new DbContextOptionsBuilder<EmailDbContext>();
//builder.Services.AddSingleton(new EmailRepository(optionBuilder.Options));
builder.Services.AddTransient<IEmailRepository, EmailRepository>();
builder.Services.AddTransient<IEmailService, EmailSerivce>();
//builder.Services.AddSingleton<ILogger>();
//builder.Services.AddSingleton(new EmailSerivce());
builder.Services.AddHostedService<SignupQueueConsumer>();
builder.Services.AddHostedService<OrderTopicConsumer>();

//builder.Services.AddSingleton<IServiceBusConsumer, RabbitMqConsumer>();

var app = builder.Build();

//app.UseServiceBusConsumer();

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
