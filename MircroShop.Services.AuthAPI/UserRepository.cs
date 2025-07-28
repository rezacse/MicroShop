using Microsoft.EntityFrameworkCore;
using MircroShop.Services.AuthAPI.Data;

namespace MircroShop.Services.AuthAPI;

public interface IUserRepository
{

    Task StartTransaction();
    Task StopTransaction();
    Task RollbackTransaction();
}

public class UserRepository : IUserRepository
{
    protected readonly AuthDbContext dbContext;



    public UserRepository(AuthDbContext context)
    {
        dbContext = context;
    }

    //protected string GetDBConnectionString()
    //{
    //    return configuration.GetConnectionString("DefaultConnection");
    //}

    public async Task StartTransaction()
    {
        await dbContext.Database.BeginTransactionAsync();
    }

    public async Task StopTransaction()
    {
        if (dbContext.Database.CurrentTransaction == null) return;

        await dbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransaction()
    {
        if (dbContext.Database.CurrentTransaction == null) return;

        await dbContext.Database.RollbackTransactionAsync();
    }
}
