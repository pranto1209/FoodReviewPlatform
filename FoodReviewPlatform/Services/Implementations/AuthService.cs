using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Models.Domains;
using FoodReviewPlatform.Models.Requests;
using FoodReviewPlatform.Models.Responses;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Services.Interfaces;
using FoodReviewPlatform.Utilities.Exceptions;
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

                    if (await authRepository.GetUserByEmail(request.Email) != null)
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
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task EditUser(EditUserRequest request)
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

            user.UserName = request.NewUserName.Trim();
            user.Email = request.NewEmail.Trim();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.ModificationTime = DateTime.UtcNow;

            if (await authRepository.GetUserByEmail(request.NewEmail) != null)
            {
                throw new CustomException("Email already exists");
            }

            await authRepository.EditUser(user);
        }

        public async Task DeleteUser(DeleteUserRequest request)
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

            await authRepository.DeleteUser(user);
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
