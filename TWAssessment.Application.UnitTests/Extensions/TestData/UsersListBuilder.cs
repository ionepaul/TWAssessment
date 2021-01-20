using System.Collections.Generic;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Application.UnitTests.Extensions.TestData
{
    public static class UsersListBuilder
    {
        public static IEnumerable<User> Users = new List<User>
        {
            new User
            {
                Email = "test1@email.com",
                Age = 21,
                Cities = new List<string>
                {
                    "Oslo",
                    "Helsinki",
                    "Cluj-Napoca"
                },
                Description = "Lorem ipsum description helsinki"
            },
            new User
            {
                Email = "test2@email.com",
                Age = 30,
                Cities = new List<string>
                {
                    "Bucuresti",
                    "Sibiu",
                    "Cluj-Napoca"
                },
                Description = "Bucuresti description is provided"
            },
            new User
            {
                Email = "test3@email.com",
                Age = 30,
                Cities = new List<string>
                {
                    "Miami",
                    "Geneva",
                    "Cluj-Napoca"
                },
                Description = "Another description of this"
            },
            new User
            {
                Email = "test4@email.com",
                Age = 43,
                Cities = new List<string>
                {
                    "Oslo",
                    "Miami",
                    "Paris"
                },
                Description = "Best description ever"
            },
            new User
            {
                Email = "test5@email.com",
                Age = 21,
                Cities = new List<string>
                {
                    "Oslo",
                    "New York",
                    "Paris"
                },
                Description = "New york new york"
            }
        };
    }
}
