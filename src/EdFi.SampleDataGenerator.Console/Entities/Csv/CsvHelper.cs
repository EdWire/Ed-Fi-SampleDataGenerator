using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EdFi.SampleDataGenerator.Console.Entities.Csv
{
    public static class CsvHelper
    {
        // EducationOrganization
        public const string BasePath = @".\Samples\SampleDataGenerator\DataFiles\";
        public const string AccountabilityRatingPath = @"EducationOrganization\AccountabilityRating.csv";
        public const string ClassPeriodPath = @"EducationOrganization\ClassPeriod.csv";
        public const string CoursePath = @"EducationOrganization\Course.csv";
        public const string EducationServiceCenterPath = @"EducationOrganization\EducationServiceCenter.csv";
        public const string LocalEducationAgencyPath = @"EducationOrganization\LocalEducationAgency.csv";
        public const string LocationPath = @"EducationOrganization\Location.csv";
        public const string ProgramPath = @"EducationOrganization\Program.csv";
        public const string SchoolPath = @"EducationOrganization\School.csv";

        // EducationOrgCalendar
        public const string CalendarPath = @"EducationOrgCalendar\Calendar.csv";
        public const string CalendarDatePath = @"EducationOrgCalendar\CalendarDate.csv";
        public const string GradingPeriodPath = @"EducationOrgCalendar\GradingPeriod.csv";
        public const string SessionPath = @"EducationOrgCalendar\Session.csv";

        // MasterSchedule
        public const string CourseOfferingPath = @"MasterSchedule\CourseOffering.csv";
        public const string SectionPath = @"MasterSchedule\Section.csv";

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
    }
}
