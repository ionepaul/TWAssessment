using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TWAssessment.Application.Common.Extensions;
using TWAssessment.Application.Common.Interfaces;
using TWAssessment.Application.Common.Validators;
using TWAssessment.Application.Common.ViewModels;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Application.Users.Queries
{
    /// <summary>
    /// Represents the query for filtering the data
    /// </summary>
    public class GetFilteredUsersQuery : IRequest<FilteredUsersResultViewModel>
    {
        public int? Age { get; set; }

        public string City { get; set; }

        public string Tag { get; set; }

        public GetFilteredUsersQuery(int? Age, string City, string Tag)
        {
            this.Age = Age;
            this.City = City;
            this.Tag = Tag;
        }
        
        /// <summary>
        /// Class handler for MediatR request of the GetFilteredUsersQuery
        /// </summary>
        public class GetFilteredUsersQueryHandler : IRequestHandler<GetFilteredUsersQuery, FilteredUsersResultViewModel>
        {
            private readonly IMockDataService _mockDataService;
            private readonly IMapper _mapper;

            public GetFilteredUsersQueryHandler(IMockDataService mockDataService, IMapper mapper)
            {
                _mockDataService = mockDataService;
                _mapper = mapper;
            }

            /// <summary>
            /// Handles the request for returning the filtering data based on requirements
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<FilteredUsersResultViewModel> Handle(GetFilteredUsersQuery request, CancellationToken cancellationToken)
            {
                request.ValidateParameters();

                var data = await _mockDataService.GetData();

                if (data == null)
                {
                    //Even if we know that something went wrong while retrieving data from the server, we shouldn't exposed detailed error information.
                    throw new Exception("Something went wrong while processing a request. Please try again.");
                }

                try
                {
                    var minAge = data.Min(x => x.Age);
                    var maxAge = data.Max(x => x.Age);
                    var cities = string.Join(",", data.Select(x => string.Join(",", x.Cities))).Split(",").Distinct();

                    data = data.FilterByAge(request.Age)
                               .FilterByCity(request.City)
                               .ApplyItalicHTMLTagOnDescriptionBasedOnTagParameter(request.Tag);

                    return _mapper.Map<IEnumerable<User>, FilteredUsersResultViewModel>(data,
                        opt => opt.AfterMap((src, dest) =>
                        {
                            dest.MaxAge = maxAge;
                            dest.MinAge = minAge;
                            dest.Cities = cities;
                        }));
                }
                catch (Exception)
                {
                    //Log exception
                    throw new Exception("Something went wrong while processing your request. Please try again");
                }
            }
        }
    }
}
