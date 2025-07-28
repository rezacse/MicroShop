using MicroShop.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Services.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponController : ControllerBase
    {

        private readonly ICouponService couponService;
        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet(Name = "gets")]
        public async Task<ResponseDto> Gets()
        {
            return await couponService.GetCoupons();
        }

        [HttpGet(Name = "get/{id}")]
        public async Task<ResponseDto> Get(int couponID)
        {
            return await couponService.GetCoupon(couponID);
        }

    }
}
