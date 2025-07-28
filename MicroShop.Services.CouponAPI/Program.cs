using AutoMapper;
using MicroShop.Services.CouponAPI;
using MicroShop.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("CouponConnection")));

//builder.Services.AddAutoMapper(typeof(Program));

//IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
//builder.Services.AddSingleton(mapper);
//builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<ICouponService, CouponService>();

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

ApplyMigration(app);

app.Run();


void ApplyMigration(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (dbContext.Database.GetPendingMigrations().Count()>0)
        {
            dbContext.Database.Migrate();
        }
    }
}


