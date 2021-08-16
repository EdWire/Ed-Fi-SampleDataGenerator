using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrganization
{
    public class ClassPeriod
    {
        public string ClassPeriodName { get; set; }
        public string SchoolId { get; set; }
        public string SchoolLink { get; set; }
        public string SchoolIdentityId { get; set; }

        public static List<ClassPeriod> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.ClassPeriodPath}";
            return CsvHelper.MapCsvToEntity<ClassPeriod, ClassPeriodMap>(path);
        }

        public static void WriteFile(List<ClassPeriod> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.ClassPeriodPath}";
            CsvHelper.WriteCsv<ClassPeriod, ClassPeriodMap>(path, records);
        }
    }

    public class ClassPeriodMap : CsvClassMap<ClassPeriod>
    {
        public ClassPeriodMap()
        {
            Map(m => m.ClassPeriodName).Name("ClassPeriodName");
            Map(m => m.SchoolId).Name("SchoolReference.id");
            Map(m => m.SchoolLink).Name("SchoolReference.ref");
            Map(m => m.SchoolIdentityId).Name("SchoolReference.SchoolIdentity.SchoolId");
        }
    }
}
