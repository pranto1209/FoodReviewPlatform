using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Services.Implementation
{
    public class AuthService(
        FoodReviewPlatformDbContext context,
        ITokenService tokenService) : IAuthService
    {
        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            var checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!checkPassword)
            {
                throw new Exception("Invalid password");
            }

            var roles = await (from userRole in context.UserRoles
                               join role in context.Roles on userRole.RoleId equals role.Id
                               where userRole.UserId == user.Id
                               select role.Name).ToListAsync();

            var jwtToken = tokenService.CreateJwtToken(user, roles);

            var response = new LoginResponse
            {
                UserId = user.Id.ToString(),
                Email = request.Email,
                Roles = roles,
                Token = jwtToken
            };

            return response;
        }

        public async Task RegisterUser(RegisterRequest request)
        {
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var user = new User
                {
                    UserName = request.UserName.Trim(),
                    Email = request.Email.Trim(),
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    InsertionTime = DateTime.UtcNow
                };

                if (await context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    throw new Exception("User already exists");
                }

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = (int)RoleEnum.User
                };

                await context.UserRoles.AddAsync(userRole);
                await context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
