using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities
{
    public class GradeLevel
    {
        public GradeLevel()
        {
            Ethnicities = new List<Ethnicity>();
        }
        public string SchoolId { get; set; }
        public string Grade { get; set; }
        public List<Ethnicity> Ethnicities { get; set; }
        public long TotalStudents { get; set; }
    }
}
