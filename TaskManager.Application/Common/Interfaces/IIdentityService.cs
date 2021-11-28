using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Common.Models;

namespace TaskManager.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<string> GetUserEmailAsync(string userId);

        Task<string> GetUserRoleAsync(string userId);

        Task<bool> IsUserExistAsync(string parameter);

        Task<Result> AddRoleToUserAsync(string userId, string role);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<List<(string appUserId, string role)>> FetchUserRolesAsync();
    }
}
