using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv
{
    public static class CsvHelper
    {
       
        private static string basePath = $"{Path.Combine(".","Samples","SampleDataGenerator","DataFiles")}{Path.DirectorySeparatorChar}";    

        public static string BasePath { get => basePath; set => basePath = value; }
        public static string AccountabilityRatingPath { get => ResourceFilePath("EducationOrganization", "AccountabilityRating.csv"); }
        public static string ClassPeriodPath { get => ResourceFilePath("EducationOrganization", "ClassPeriod.csv"); }
        public static string CoursePath { get => ResourceFilePath("EducationOrganization", "Course.csv"); }
        public static string EducationServiceCenterPath { get => ResourceFilePath("EducationOrganization", "EducationServiceCenter.csv"); }
        public static string LocalEducationAgencyPath { get => ResourceFilePath("EducationOrganization", "LocalEducationAgency.csv"); }
        public static string LocationPath { get => ResourceFilePath("EducationOrganization", "Location.csv"); }
        public static string ProgramPath { get => ResourceFilePath("EducationOrganization", "Program.csv"); }
        public static string SchoolPath { get => ResourceFilePath("EducationOrganization", "School.csv"); }

        public static string CalendarPath { get => ResourceFilePath("EducationOrgCalendar", "Calendar.csv"); }
        public static string CalendarDatePath { get => ResourceFilePath("EducationOrgCalendar", "CalendarDate.csv"); }
        public static string GradingPeriodPath { get => ResourceFilePath("EducationOrgCalendar", "GradingPeriod.csv"); }
        public static string SessionPath { get => ResourceFilePath("EducationOrgCalendar", "Session.csv"); }

        public static string CourseOfferingPath { get => ResourceFilePath("MasterSchedule", "CourseOffering.csv"); }
        public static string SectionPath { get => ResourceFilePath("MasterSchedule", "Section.csv"); }


        public static List<T> MapCsvToEntity<T,TU>(string path) where TU : CsvClassMap<T>
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<TU>();
                return csv.GetRecords<T>().ToList();
            }
        }

        public static void WriteCsv<T,TU>(string path, List<T> records) where TU : CsvClassMap<T>
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.RegisterClassMap<TU>();
                csv.WriteRecords(records);
            }
        }

        private static string ResourceFilePath(string folder, string filename)
        {
            return Path.Combine(folder, filename);
        }
    }
}
