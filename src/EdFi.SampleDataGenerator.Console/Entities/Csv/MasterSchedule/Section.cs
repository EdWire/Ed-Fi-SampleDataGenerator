using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.MasterSchedule
{
    public class Section
    {
        public string SectionIdentifier { get; set; }
        public string SequenceOfCourse { get; set; }
        public string EducationalEnvironment { get; set; }
        public string InstructionLanguage { get; set; }
        public string AvailableCreditsCreditConversion { get; set; }
        public string AvailableCreditsCreditType { get; set; }
        public string AvailableCreditsCredits1 { get; set; }
        public string CourseOfferingId { get; set; }
        public string CourseOfferingLink { get; set; }
        public string CourseOfferingIdentityLocalCourseCode { get; set; }
        public string CourseOfferingIdentitySessionId { get; set; }
        public string CourseOfferingIdentitySessionLink { get; set; }
        public string CourseOfferingIdentitySessionIdentitySchoolYear { get; set; }
        public string CourseOfferingIdentitySessionIdentitySessionName { get; set; }
        public string CourseOfferingIdentitySessionIdentitySchoolId { get; set; }
        public string CourseOfferingIdentitySessionIdentitySchoolLink { get; set; }
        public string CourseOfferingIdentitySessionIdentitySchoolIdentityId { get; set; }
        public string CourseOfferingIdentitySessionLookupSchoolYear { get; set; }
        public string CourseOfferingIdentitySessionLookupSessionName { get; set; }
        public string CourseOfferingIdentitySessionLookupTerm { get; set; }
        public string CourseOfferingIdentitySessionLookupSchoolId { get; set; }
        public string CourseOfferingIdentitySessionLookupSchoolLink { get; set; }
        public string CourseOfferingIdentitySessionLookupSchoolIdentityId { get; set; }
        public string CourseOfferingIdentitySchoolId { get; set; }
        public string CourseOfferingIdentitySchoolLink { get; set; }
        public string CourseOfferingIdentitySchoolIdentityId { get; set; }
        public string LocationSchoolId { get; set; }
        public string LocationSchoolLink { get; set; }
        public string LocationSchoolIdentityId { get; set; }
        public string LocationId { get; set; }
        public string LocationIdentityClassroomIdentificationCode { get; set; }
        public string LocationIdentitySchoolId { get; set; }
        public string LocationIdentitySchoolLink { get; set; }
        public string LocationIdentitySchoolIdentityId { get; set; }
        public string ClassPeriodId { get; set; }
        public string ClassPeriodLink { get; set; }
        public string ClassPeriodIdentityClassPeriodName { get; set; }
        public string ClassPeriodIdentitySchoolId { get; set; }
        public string ClassPeriodIdentitySchoolLink { get; set; }
        public string ClassPeriodIdentitySchoolIdentityId { get; set; }
        public string ProgramIdentityProgramName { get; set; }
        public string ProgramIdentityProgramType { get; set; }
        public string ProgramIdentityEducationOrganizationId { get; set; }
        public string ProgramIdentityEducationOrganizationLink { get; set; }
        public string ProgramIdentityEducationOrganizationIdentityId { get; set; }

        public static List<Section> ReadFile()
        {
            var path = $"{CsvHelper.BasePath}{CsvHelper.SectionPath}";
            return CsvHelper.MapCsvToEntity<Section, SectionMap>(path);
        }

        public static void WriteFile(List<Section> records)
        {
            var path = $"{CsvHelper.BasePath}{CsvHelper.SectionPath}";
            CsvHelper.WriteCsv<Section, SectionMap>(path, records);
        }
    }

    public class SectionMap : CsvClassMap<Section>
    {
        public SectionMap()
        {
            Map(m => m.SectionIdentifier).Name("SectionIdentifier");
            Map(m => m.SequenceOfCourse).Name("SequenceOfCourse");
            Map(m => m.EducationalEnvironment).Name("EducationalEnvironment");
            Map(m => m.InstructionLanguage).Name("InstructionLanguage");
            Map(m => m.AvailableCreditsCreditConversion).Name("AvailableCredits.CreditConversion");
            Map(m => m.AvailableCreditsCreditType).Name("AvailableCredits.CreditType");
            Map(m => m.AvailableCreditsCredits1).Name("AvailableCredits.Credits1");
            Map(m => m.CourseOfferingId).Name("CourseOfferingReference.id");
            Map(m => m.CourseOfferingLink).Name("CourseOfferingReference.ref");
            Map(m => m.CourseOfferingIdentityLocalCourseCode).Name("CourseOfferingReference.CourseOfferingIdentity.LocalCourseCode");
            Map(m => m.CourseOfferingIdentitySessionId).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.id");
            Map(m => m.CourseOfferingIdentitySessionLink).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.ref");
            Map(m => m.CourseOfferingIdentitySessionIdentitySchoolYear).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionIdentity.SchoolYear");
            Map(m => m.CourseOfferingIdentitySessionIdentitySessionName).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionIdentity.SessionName");
            Map(m => m.CourseOfferingIdentitySessionIdentitySchoolId).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionIdentity.SchoolReference.id");
            Map(m => m.CourseOfferingIdentitySessionIdentitySchoolLink).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionIdentity.SchoolReference.ref");
            Map(m => m.CourseOfferingIdentitySessionIdentitySchoolIdentityId).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionIdentity.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.CourseOfferingIdentitySessionLookupSchoolYear).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionLookup.SchoolYear");
            Map(m => m.CourseOfferingIdentitySessionLookupSessionName).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionLookup.SessionName");
            Map(m => m.CourseOfferingIdentitySessionLookupTerm).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionLookup.Term");
            Map(m => m.CourseOfferingIdentitySessionLookupSchoolId).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionLookup.SchoolReference.id");
            Map(m => m.CourseOfferingIdentitySessionLookupSchoolLink).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionLookup.SchoolReference.ref");
            Map(m => m.CourseOfferingIdentitySessionLookupSchoolIdentityId).Name("CourseOfferingReference.CourseOfferingIdentity.SessionReference.SessionLookup.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.CourseOfferingIdentitySchoolId).Name("CourseOfferingReference.CourseOfferingIdentity.SchoolReference.id");
            Map(m => m.CourseOfferingIdentitySchoolLink).Name("CourseOfferingReference.CourseOfferingIdentity.SchoolReference.ref");
            Map(m => m.CourseOfferingIdentitySchoolIdentityId).Name("CourseOfferingReference.CourseOfferingIdentity.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.LocationSchoolId).Name("LocationSchoolReference.id");
            Map(m => m.LocationSchoolLink).Name("LocationSchoolReference.ref");
            Map(m => m.LocationSchoolIdentityId).Name("LocationSchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.LocationId).Name("LocationReference.id");
            Map(m => m.LocationIdentityClassroomIdentificationCode).Name("LocationReference.LocationIdentity.ClassroomIdentificationCode");
            Map(m => m.LocationIdentitySchoolId).Name("LocationReference.LocationIdentity.SchoolReference.id");
            Map(m => m.LocationIdentitySchoolLink).Name("LocationReference.LocationIdentity.SchoolReference.ref");
            Map(m => m.LocationIdentitySchoolIdentityId).Name("LocationReference.LocationIdentity.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.ClassPeriodId).Name("ClassPeriodReference.id");
            Map(m => m.ClassPeriodLink).Name("ClassPeriodReference.ref");
            Map(m => m.ClassPeriodIdentityClassPeriodName).Name("ClassPeriodReference.ClassPeriodIdentity.ClassPeriodName");
            Map(m => m.ClassPeriodIdentitySchoolId).Name("ClassPeriodReference.ClassPeriodIdentity.SchoolReference.id");
            Map(m => m.ClassPeriodIdentitySchoolLink).Name("ClassPeriodReference.ClassPeriodIdentity.SchoolReference.ref");
            Map(m => m.ClassPeriodIdentitySchoolIdentityId).Name("ClassPeriodReference.ClassPeriodIdentity.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.ProgramIdentityProgramName).Name("ProgramReference.ProgramIdentity.ProgramName");
            Map(m => m.ProgramIdentityProgramType).Name("ProgramReference.ProgramIdentity.ProgramType");
            Map(m => m.ProgramIdentityEducationOrganizationId).Name("ProgramReference.ProgramIdentity.EducationOrganizationReference.id");
            Map(m => m.ProgramIdentityEducationOrganizationLink).Name("ProgramReference.ProgramIdentity.EducationOrganizationReference.ref");
            Map(m => m.ProgramIdentityEducationOrganizationIdentityId).Name("ProgramReference.ProgramIdentity.EducationOrganizationReference.EducationOrganizationIdentity.EducationOrganizationId");
        }
    }
}
