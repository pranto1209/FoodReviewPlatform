using FoodReviewPlatform.Databases.Entities;

namespace FoodReviewPlatform.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById();
        Task<List<string>> GetRolesByUser(long userId);
        Task AddUser(User user);
        Task EditUser(User user);
        Task DeleteUser(User user);
        Task AddUserRole(UserRole userRole);
    }
}
