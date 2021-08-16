using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrganization
{
    public class Location
    {
        public string Id { get; set; }
        public string ClassroomIdentificationCode { get; set; }
        public string MaximumNumberOfSeats { get; set; }
        public string OptimalNumberOfSeats { get; set; }
        public string SchoolId { get; set; }
        public string SchoolLink { get; set; }
        public string SchoolIdentityId { get; set; }
        public static List<Location> ReadFile()
        {
            var path = $"{CsvHelper.BasePath}{CsvHelper.LocationPath}";
            return CsvHelper.MapCsvToEntity<Location, LocationMap>(path);
        }

        public static void WriteFile(List<Location> records)
        {
            var path = $"{CsvHelper.BasePath}{CsvHelper.LocationPath}";
            CsvHelper.WriteCsv<Location, LocationMap>(path, records);
        }
    }

    public class LocationMap : CsvClassMap<Location>
    {
        public LocationMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.ClassroomIdentificationCode).Name("ClassroomIdentificationCode");
            Map(m => m.MaximumNumberOfSeats).Name("MaximumNumberOfSeats");
            Map(m => m.OptimalNumberOfSeats).Name("OptimalNumberOfSeats");
            Map(m => m.SchoolId).Name("SchoolReference.id");
            Map(m => m.SchoolLink).Name("SchoolReference.ref");
            Map(m => m.SchoolIdentityId).Name("SchoolReference.SchoolIdentity.SchoolId");
        }
    }
}
