using CsvHelper.Configuration;
using System.Collections.Generic;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv.EducationOrganization
{
    public class School
    {
        public string Id { get; set; }
        public string NameOfInstitution { get; set; }
        public string ShortNameOfInstitution { get; set; }
        public string EducationOrganizationCategory { get; set; }
        public string OperationalStatus { get; set; }
        public string StateOrganizationId { get; set; }
        public string WebSite { get; set; }
        public string CharterStatus { get; set; }
        public string SchoolCategory { get; set; }
        public string SchoolType { get; set; }
        public string SchoolId { get; set; }
        public string TitleIPartASchoolDesignation { get; set; }
        public string EducationOrganizationIdentificationCode { get; set; }
        public string EducationOrganizationIdentificationSystem { get; set; }
        public string AddressType { get; set; }
        public string StreetNumberName { get; set; }
        public string BuildingSiteNumber { get; set; }
        public string ApartmentRoomSuiteNumber { get; set; }
        public string City { get; set; }
        public string StateAbbreviation { get; set; }
        public string NameOfCounty { get; set; }
        public string PostalCode { get; set; }
        public string CountyFIPSCode { get; set; }
        public string InstitutionTelephoneNumberType { get; set; }
        public string TelephoneNumber { get; set; }
        public string AdministrativeFundingControl { get; set; }
        public string GradeLevel { get; set; }
        public string LocalEducationAgencyId { get; set; }
        public string LocalEducationAgencyLink { get; set; }
        public string LocalEducationAgencyIdentityId { get; set; }

        public static List<School> ReadFile()
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.SchoolPath}";
            return CsvHelper.MapCsvToEntity<School, SchoolMap>(path);
        }

        public static void WriteFile(List<School> records)
        {
            string path = $"{CsvHelper.BasePath}{CsvHelper.SchoolPath}";
            CsvHelper.WriteCsv<School, SchoolMap>(path, records);
        }
    }

    public class SchoolMap : CsvClassMap<School>
    {
        public SchoolMap()
        {
            Map(x => x.Id).Name("id");
            Map(x => x.NameOfInstitution).Name("NameOfInstitution");
            Map(x => x.ShortNameOfInstitution).Name("ShortNameOfInstitution");
            Map(x => x.EducationOrganizationCategory).Name("EducationOrganizationCategory");
            Map(x => x.OperationalStatus).Name("OperationalStatus");
            Map(x => x.StateOrganizationId).Name("StateOrganizationId");
            Map(x => x.WebSite).Name("WebSite");
            Map(x => x.CharterStatus).Name("CharterStatus");
            Map(x => x.SchoolCategory).Name("SchoolCategory");
            Map(x => x.SchoolType).Name("SchoolType");
            Map(x => x.SchoolId).Name("SchoolId");
            Map(x => x.TitleIPartASchoolDesignation).Name("TitleIPartASchoolDesignation");
            Map(x => x.EducationOrganizationIdentificationCode).Name("EducationOrganizationIdentificationCode.IdentificationCode");
            Map(x => x.EducationOrganizationIdentificationSystem).Name("EducationOrganizationIdentificationCode.EducationOrganizationIdentificationSystem");
            Map(m => m.AddressType).Name("Address.AddressType");
            Map(m => m.StreetNumberName).Name("Address.StreetNumberName");
            Map(m => m.BuildingSiteNumber).Name("Address.BuildingSiteNumber");
            Map(m => m.ApartmentRoomSuiteNumber).Name("Address.ApartmentRoomSuiteNumber");
            Map(m => m.City).Name("Address.City");
            Map(m => m.StateAbbreviation).Name("Address.StateAbbreviation");
            Map(m => m.NameOfCounty).Name("Address.NameOfCounty");
            Map(m => m.PostalCode).Name("Address.PostalCode");
            Map(m => m.CountyFIPSCode).Name("Address.CountyFIPSCode");
            Map(m => m.InstitutionTelephoneNumberType).Name("InstitutionTelephone.InstitutionTelephoneNumberType");
            Map(m => m.TelephoneNumber).Name("InstitutionTelephone.TelephoneNumber");
            Map(m => m.AdministrativeFundingControl).Name("AdministrativeFundingControl");
            Map(m => m.GradeLevel).Name("GradeLevel");
            Map(m => m.LocalEducationAgencyId).Name("LocalEducationAgencyReference.id");
            Map(m => m.LocalEducationAgencyLink).Name("LocalEducationAgencyReference.ref");
            Map(m => m.LocalEducationAgencyIdentityId).Name("LocalEducationAgencyReference.LocalEducationAgencyIdentity.LocalEducationAgencyId");
        }
    }
}
