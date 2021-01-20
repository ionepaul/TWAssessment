using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TWAssessment.Application.Common.Extensions;
using TWAssessment.Application.UnitTests.Extensions.TestData;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Application.UnitTests.Extensions
{
    [TestFixture]
    public class UserListExtensionsTests
    {
        private IEnumerable<User> _usersList;

        [SetUp]
        public void SetUp()
        {
            _usersList = UsersListBuilder.Users;
        }

        [Test]
        [TestCase(null)]
        public void FilterByAge_EmptyValue_Test(int? age)
        {
            var result = _usersList.FilterByAge(age);

            result.Count().Should().Equals(_usersList.Count());
        }

        [Test]
        [TestCase(30)]
        [TestCase(21)]
        public void FilterByAge_WithValues_Test(int? age)
        {
            var result = _usersList.FilterByAge(age);

            result.Count().Should().Equals(2);
        }

        [Test]
        [TestCase("")]
        public void FilterByCity_EmptyValue_Test(string city)
        {
            var result = _usersList.FilterByCity(city);

            result.Count().Should().Equals(_usersList.Count());
        }

        [Test]
        [TestCase("Sibiu")]
        [TestCase("Bucuresti")]
        [TestCase("Geneva")]
        public void FilterByCity_WithValues_Test(string city)
        {
            var result = _usersList.FilterByCity(city);

            result.Count().Should().Equals(1);
        }


        [Test]
        [TestCase(null, "")]
        public void FilterByAgeAndCity_EmptyValue_Test(int? age, string city)
        {
            var result = _usersList.FilterByAge(age)
                                   .FilterByCity(city);

            result.Count().Should().Equals(_usersList.Count());
        }

        [Test]
        [TestCase(21, "Cluj-Napoca")]
        public void FilterByAgeAndCity_WithValues_Test(int? age, string city)
        {
            var result = _usersList.FilterByAge(age)
                                   .FilterByCity(city);

            result.Count().Should().Equals(1);
        }

        [Test]
        [TestCase("new")]
        public void ApplyHTMLTags_WithSingleValue_Test(string tag)
        {
            var result = _usersList.ApplyItalicHTMLTagOnDescriptionBasedOnTagParameter(tag);

            result.Count().Should().Equals(_usersList.Count());
            result.ElementAt(4).Description.Should().Equals("<i>New</i> york <i>new</i> york");
        }

        [Test]
        [TestCase("description, Another")]
        public void ApplyHTMLTags_WithMultipleValues_Test(string tag)
        {
            var result = _usersList.ApplyItalicHTMLTagOnDescriptionBasedOnTagParameter(tag);

            result.Count().Should().Equals(_usersList.Count());
            result.ElementAt(2).Description.Should().Equals("<i>Another</i> description of this");
            result.ElementAt(1).Description.Should().Equals("Bucuresti <i>description</i> is provided");
        }

        [Test]
        [TestCase(21, "Cluj-Napoca", "description,helsinki")]
        public void FilterByAgeAndCityAndApplyHTMLTags_Test(int? age, string city, string tag)
        {
            var result = _usersList.FilterByAge(age)
                                   .FilterByCity(city)
                                   .ApplyItalicHTMLTagOnDescriptionBasedOnTagParameter(tag);

            result.Should().HaveCount(1);
            result.ElementAt(0).Description.Should().Equals("Lorem ipsum <i>description</i> <i>helsinki</i>");
        }
    }
}
