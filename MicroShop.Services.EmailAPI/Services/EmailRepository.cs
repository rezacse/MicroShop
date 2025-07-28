using MicroShop.Services.EmailAPI.Data;
using MicroShop.Services.EmailAPI.Dtos;
using MicroShop.Services.EmailAPI.Tables;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Services.EmailAPI.Services;

public interface IEmailRepository
{
    Task AddEmailLog(AddEmailDto data);

    Task StartTransaction();
    Task StopTransaction();
    Task RollbackTransaction();
}

public class EmailRepository : IEmailRepository
{
    protected readonly EmailDbContext dbContext;

    public async Task AddEmailLog(AddEmailDto data)
    {
        var email = new EmailLog(data);
        dbContext.EmailLog.Add(email);
        await dbContext.SaveChangesAsync();
    }



    public EmailRepository(EmailDbContext context)
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
