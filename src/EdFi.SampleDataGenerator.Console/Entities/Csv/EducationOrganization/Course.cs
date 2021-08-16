using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrganization
{
    public class Course
    {
        public string Id { get; set; }
        public string CourseCode { get; set; }
        public string CareerPathWay { get; set; }
        public string CourseDefinedBy { get; set; }
        public string CourseDescription { get; set; }
        public string CourseGPAApplicability { get; set; }
        public string CourseLevelCharacteristic { get; set; }
        public string CourseTitle { get; set; }
        public string NumberOfParts { get; set; }
        public string AcademicSubject { get; set; }
        public string OfferedGradeLevel { get; set; }
        public string AssigningOrganizationIdentificationCode { get; set; }
        public string CourseIdentificationCode { get; set; }
        public string CourseIdentificationSystem { get; set; }
        public string EducationOrganizationId { get; set; }
        public string EducationOrganizationLink { get; set; }
        public string EducationOrganizationIdentityId { get; set; }
        public string LearningObjectiveId { get; set; }
        public string  LearningObjectiveLink { get; set; }
        public string LearningObjectiveIdentityObjective { get; set; }
        public string LearningStandardId { get; set; }
        public string LearningStandardLink { get; set; }
        public string LearningStandardIdentityId { get; set; }
        public string MinimumAvailableCreditsConversion { get; set; }
        public string MinimumAvailableCreditType { get; set; }
        public string MinimumAvailableCredits1 { get; set; }
        public string MaximumAvailableCreditsConversion { get; set; }
        public string MaximumAvailableCreditType { get; set; }
        public string CompetencyLevel { get; set; }

        public static List<Course> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CoursePath}";
            return CsvHelper.MapCsvToEntity<Course, CourseMap>(path);
        }

        public static void WriteFile(List<Course> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CoursePath}";
            CsvHelper.WriteCsv<Course, CourseMap>(path, records);
        }
    }

    public class CourseMap: CsvClassMap<Course>
    {
        public CourseMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.CourseCode).Name("CourseCode");
            Map(m => m.CareerPathWay).Name("CareerPathway");
            Map(m => m.CourseDefinedBy).Name("CourseDefinedBy");
            Map(m => m.CourseDescription).Name("CourseDescription");
            Map(m => m.CourseGPAApplicability).Name("CourseGPAApplicability");
            Map(m => m.CourseLevelCharacteristic).Name("CourseLevelCharacteristic");
            Map(m => m.CourseTitle).Name("CourseTitle");
            Map(m => m.NumberOfParts).Name("NumberOfParts");
            Map(m => m.AcademicSubject).Name("AcademicSubject");
            Map(m => m.OfferedGradeLevel).Name("OfferedGradeLevel");
            Map(m => m.AssigningOrganizationIdentificationCode).Name("CourseIdentificationCode.AssigningOrganizationIdentificationCode");
            Map(m => m.CourseIdentificationCode).Name("CourseIdentificationCode.IdentificationCode");
            Map(m => m.CourseIdentificationSystem).Name("CourseIdentificationCode.CourseIdentificationSystem");
            Map(m => m.EducationOrganizationId).Name("EducationOrganizationReference.id");
            Map(m => m.EducationOrganizationLink).Name("EducationOrganizationReference.ref");
            Map(m => m.EducationOrganizationIdentityId).Name("EducationOrganizationReference.EducationOrganizationIdentity.EducationOrganizationId");
            Map(m => m.LearningObjectiveId).Name("LearningObjectiveReference.id");
            Map(m => m.LearningObjectiveLink).Name("LearningObjectiveReference.ref");
            Map(m => m.LearningObjectiveIdentityObjective).Name("LearningObjectiveReference.LearningObjectiveIdentity.Objective");
            Map(m => m.LearningStandardId).Name("LearningStandardReference.id");
            Map(m => m.LearningStandardLink).Name("LearningStandardReference.ref");
            Map(m => m.LearningStandardIdentityId).Name("LearningStandardReference.LearningStandardIdentity.LearningStandardId");
            Map(m => m.MinimumAvailableCreditsConversion).Name("MinimumAvailableCredits.CreditConversion");
            Map(m => m.MinimumAvailableCreditType).Name("MinimumAvailableCredits.CreditType");
            Map(m => m.MinimumAvailableCredits1).Name("MinimumAvailableCredits.Credits1");
            Map(m => m.MaximumAvailableCreditsConversion).Name("MaximumAvailableCredits.CreditConversion");
            Map(m => m.MaximumAvailableCreditType).Name("MaximumAvailableCredits.CreditType");
            Map(m => m.CompetencyLevel).Name("CompetencyLevel");
        }
    }
}
