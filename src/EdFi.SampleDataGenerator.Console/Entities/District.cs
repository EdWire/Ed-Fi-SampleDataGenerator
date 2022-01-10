using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities
{
    public class District
    {
        public District()
        {
            Schools = new List<School>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateAbr { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }
        public List<School> Schools { get; set; }
    }
}
