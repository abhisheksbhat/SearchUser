#region Namespaces
using SearchUserAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace SearchUserAPI.Repositories
{
    /// <summary>
    /// IUserRepository interface
    /// </summary>
    public interface ISearchUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetFilteredUsers(ISearchDetail searchDetail);
        Task<List<State>> GetAllStates();
    }
}
