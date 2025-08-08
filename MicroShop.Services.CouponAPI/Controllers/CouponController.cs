using MicroShop.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroShop.Services.CouponAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/coupon")]
    public class CouponController : ControllerBase
    {

        private readonly ICouponService couponService;
        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet("gets")]
        public async Task<ResponseDto> Gets()
        {
            return await couponService.GetCoupons();
        }

        [HttpGet("get/{couponID:int}")]
        public async Task<ResponseDto> Get(int couponID)
        {
            return await couponService.GetCoupon(couponID);
        }

    }
}
