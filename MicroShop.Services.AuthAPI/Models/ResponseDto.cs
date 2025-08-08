namespace MircroShop.Services.AuthAPI.Models
{
    public class ResponseDto(bool isSuccess, string msg, object data = null)
    {
        public string Message { get; } = msg;
        public bool IsSuccess { get; } = isSuccess;
        public object? Data { get; } = data;
    }
}
