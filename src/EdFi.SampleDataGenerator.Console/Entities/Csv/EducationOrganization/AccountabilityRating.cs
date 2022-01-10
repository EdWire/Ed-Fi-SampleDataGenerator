using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrganization
{
    public class AccountabilityRating
    {
        public string Rating { get; set; }
        public string RatingDate { get; set; }
        public string RatingOrganization { get; set; }
        public string RatingProgram { get; set; }
        public string RatingTitle { get; set; }
        public string SchoolYear { get; set; }
        public string EducationOrganizationId { get; set; }
        public string EducationOrganizationLink { get; set; }
        public string EducationOrganizationIdentityId { get; set; }

        public static List<AccountabilityRating> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.AccountabilityRatingPath}";
            return CsvHelper.MapCsvToEntity<AccountabilityRating, AccountabilityRatingMap>(path);
        }

        public static void WriteFile(List<AccountabilityRating> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.AccountabilityRatingPath}";
            CsvHelper.WriteCsv<AccountabilityRating, AccountabilityRatingMap>(path, records);
        }
    }

    public class AccountabilityRatingMap : CsvClassMap<AccountabilityRating>
    {
        public AccountabilityRatingMap()
        {
            Map(m => m.Rating).Name("Rating");
            Map(m => m.RatingDate).Name("RatingDate");
            Map(m => m.RatingOrganization).Name("RatingOrganization");
            Map(m => m.RatingProgram).Name("RatingProgram");
            Map(m => m.RatingTitle).Name("RatingTitle");
            Map(m => m.SchoolYear).Name("SchoolYear");
            Map(m => m.EducationOrganizationId).Name("EducationOrganizationReference.id");
            Map(m => m.EducationOrganizationLink).Name("EducationOrganizationReference.ref");
            Map(m => m.EducationOrganizationIdentityId).Name("EducationOrganizationReference.EducationOrganizationIdentity.EducationOrganizationId");
        }
    }
}
