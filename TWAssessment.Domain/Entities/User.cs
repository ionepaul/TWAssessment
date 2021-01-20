using System.Collections.Generic;

namespace TWAssessment.Domain.Entities
{
    public class User
    {
        public string Email { get; set; }

        public int Age { get; set; }

        public IEnumerable<string> Cities { get; set; }

        public string Description { get; set; }
    }
}
