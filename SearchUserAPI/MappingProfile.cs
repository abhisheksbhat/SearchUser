#region Namespaces
using AutoMapper;
using SearchUserAPI.Models;
using SearchUserAPI.ViewModels;
#endregion

namespace SearchUserAPI
{
    /// <summary>
    /// Class used by AutoMapper for managing the mappings
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MappingProfile() 
        {
            // Map User to UserViewModel
            CreateMap<User, UserViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.LastName) ? src.FirstName : src.FirstName + " " + src.LastName));
        }

    }
}