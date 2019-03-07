#region Namespaces
using Microsoft.EntityFrameworkCore;
using SearchUserAPI.Models;
using SearchUserAPI.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
#endregion
namespace SearchUserAPI.Repositories
{
    /// <summary>
    /// UserRepository class acts a mediator between Controller and Database ORM
    /// </summary>
    public class SearchUserRepository : ISearchUserRepository
    {
        private UserDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">UserDbContext</param>
        public SearchUserRepository(UserDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get All the States
        /// </summary>
        /// <returns>Returns list of States</returns>
        public async Task<List<State>> GetAllStates()
        {
            List<State> stateList = await _context.State.ToListAsync();
            return stateList;
        }

        /// <summary>
        /// Get All the users
        /// </summary>
        /// <returns>Returns a user list</returns>
        public async Task<List<User>> GetAllUsers()
        {
           List<User> usersList = await _context.User.ToListAsync();
           return usersList;
        }

        /// <summary>
        /// Gets filtered users based on StateCode or Age Range with conditions such as And and OR
        /// </summary>
        /// <param name="ISearchDetail">SearchDetail</param>
        /// <returns>Returns filtered users matching criteria such as State Code , Age Range</returns>
        public async Task<List<User>> GetFilteredUsers(ISearchDetail searchDetail)
        {
            List<User> usersList = null;
            switch (searchDetail.SearchCriteriaEnum)
            {
                case SearchCriteria.State:
                    {
                        if (string.IsNullOrEmpty(searchDetail.StateCode))
                        {
                            throw new APIException("Search condition is invalid:: State");
                        }

                        usersList = await _context.User.AsNoTracking().Where(x => x.StateCode.ToUpper() == searchDetail.StateCode.ToUpper()).ToListAsync();
                        break;
                    }
                case SearchCriteria.AgeRange:
                    {
                        if (searchDetail.FromAge < 0 || searchDetail.ToAge < 0 || searchDetail.ToAge < searchDetail.FromAge)
                        {
                            throw new APIException("Search condition is invalid:: AgeRange");
                        }
                        else
                        {
                            usersList = await _context.User.AsNoTracking().Where(x => (x.Age >= searchDetail.FromAge && x.Age <= searchDetail.ToAge)).ToListAsync();
                        }
                        break;
                    }
                case SearchCriteria.StateAndAgeRange:
                    {
                        if ((string.IsNullOrEmpty(searchDetail.StateCode)))
                        {
                            throw new APIException("Search condition is invalid:: State");
                        }
                        else if (searchDetail.FromAge < 0 || searchDetail.ToAge < 0 || searchDetail.ToAge < searchDetail.FromAge)
                        {
                            throw new APIException("Search condition is invalid:: AgeRange");
                        }
                            usersList = await _context.User.AsNoTracking().Where(x => (x.Age >= searchDetail.FromAge && x.Age <= searchDetail.ToAge) && (x.StateCode.ToUpper() == searchDetail.StateCode.ToUpper())).ToListAsync();
                        break;
                    }
                case SearchCriteria.StateOrAgeRange:
                    {
                        if ((string.IsNullOrEmpty(searchDetail.StateCode)))
                        {
                            throw new APIException("Search condition is invalid:: State");
                        }
                        else if (searchDetail.FromAge < 0 || searchDetail.ToAge < 0 || searchDetail.ToAge < searchDetail.FromAge)
                        {
                            throw new APIException("Search condition is invalid:: AgeRange");
                        }
                        usersList = await _context.User.AsNoTracking().Where(x => (x.Age >= searchDetail.FromAge && x.Age <= searchDetail.ToAge) || (x.StateCode.ToUpper() == searchDetail.StateCode.ToUpper())).ToListAsync();
                        break;
                    }
            }

           return usersList;
        }
       
    }

   
}