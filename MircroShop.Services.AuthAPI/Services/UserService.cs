using MicroShop.Services.AuthAPI.MessageSender;
using Microsoft.AspNetCore.Identity;
using MircroShop.Services.AuthAPI.Models;
using MircroShop.Services.AuthAPI.Tables;

namespace MicroShop.Services.AuthAPI.Services
{
    public interface IUserService
    {
        Task<ResponseDto> Register(RegisterDto req);
        Task<ResponseDto> Login(LoginDto req);
        Task<ResponseDto> AssignRole(string email, string roleName);
    }

    public class UserService : IUserService
    {
        private readonly ILogger<UserService> logger;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenFactory tokenFactory;
        private readonly IUserRepository userRepository;
        private readonly IAppMessageSender messageSender;
        private readonly string queueName;

        public UserService(
            IConfiguration configuration,
            ILogger<UserService> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenFactory tokenFactory,
            IUserRepository userRepository,
            IAppMessageSender messageSender)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenFactory = tokenFactory;
            this.userRepository = userRepository;
            this.messageSender = messageSender;

            queueName = configuration.GetValue<string>("RabbitMQ:QueueName") ?? string.Empty;
        }


        public async Task<ResponseDto> Register(RegisterDto req)
        {
            try
            {
                var user = new User(req);
                var data = await userManager.CreateAsync(user, req.Password);
                if(!data.Succeeded)
                {
                    var errors = string.Join(", ", data.Errors.Select(e => e.Description));
                    return new ResponseDto(false, errors);
                }

                await AssignRole(req.Email, "CUSTOMER");

                var body = new
                {
                    Name = req.Name,
                    Email = req.Email,
                };

                await messageSender.SendMessage(queueName, body);

                return new ResponseDto(true, "Registered Successfully");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"registering User");
                return new ResponseDto(true, "", "Unhandled error");
            }
        }

        public async Task<ResponseDto> Login(LoginDto req)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(req.Email);
                if (user == null) return new ResponseDto(false, "Invalid Credentials");

                var isValid = await userManager.CheckPasswordAsync(user, req.Password);
                if (!isValid) return new ResponseDto(false, "Invalid Credentials");

                var token = tokenFactory.GenerateAccessToken(user);
                return new ResponseDto(true, "", token);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"logging User");
                return new ResponseDto(true, "", "Unhandled error");
            }
        }

        public async Task<ResponseDto> AssignRole(string email, string roleName)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null) return new ResponseDto(false, "User not found!");

                var isExist = await roleManager.RoleExistsAsync(roleName);
                if (!isExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    //return new ResponseDto(false, "Role not found!");
                }

                await userManager.AddToRoleAsync(user, roleName);

                return new ResponseDto(true, "Role assigned");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"assigning Role");
                return new ResponseDto(true, "", "Unhandled error");
            }
        }
    }
}
