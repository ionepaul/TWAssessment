using System.Collections.Generic;
using TWAssessment.Application.Common.Mappings;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Application.Common.ViewModels
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Email { get; set; }

        public int Age { get; set; }

        public IEnumerable<string> Cities { get; set; }

        public string Description { get; set; }
    }
}
