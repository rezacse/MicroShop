using AutoMapper;
using MicroShop.Services.CouponAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroShop.Services.CouponAPI
{
    public interface ICouponService
    {
        Task<ResponseDto> GetCoupons();
        Task<ResponseDto> GetCoupon(int couponID);
    }

    public class CouponService : ICouponService
    {
        private readonly ILogger<CouponService> logger;
        private readonly ICouponRepository couponRepository;

        public CouponService(
            ILogger<CouponService> logger,
            ICouponRepository couponRepository)
        {
            this.logger = logger;
            this.couponRepository = couponRepository;
        }

        public async Task<ResponseDto> GetCoupons()
        {
            try
            {
                var data = await couponRepository.GetCoupons();
                return new ResponseDto(true, "", data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"getting Coupons");
                return new ResponseDto(true, "", "Unhandled error");
            }
        }

        public async Task<ResponseDto> GetCoupon(int couponID)
        {
            try
            {
                var data = await couponRepository.GetCoupon(couponID);
                return new ResponseDto(true, "", data);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"getting Coupons");
                return new ResponseDto(true, "", "Unhandled error");
            }
        }
    }
}
