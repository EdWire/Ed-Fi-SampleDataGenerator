using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities
{
    public class School
    {
        public School()
        {
            GradeLevels = new List<GradeLevel>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateAbr { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }
        public string Level { get; set; }
        public string LeaId { get; set; }
        public List<GradeLevel> GradeLevels { get; set; }
    }
}
