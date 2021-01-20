using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TWAssessment.Application.Common.ViewModels;
using TWAssessment.Application.Users.Queries;

namespace TWAssessment.API.Controllers
{
    [Route("v{v:apiVersion}/users")]
    [ApiVersion("1")]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// Filters data from https://run.mocky.io/v3/9a227fec-2ab2-43b5-9584-3cb455cc5a40 based on specified parameters
        /// </summary>
        /// <param name="age">Filter data based on provided age</param>
        /// <param name="city">Filter data based on provided city</param>
        /// <param name="tag">Tags separated by comma, if tags are found in data description it will be encapsulated in an italic HTML tag.</param>
        /// <returns>
        /// - All items if the request has no parameters.
        /// - A filtered subset based on filter parameters and the maximum and minimum age of all items and an array with all the cities of all items
        /// - Add html tags on item description to mark the words provided in tag parameter
        /// </returns>
        [HttpGet("filter")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(FilteredUsersResultViewModel), 200)]
        [ProducesResponseType(typeof(ExceptionViewModel), 400)]
        [ProducesResponseType(typeof(ExceptionViewModel), 500)]
        public async Task<FilteredUsersResultViewModel> GetUsers([FromQuery] int? age, [FromQuery] string city = "", [FromQuery ] string tag = "")
        {
            return await Mediator.Send(new GetFilteredUsersQuery(age, city, tag));
        }
    }
}
