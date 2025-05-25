using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Services.Interfaces;
using FoodReviewPlatform.Utilities.Middlewares.ExceptionMiddlewares;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace FoodReviewPlatform.Services.Implementations
{
    public class AuthService(
        IAuthRepository authRepository,
        IConfiguration configuration) : IAuthService
    {
        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var user = await authRepository.GetUserByEmail(request.Email);

            if (user is null)
            {
                throw new CustomException("User not found");
            }

            var checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!checkPassword)
            {
                throw new CustomException("Invalid password");
            }

            var roles = await authRepository.GetRolesByUser(user.Id);

            var token = CreateJwtToken(user, roles);

            var response = new LoginResponse
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                Email = request.Email,
                Roles = roles,
                Token = token
            };

            return response;
        }

        public async Task RegisterUser(RegisterRequest request)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var user = new User
            {
                UserName = request.UserName.Trim(),
                Email = request.Email.Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                InsertionTime = DateTime.UtcNow
            };

            if (await authRepository.GetUserByEmail(user.Email) is not null)
            {
                throw new CustomException("User already exists");
            }

            await authRepository.AddUser(user);

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = (int)UserRoleEnum.User
            };

            await authRepository.AddUserRole(userRole);

            scope.Complete();
        }

        public async Task<UserResponse> GetUserById()
        {
            var user = await authRepository.GetUserById();

            var response = new UserResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return response;
        }

        public async Task EditUser(EditUserRequest request)
        {
            var user = await authRepository.GetUserById();

            var checkPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);

            if (!checkPassword)
            {
                throw new CustomException("Invalid password");
            }

            if (user.Email.ToLower() != request.Email.ToLower() && await authRepository.GetUserByEmail(request.Email) != null)
            {
                throw new CustomException("Email already exists");
            }

            user.UserName = request.UserName.Trim();
            user.Email = request.Email.Trim();
            if (!string.IsNullOrWhiteSpace(request.NewPassword)) user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ModificationTime = DateTime.UtcNow;

            await authRepository.EditUser(user);
        }

        public async Task DeleteUser(DeleteUserRequest request)
        {
            var user = await authRepository.GetUserById();

            var checkPassword = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash);

            if (!checkPassword)
            {
                throw new CustomException("Invalid password");
            }

            await authRepository.DeleteUser(user);
        }

        private string CreateJwtToken(User user, List<string> roles)
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

            var token = new JwtSecurityToken
            (
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
