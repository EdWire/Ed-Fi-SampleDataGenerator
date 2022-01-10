using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrganization
{
    public class Program
    {
        public string Id { get; set; }
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string ProgramType { get; set; }
        public string ProgramSponsor { get; set; }
        public string EducationOrganizationId { get; set; }
        public string EducationOrganizationLink { get; set; }
        public string EducationOrganizationIdentityId { get; set; }

        public static List<Program> ReadFile()
        {
            var path = $"{CsvHelper.BasePath}{CsvHelper.ProgramPath}";
            return CsvHelper.MapCsvToEntity<Program, ProgramMap>(path);
        }

        public static void WriteFile(List<Program> records)
        {
            var path = $"{CsvHelper.BasePath}{CsvHelper.ProgramPath}";
            CsvHelper.WriteCsv<Program, ProgramMap>(path, records);
        }
    }

    public class ProgramMap: CsvClassMap<Program>
    {
        public ProgramMap()
        {
            Map(x => x.Id).Name("id");
            Map(x => x.ProgramId).Name("ProgramId");
            Map(x => x.ProgramName).Name("ProgramName");
            Map(x => x.ProgramType).Name("ProgramType");
            Map(x => x.ProgramSponsor).Name("ProgramSponsor");
            Map(x => x.EducationOrganizationId).Name("EducationOrganizationReference.id");
            Map(x => x.EducationOrganizationLink).Name("EducationOrganizationReference.ref");
            Map(x => x.EducationOrganizationIdentityId).Name("EducationOrganizationReference.EducationOrganizationIdentity.EducationOrganizationId");
        }
    }
}
