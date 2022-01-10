using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrgCalendar
{
    public class GradingPeriod
    {
        public string Id { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string TotalInstructionalDays { get; set; }
        public string PeriodSequence { get; set; }
        public string GradingPeriod1 { get; set; }
        public string SchoolId { get; set; }
        public string SchoolLink { get; set; }
        public string SchoolIdentityId { get; set; }
        public string SchoolYear { get; set; }

        public static List<GradingPeriod> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.GradingPeriodPath}";
            return CsvHelper.MapCsvToEntity<GradingPeriod, GradingPeriodMap>(path);
        }

        public static void WriteFile(List<GradingPeriod> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.GradingPeriodPath}";
            CsvHelper.WriteCsv<GradingPeriod, GradingPeriodMap>(path, records);
        }

    }

    public class GradingPeriodMap : CsvClassMap<GradingPeriod>
    {
        public GradingPeriodMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.BeginDate).Name("BeginDate");
            Map(m => m.EndDate).Name("EndDate");
            Map(m => m.TotalInstructionalDays).Name("TotalInstructionalDays");
            Map(m => m.PeriodSequence).Name("PeriodSequence");
            Map(m => m.GradingPeriod1).Name("GradingPeriod1");
            Map(m => m.SchoolId).Name("SchoolReference.id");
            Map(m => m.SchoolLink).Name("SchoolReference.ref");
            Map(m => m.SchoolIdentityId).Name("SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.SchoolYear).Name("SchoolYear");
        }
    }
}
