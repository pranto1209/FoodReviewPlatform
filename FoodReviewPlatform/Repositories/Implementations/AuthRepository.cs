using FoodReviewPlatform.Databases;
using FoodReviewPlatform.Databases.Entities;
using FoodReviewPlatform.Repositories.Interfaces;
using FoodReviewPlatform.Utilities.AuditInfos;
using Microsoft.EntityFrameworkCore;

namespace FoodReviewPlatform.Repositories.Implementations
{
    public class AuthRepository(FoodReviewPlatformDbContext context) : IAuthRepository
    {
        public async Task<User> GetUserByEmail(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById()
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == AuditContext.UserId);
        }

        public async Task<List<string>> GetRolesByUser(long userId)
        {
            var query = await (from userRole in context.UserRoles
                               join role in context.Roles on userRole.RoleId equals role.Id
                               where userRole.UserId == userId
                               select role.Name).ToListAsync();

            return query;
        }

        public async Task AddUser(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task EditUser(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task AddUserRole(UserRole userRole)
        {
            await context.UserRoles.AddAsync(userRole);
            await context.SaveChangesAsync();
        }
    }
}
