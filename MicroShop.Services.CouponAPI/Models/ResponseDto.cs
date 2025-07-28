namespace MicroShop.Services.CouponAPI.Models
{
    public class ResponseDto(bool isSuccess, string msg, object data = null)
    {
        public bool Status { get; } = true;
        public string Message { get; } = msg;
        public bool IsSuccess { get; } = isSuccess;
        public object? Data { get; } = data;
    }
}
