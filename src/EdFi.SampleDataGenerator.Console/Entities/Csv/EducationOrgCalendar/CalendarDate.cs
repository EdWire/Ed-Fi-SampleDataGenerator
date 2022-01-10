using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrgCalendar
{
    public class CalendarDate
    {
        public string Id { get; set; }
        public string CalendarEvent { get; set; }
        public string Date { get; set; }
        public string CalendarId { get; set; }
        public string CalendarLink { get; set; }
        public string CalendarCode { get; set; }
        public string SchoolId { get; set; }
        public string SchoolLink { get; set; }
        public string SchoolIdentityId { get; set; }
        public string SchoolYear { get; set; }

        public static List<CalendarDate> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CalendarDatePath}";
            return CsvHelper.MapCsvToEntity<CalendarDate, CalendarDateMap>(path);
        }

        public static void WriteFile(List<CalendarDate> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CalendarDatePath}";
            CsvHelper.WriteCsv<CalendarDate, CalendarDateMap>(path, records);
        }
    }

    public class CalendarDateMap : CsvClassMap<CalendarDate>
    {
        public CalendarDateMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.CalendarEvent).Name("CalendarEvent");
            Map(m => m.Date).Name("Date");
            Map(m => m.CalendarId).Name("CalendarReference.id");
            Map(m => m.CalendarLink).Name("CalendarReference.ref");
            Map(m => m.CalendarCode).Name("CalendarReference.CalendarIdentity.CalendarCode");
            Map(m => m.SchoolId).Name("CalendarReference.CalendarIdentity.SchoolReference.id");
            Map(m => m.SchoolLink).Name("CalendarReference.CalendarIdentity.SchoolReference.ref");
            Map(m => m.SchoolIdentityId).Name("CalendarReference.CalendarIdentity.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.SchoolYear).Name("CalendarReference.CalendarIdentity.SchoolYear");
        }
    }
}
