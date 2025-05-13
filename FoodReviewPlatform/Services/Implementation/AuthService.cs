using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class AuthService(
        UserManager<IdentityUser> userManager,
        FoodReviewPlatformDbContext context,
        ITokenService tokenService) : IAuthService
    {
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var identityUser = await userManager.FindByEmailAsync(request.Email);

            if (identityUser is null)
            {
                throw new Exception("User not found");
            }

            var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!checkPasswordResult)
            {
                throw new Exception("Invalid password");
            }

            var roles = await userManager.GetRolesAsync(identityUser);

            var jwtToken = tokenService.CreateJwtToken(identityUser, roles.ToList());

            var response = new LoginResponse
            {
                Email = request.Email,
                Roles = roles.ToList(),
                Token = jwtToken
            };

            return response;
        }

        public async Task Register(RegisterRequest request)
        {
            var user = new IdentityUser
            {
                UserName = request.UserName.Trim(),
                Email = request.Email.Trim()
            };

            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                throw new Exception("User already exists");
            }

            identityResult = await userManager.AddToRoleAsync(user, UserRoleClass.User);

            if (!identityResult.Succeeded)
            {
                throw new Exception("Role does not exists");
            }
        }

        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var identityUser = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (identityUser is null)
            {
                throw new Exception("User not found");
            }

            var checkPasswordResult = BCrypt.Net.BCrypt.Verify(request.Password, identityUser.PasswordHash);

            if (!checkPasswordResult)
            {
                throw new Exception("Invalid password");
            }

            var jwtToken = tokenService.CreateJwtToken(identityUser);

            var response = new LoginResponse
            {
                Email = request.Email,
                Token = jwtToken
            };

            return response;
        }

        public async Task RegisterUser(RegisterRequest request)
        {
            var user = new User
            {
                UserName = request.UserName.Trim(),
                Email = request.Email.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                InsertionTime = DateTime.UtcNow,
            };

            if (await context.Users.AnyAsync(u => u.Email == user.Email))
            {
                throw new Exception("User already exists");
            }

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
