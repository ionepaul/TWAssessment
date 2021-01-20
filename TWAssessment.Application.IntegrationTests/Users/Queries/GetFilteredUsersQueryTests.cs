using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TWAssessment.Application.Common.Exceptions;
using TWAssessment.Application.Common.ViewModels;
using TWAssessment.Application.Users.Queries;

namespace TWAssessment.Application.IntegrationTests.Users.Queries
{
    using static TestingFixture;

    public class GetFilteredUsersQueryTests
    {
        [Test]
        [TestCase(null, "", "")]
        public async Task ShouldReturnAllData(int? age, string city, string tag)
        {
            var query = new GetFilteredUsersQuery(age, city, tag);

            var queryResult = await SendAsync(query);

            queryResult.Should().NotBeNull();
            queryResult.Result.Should().NotBeEmpty();
            queryResult.Result.Should().HaveCount(25);

            //Min Age should always be 21 based on data of the mock service
            //Max Age should always be 61 based on data of the mock service
            queryResult.MinAge.Should().Equals(21);
            queryResult.MaxAge.Should().Equals(61);

            //Cities array should always contain six entries basesd on data of the mock service
            queryResult.Cities.Should().NotBeEmpty();
            queryResult.Cities.Should().HaveCount(6);
        }

        [Test]
        [TestCase(45, "Kodinsk", "")]
        public async Task ShouldReturnFilteredData(int? age, string city, string tag)
        {
            var query = new GetFilteredUsersQuery(age, city, tag);

            var queryResult = await SendAsync(query);

            queryResult.Should().NotBeNull();
            queryResult.Result.Should().NotBeEmpty();
            queryResult.Result.Should().HaveCount(2);

            //Min Age should always be 21 based on data of the mock service
            //Max Age should always be 61 based on data of the mock service
            queryResult.MinAge.Should().Equals(21);
            queryResult.MaxAge.Should().Equals(61);

            //Cities array should always contain six entries basesd on data of the mock service
            queryResult.Cities.Should().NotBeEmpty();
            queryResult.Cities.Should().HaveCount(6);
        }

        [Test]
        [TestCase(24, "Amiens", "fusce,ornare")]
        public async Task ShouldReturnFilteredAndMarkHTMLTagProperly(int? age, string city, string tag)
        {
            var query = new GetFilteredUsersQuery(age, city, tag);

            var queryResult = await SendAsync(query);

            queryResult.Should().NotBeNull();
            queryResult.Result.Should().NotBeEmpty();
            queryResult.Result.Should().HaveCount(1);
            queryResult.Result.ElementAt(0).Description.Should().Equals("<i>Fusce</i> congue, diam id <i>ornare</i> imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue.");

            //Min Age should always be 21 based on data of the mock service
            //Max Age should always be 61 based on data of the mock service
            queryResult.MinAge.Should().Equals(21);
            queryResult.MaxAge.Should().Equals(61);

            //Cities array should always contain six entries basesd on data of the mock service
            queryResult.Cities.Should().NotBeEmpty();
            queryResult.Cities.Should().HaveCount(6);
        }

        [Test]
        [TestCase(-1)]
        public void ShouldThrowBadRequestExceptionWhenInvalidAgeParameterTest(int? age)
        {
            Func<Task<FilteredUsersResultViewModel>> FilterByAge_InvalidInput_TestAction = async () =>
            {
                var query = new GetFilteredUsersQuery(age, "", "");

                var queryResult = await SendAsync(query);

                return queryResult;
            };

            FilterByAge_InvalidInput_TestAction.Should().Throw<BadRequestException>().WithMessage("Parameter age must be greater than 0.");
        }


        [Test]
        [TestCase("diam is")]
        public void ShouldThrowBadRequestExceptionWhenInvalidTagParameterTest(string tag)
        {
            Func<Task<FilteredUsersResultViewModel>> ApplyHTMLTag_InvalidInput_TestAction = async () =>
            {
                var query = new GetFilteredUsersQuery(null, "", tag);

                var queryResult = await SendAsync(query);

                return queryResult;
            };

            ApplyHTMLTag_InvalidInput_TestAction.Should().Throw<BadRequestException>().WithMessage("Parameter tag must contain single words separated by comma.");
        }
    }
}
