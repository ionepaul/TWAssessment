using System.Collections.Generic;
using System.Threading.Tasks;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Application.Common.Interfaces
{
    public interface IMockDataService
    {
        Task<IEnumerable<User>> GetData();
    }
}
