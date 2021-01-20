using TWAssessment.Application.Common.Exceptions;
using TWAssessment.Application.Users.Queries;

namespace TWAssessment.Application.Common.Validators
{
    public static class GetFilteredUsersQueryValidator
    {
        public static void ValidateParameters(this GetFilteredUsersQuery request)
        {
            if (request.Age.HasValue && request.Age < 1)
            {
                throw new BadRequestException("Parameter age must be greater than 0.");
            }

            if (!string.IsNullOrEmpty(request.Tag))
            {
                foreach (var tag in request.Tag.Split(","))
                {
                    if (tag.Trim().IndexOf(" ") != -1)
                    {
                        throw new BadRequestException("Parameter tag must contain single words separated by comma.");
                    }
                }
            }
        }
    }
}
