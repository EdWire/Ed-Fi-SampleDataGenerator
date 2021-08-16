using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrgCalendar
{
    public class Calendar
    {
        public string Id { get; set; }
        public string CalendarCode { get; set; }
        public string SchoolYear { get; set; }
        public string CalendarType { get; set; }
        public string SchoolId { get; set; }
        public string SchoolLink { get; set; }
        public string SchoolIdentityId { get; set; }

        public static List<Calendar> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CalendarPath}";
            return CsvHelper.MapCsvToEntity<Calendar, CalendarMap>(path);
        }

        public static void WriteFile(List<Calendar> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CalendarPath}";
            CsvHelper.WriteCsv<Calendar, CalendarMap>(path, records);
        }
    }

    public class CalendarMap : CsvClassMap<Calendar>
    {
        public CalendarMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.CalendarCode).Name("CalendarCode");
            Map(m => m.SchoolYear).Name("SchoolYear");
            Map(m => m.CalendarType).Name("CalendarType");
            Map(m => m.SchoolId).Name("SchoolReference.id");
            Map(m => m.SchoolLink).Name("SchoolReference.ref");
            Map(m => m.SchoolIdentityId).Name("SchoolReference.SchoolIdentity.SchoolId");
        }
    }
}
