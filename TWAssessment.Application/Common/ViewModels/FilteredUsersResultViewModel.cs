using System.Collections.Generic;
using AutoMapper;
using TWAssessment.Application.Common.Mappings;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Application.Common.ViewModels
{
    public class FilteredUsersResultViewModel : IMapFrom<IEnumerable<User>>
    {
        /// <summary>
        /// Result array containing filtered users object
        /// </summary>
        public IEnumerable<UserViewModel> Result { get; set; }

        /// <summary>
        /// Mininum age of all data
        /// </summary>
        public int MinAge { get; set; }

        /// <summary>
        /// Mininum age of all data
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// All the cities from the service URL provided
        /// </summary>
        public IEnumerable<string> Cities { get; set; }

        /// <summary>
        /// Auto Mapper method for mapping the filtered users array to view model
        /// Ignoring other properties since it will get populated after the results array mapping
        /// </summary>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<IEnumerable<User>, FilteredUsersResultViewModel>()
                .ForMember(d => d.MaxAge, opt => opt.Ignore())
                .ForMember(d => d.MinAge, opt => opt.Ignore())
                .ForMember(d => d.Cities, opt => opt.Ignore())
                .ForMember(d => d.Result, opt => opt.MapFrom(s => s));
        }
    }
}
