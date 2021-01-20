using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TWAssessment.Domain.Entities;

namespace TWAssessment.Application.Common.Extensions
{
    public static class UserListExtension
    {
        /// <summary>
        /// Filters array of users mock data based on age
        /// </summary>
        /// <param name="users"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        public static IEnumerable<User> FilterByAge(this IEnumerable<User> users, int? age)
        {
            if (age.HasValue)
            {
                return users.Where(u => u.Age == age.Value);
            }

            return users;
        }

        /// <summary>
        /// Filters array of users mock data based on city
        /// </summary>
        /// <param name="users"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public static IEnumerable<User> FilterByCity(this IEnumerable<User> users, string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                return users.Where(u => u.Cities.Contains(city, StringComparer.InvariantCultureIgnoreCase));
            }

            return users;
        }

        /// <summary>
        /// If query parameter tags were found on user description it will incorporate the tag into italic HTML tag <i></i>
        /// </summary>
        /// <param name="users"></param>
        /// <param name="tags">Multiple query parameter tags split by comma</param>
        /// <returns></returns>
        public static IEnumerable<User> ApplyItalicHTMLTagOnDescriptionBasedOnTagParameter(this IEnumerable<User> users, string tags)
        {
            if (!string.IsNullOrEmpty(tags))
            {
                var tagList = tags.Split(",");

                foreach(var user in users)
                {
                    foreach(var tag in tagList)
                    {
                        user.Description = user.Description.Replace(tag.Trim().ToLowerInvariant(), $"<i>{tag.Trim().ToLowerInvariant()}</i>")
                                                           .Replace(tag.Trim().ToUpperInvariant(), $"<i>{tag.Trim().ToUpperInvariant()}</i>")
                                                           .Replace(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tag.Trim().ToLowerInvariant()), $"<i>{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tag.Trim().ToLowerInvariant())}</i>");
                    }
                }
            }

            return users;
        }
    }
}
