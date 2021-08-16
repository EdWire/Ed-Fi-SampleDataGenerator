using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrgCalendar
{
    public class Session
    {
        public string Id { get; set; }
        public string SessionName { get; set; }
        public string SchoolYear { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string TotalInstructionalDays { get; set; }
        public string Term { get; set; }
        public string SchoolId { get; set; }
        public string SchoolLink { get; set; }
        public string SchoolIdentityId { get; set; }
        public string GradingPeriodId { get; set; }
        public string GradingPeriodLink { get; set; }
        public string GradingPeriodIdentityGradingPeriod { get; set; }
        public string GradingPeriodIdentityPeriodSequence { get; set; }
        public string GradingPeriodIdentitySchoolYear { get; set; }
        public string GradingPeriodIdentitySchoolId { get; set; }

        public static List<Session> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.SessionPath}";
            return CsvHelper.MapCsvToEntity<Session, SessionMap>(path);
        }

        public static void WriteFile(List<Session> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.SessionPath}";
            CsvHelper.WriteCsv<Session, SessionMap>(path, records);
        }
    }

    public class SessionMap : CsvClassMap<Session>
    {
        public SessionMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.SessionName).Name("SessionName");
            Map(m => m.SchoolYear).Name("SchoolYear");
            Map(m => m.BeginDate).Name("BeginDate");
            Map(m => m.EndDate).Name("EndDate");
            Map(m => m.TotalInstructionalDays).Name("TotalInstructionalDays");
            Map(m => m.Term).Name("Term");
            Map(m => m.SchoolId).Name("SchoolReference.id");
            Map(m => m.SchoolLink).Name("SchoolReference.ref");
            Map(m => m.SchoolIdentityId).Name("SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.GradingPeriodId).Name("GradingPeriodReference.id");
            Map(m => m.GradingPeriodLink).Name("GradingPeriodReference.ref");
            Map(m => m.GradingPeriodIdentityGradingPeriod).Name("GradingPeriodReference.GradingPeriodIdentity.GradingPeriod");
            Map(m => m.GradingPeriodIdentityPeriodSequence).Name("GradingPeriodReference.GradingPeriodIdentity.PeriodSequence");
            Map(m => m.GradingPeriodIdentitySchoolYear).Name("GradingPeriodReference.GradingPeriodIdentity.SchoolYear");
            Map(m => m.GradingPeriodIdentitySchoolId).Name("GradingPeriodReference.GradingPeriodIdentity.SchoolReference.SchoolIdentity.SchoolId");
        }
    }
}
