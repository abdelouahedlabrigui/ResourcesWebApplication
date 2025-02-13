using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI.Biography
{
    public class Person
    {
        public int Id { get; set; }
        // Full Name:
        // Include the full name of the person.
        public string FullName { get; set; }
        // Date of Birth:
        // Provide the birth date and place.
        public string Birthday { get; set; }
        // Early Life and Education:
        // Briefly describe their upbringing, family background, and educational history.
        public string EarlyLife { get; set; }
        public string Education { get; set; }
        // Career Beginnings:
        // Detail how they started their career, including any significant early experiences or influences.
        public string CareerBeginning { get; set; }
        // Major Achievements:
        // Highlight key accomplishments, awards, and recognitions.
        public string MajorAchievement { get; set; }
        // Personal Life:
        // Include information about their personal interests, hobbies, and family life.
        public string PersonalLife { get; set; }
        // Philanthropy and Social Contributions:
        // Mention any charitable work or contributions to society.
        public string Philanthropy { get; set; }
        public string SocialContribution { get; set; }
        // Legacy and Impact:
        // Summarize their overall impact and legacy in their field or community.
        public string Legacy { get; set; }
        public string Impact { get; set; }
    }
}