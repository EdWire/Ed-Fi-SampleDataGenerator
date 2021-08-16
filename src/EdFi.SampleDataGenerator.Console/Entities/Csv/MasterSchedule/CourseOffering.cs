using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.MasterSchedule
{
    public class CourseOffering
    {
        public string LocalCourseCode { get; set; }
        public string LocalCourseTitle { get; set; }
        public string InstructionalTimePlanned { get; set; }
        public string SchoolId { get; set; }
        public string SchoolLink { get; set; }
        public string SchoolIdentityId { get; set; }
        public string SessionId { get; set; }
        public string SessionLink { get; set; }
        public string SessionIdentitySchoolYear { get; set; }
        public string SessionIdentitySessionName { get; set; }
        public string SessionIdentitySchoolId { get; set; }
        public string SessionIdentitySchoolLink { get; set; }
        public string SessionIdentitySchoolIdentityId { get; set; }
        public string SessionLookupSchoolYear { get; set; }
        public string SessionLookupSessionName { get; set; }
        public string SessionLookupTerm { get; set; }
        public string SessionLookupSchoolId { get; set; }
        public string SessionLookupSchoolLink { get; set; }
        public string SessionLookupSchoolIdentityId { get; set; }
        public string CourseId { get; set; }
        public string CourseLink { get; set; }
        public string CourseIdentityCourseCode { get; set; }
        public string CourseIdentityEducationOrganizationId { get; set; }
        public string CourseIdentityEducationOrganizationLink { get; set; }
        public string CourseIdentityEducationOrganizationIdentityId { get; set; }
        public string CourseLookupCourseCode { get; set; }
        public string CourseLookupCourseTitle { get; set; }
        public string CourseAssigningOrganizationIdentificationCode { get; set; }
        public string CourseLookupIdentificationCode { get; set; }
        public string CourseLookupIdentificationSystem { get; set; }

        public static List<CourseOffering> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CourseOfferingPath}";
            return CsvHelper.MapCsvToEntity<CourseOffering, CourseOfferingMap>(path);
        }

        public static void WriteFile(List<CourseOffering> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.CourseOfferingPath}";
            CsvHelper.WriteCsv<CourseOffering, CourseOfferingMap>(path, records);
        }
    }

    public class CourseOfferingMap : CsvClassMap<CourseOffering>
    {
        public CourseOfferingMap()
        {
            Map(m => m.LocalCourseCode).Name("LocalCourseCode");
            Map(m => m.LocalCourseTitle).Name("LocalCourseTitle");
            Map(m => m.InstructionalTimePlanned).Name("InstructionalTimePlanned");
            Map(m => m.SchoolId).Name("SchoolReference.id");
            Map(m => m.SchoolLink).Name("SchoolReference.ref");
            Map(m => m.SchoolIdentityId).Name("SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.SessionId).Name("SessionReference.id");
            Map(m => m.SessionLink).Name("SessionReference.ref");
            Map(m => m.SessionIdentitySchoolYear).Name("SessionReference.SessionIdentity.SchoolYear");
            Map(m => m.SessionIdentitySessionName).Name("SessionReference.SessionIdentity.SessionName");
            Map(m => m.SessionIdentitySchoolId).Name("SessionReference.SessionIdentity.SchoolReference.id");
            Map(m => m.SessionIdentitySchoolLink).Name("SessionReference.SessionIdentity.SchoolReference.ref");
            Map(m => m.SessionIdentitySchoolIdentityId).Name("SessionReference.SessionIdentity.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.SessionLookupSchoolYear).Name("SessionReference.SessionLookup.SchoolYear");
            Map(m => m.SessionLookupSessionName).Name("SessionReference.SessionLookup.SessionName");
            Map(m => m.SessionLookupTerm).Name("SessionReference.SessionLookup.Term");
            Map(m => m.SessionLookupSchoolId).Name("SessionReference.SessionLookup.SchoolReference.id");
            Map(m => m.SessionLookupSchoolLink).Name("SessionReference.SessionLookup.SchoolReference.ref");
            Map(m => m.SessionLookupSchoolIdentityId).Name("SessionReference.SessionLookup.SchoolReference.SchoolIdentity.SchoolId");
            Map(m => m.CourseId).Name("CourseReference.id");
            Map(m => m.CourseLink).Name("CourseReference.ref");
            Map(m => m.CourseIdentityCourseCode).Name("CourseReference.CourseIdentity.CourseCode");
            Map(m => m.CourseIdentityEducationOrganizationId).Name("CourseReference.CourseIdentity.EducationOrganizationReference.id");
            Map(m => m.CourseIdentityEducationOrganizationLink).Name("CourseReference.CourseIdentity.EducationOrganizationReference.ref");
            Map(m => m.CourseIdentityEducationOrganizationIdentityId).Name("CourseReference.CourseIdentity.EducationOrganizationReference.EducationOrganizationIdentity.EducationOrganizationId");
            Map(m => m.CourseLookupCourseCode).Name("CourseReference.CourseLookup.CourseCode");
            Map(m => m.CourseLookupCourseTitle).Name("CourseReference.CourseLookup.CourseTitle");
            Map(m => m.CourseAssigningOrganizationIdentificationCode).Name("CourseReference.CourseLookup.CourseIdentificationCode.AssigningOrganizationIdentificationCode");
            Map(m => m.CourseLookupIdentificationCode).Name("CourseReference.CourseLookup.CourseIdentificationCode.IdentificationCode");
            Map(m => m.CourseLookupIdentificationSystem).Name("CourseReference.CourseLookup.CourseIdentificationCode.CourseIdentificationSystem");
        }
    }
}
