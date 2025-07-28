using MicroShop.Services.CouponAPI.Data;
using MicroShop.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroShop.Services.CouponAPI;

public interface ICouponRepository
{
    Task<List<CouponListItem>> GetCoupons();
    Task<CouponListItem?> GetCoupon(int couponID);

    Task StartTransaction();
    Task StopTransaction();
    Task RollbackTransaction();
}

public class CouponRepository : ICouponRepository
{
    protected readonly AppDbContext dbContext;

    public async Task<List<CouponListItem>> GetCoupons()
    {
        return await (
            from m in dbContext.Coupon
            orderby m.StartOn descending
            select new CouponListItem(m.ID,
                m.Code,
                m.DiscountAmount,
                m.IsDiscountPercentage,
                m.MinimumPurchaseAmount,
                m.MaximumDiscountAmount,
                m.OnlyForFirst,
                m.NoOfTimeCanBeUsed,
                m.NoTimeOfUsed,
                m.StartOn,
                m.EndOn)
            ).ToListAsync();
    }



    public async Task<CouponListItem?> GetCoupon(int couponID)
    {
        return await(
            from m in dbContext.Coupon
            where m.ID == couponID
            select new CouponListItem(m.ID,
                m.Code,
                m.DiscountAmount,
                m.IsDiscountPercentage,
                m.MinimumPurchaseAmount,
                m.MaximumDiscountAmount,
                m.OnlyForFirst,
                m.NoOfTimeCanBeUsed,
                m.NoTimeOfUsed,
                m.StartOn,
                m.EndOn)
            ).FirstOrDefaultAsync();
    }


    public CouponRepository(AppDbContext context)
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
