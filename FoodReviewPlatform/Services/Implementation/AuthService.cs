using FoodReviewPlatform.Database;
using FoodReviewPlatform.Database.Entities;
using FoodReviewPlatform.Models.Domain;
using FoodReviewPlatform.Models.Request;
using FoodReviewPlatform.Models.Response;
using FoodReviewPlatform.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace FoodReviewPlatform.Services.Implementation
{
    public class AuthService(
        FoodReviewPlatformDbContext context,
        IConfiguration configuration) : IAuthService
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

            var jwtToken = CreateJwtToken(user, roles, configuration);

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
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
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

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private static string CreateJwtToken(User user, List<string> roles, IConfiguration configuration)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
