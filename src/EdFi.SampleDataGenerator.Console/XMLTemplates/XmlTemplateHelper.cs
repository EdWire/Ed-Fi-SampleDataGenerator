
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EdFi.SampleDataGenerator.Console.XMLTemplates
{
    public static class XmlTemplateHelper
    {
        public static List<string> ValidEthnicitiesRaces = new List<string> { "Hispanic", "Asian", "Black", "AmericanIndianAlaskanNative", "NativeHawaiianPacificIslander", "White" };
        public const string BasePath = @".\XMLTemplates\";
        public const string WriteFilePath = @".\Samples\SampleDataGenerator\NewGeneratedConfig.xml";

        public const string ConfigStart = "StartConfigTemplate.txt";
        public const string ConfigEnd = "</SampleDataGeneratorConfig>";

        public const string DistrictStart = "StartDistrictConfigTemplate.txt";
        public const string DistrictEnd = "	</DistrictProfile>";

        public const string SchoolStart = "StartSchoolConfigTemplate.txt";
        public const string SchoolEnd = "		</SchoolProfile>";

        public const string StaffProfile = "StaffProfileConfigTemplate.txt";

        public const string StartStudentProfile = "StartStudentProfileConfigTemplate.txt";
        public const string StartStudentProfileEconomic = "StartStudentProfileEconomicTemplate.txt";
        public const string StartStudentHomeless = "StartStudentProfileHomelessTemplate.txt";
        public const string StudentProfileEthnicityOptionValue = "StudentProfileEthnicityOptionValue.txt";
        public const string EndStudentProfile = "EndStudentProfileConfigTemplate.txt";

        public const string GraduationPlans = "GraduationPlanConfigTemplates.txt";

        public const string Grade1File = "Grade1ConfigTemplate.txt";
        public const string Grade2File = "Grade2ConfigTemplate.txt";
        public const string Grade3File = "Grade3ConfigTemplate.txt";

        public const string Grade4File = "Grade4ConfigTemplate.txt";
        public const string Grade5File = "Grade5ConfigTemplate.txt";
        public const string Grade6File = "Grade6ConfigTemplate.txt";

        public const string Grade7File = "Grade7ConfigTemplate.txt";
        public const string Grade8File = "Grade8ConfigTemplate.txt";

        public const string Grade9File = "Grade9ConfigTemplate.txt";
        public const string Grade10File = "Grade10ConfigTemplate.txt";
        public const string Grade11File = "Grade11ConfigTemplate.txt";
        public const string Grade12File = "Grade12ConfigTemplate.txt";

        public static void BuildConfigFile(Entities.District district) {
            var fullFileString = "";
            fullFileString += ReadFile(ConfigStart);
            fullFileString += ReadFile(DistrictStart)
                .Replace("{{district.name}}", district.Name)
                .Replace("{{district.state}}", district.StateAbr)
                .Replace("{{district.city}}", district.City)
                .Replace("{{district.county}}", district.State)
                .Replace("{{district.areaCode}}", district.AreaCode)
                .Replace("{{district.postalCode}}", district.PostalCode);

            var studentProfilesString = "";
            foreach(var school in district.Schools)
            {
                fullFileString += ReadFile(SchoolStart)
                    .Replace("{{school.name}}", school.Name)
                    .Replace("{{school.id}}", school.Id);

                foreach(var grade in school.GradeLevels) // Format expected for grades : Grade 1, Grade 2, Grade 3.....
                {
                    fullFileString += ReadGradeFile(GetIntFromGrade(grade.Grade))
                        .Replace("{{grade.students}}", grade.TotalStudents.ToString())
                        .Replace("{{grade.profile}}", $"{school.Id}{grade.Grade}")
                        .Replace("{{grade.transfersIn}}", $"{Decimal.ToInt32(Math.Ceiling(grade.TotalStudents * 0.05m))}")
                        .Replace("{{grade.transfersOut}}", $"{Decimal.ToInt32(Math.Ceiling(grade.TotalStudents * 0.02m))}");

                    studentProfilesString += ReadFile(StartStudentProfile)
                        .Replace("{{profile.name}}", $"{school.Id}{grade.Grade}");

                    var racePercentages = new Dictionary<string, decimal>();
                    foreach (var race in ValidEthnicitiesRaces.Where(x => x != "White"))
                    {
                        var count = grade.Ethnicities.Where(x => x.Name == race).Sum(x => x.StudentCount);
                        var percentage = Math.Round((decimal)count / grade.TotalStudents, 2);

                        if (percentage > 0)
                        {
                            racePercentages.Add(race, percentage);
                        }
                    }
                    
                    var currentTotalPercentage = racePercentages.Values.Sum();
                    if (currentTotalPercentage >= 1.0m)
                    {
                        const decimal expectedTotalRacePercentage = 0.95m;
                        var desiredPercentage = expectedTotalRacePercentage / currentTotalPercentage;
                        var races = new List<string>(racePercentages.Keys);
                        foreach (var race in races)
                        {
                            var actualValue = racePercentages[race];
                            var adjustedValue = actualValue * desiredPercentage;
                            racePercentages[race] = adjustedValue;
                        }
                        currentTotalPercentage = racePercentages.Values.Sum();
                    }

                    var whitePercentage = 1.0m - currentTotalPercentage;

                    foreach (var racePercentage in racePercentages)
                    {
                        studentProfilesString += ReadFile(StudentProfileEthnicityOptionValue)
                                .Replace("{{option.name}}", racePercentage.Key)
                                .Replace("{{option.value}}", racePercentage.Value.ToString(CultureInfo.InvariantCulture));
                    }

                    studentProfilesString += ReadFile(StudentProfileEthnicityOptionValue)
                        .Replace("{{option.name}}", "White")
                            .Replace("{{option.value}}", whitePercentage.ToString(CultureInfo.InvariantCulture));


                    var maleCount = grade.Ethnicities.Where(x => x.Sex == "Male").Sum(x => x.StudentCount);
                    var malePercentage = Math.Round((decimal)maleCount / grade.TotalStudents, 2);
                    malePercentage = malePercentage >= 1 ? 0.95m : malePercentage == 0.0m ? 0.05m : malePercentage;
                    var femalePercentage = 1 - malePercentage;

                    studentProfilesString += ReadFile(StartStudentProfileEconomic)
                        .Replace("{{profile.male}}", $"{malePercentage}")
                        .Replace("{{profile.female}}", $"{femalePercentage}");
                    var random = new Random();
                    
                    foreach (var race in ValidEthnicitiesRaces)
                    {
                        int randomNumber = random.Next(10, 30);

                        studentProfilesString += ReadFile(StudentProfileEthnicityOptionValue)
                                .Replace("{{option.name}}", race)
                                .Replace("{{option.value}}", $"0.{randomNumber}");
                    }

                    studentProfilesString += ReadFile(StartStudentHomeless);

                    foreach (var race in ValidEthnicitiesRaces)
                    {
                        studentProfilesString += ReadFile(StudentProfileEthnicityOptionValue)
                                .Replace("{{option.name}}", race)
                                .Replace("{{option.value}}", "0.01");
                    }

                    studentProfilesString += ReadFile(EndStudentProfile);
                }

                fullFileString += ReadFile(StaffProfile);
                fullFileString += $"{SchoolEnd}\n";
            }

            fullFileString += $"\n{DistrictEnd}\n";
            fullFileString += studentProfilesString;
            fullFileString += ReadFile(GraduationPlans);
            fullFileString += $"{ConfigEnd}";

            try
            {
                File.WriteAllText(WriteFilePath, fullFileString);
            }
            catch (Exception)
            {
                System.Console.WriteLine("En error occurred while writing the config file.");

                throw;
            }
        }

        public static int GetIntFromGrade(string gradeName)
        {
            try
            {
                return Int32.Parse(gradeName.Trim().Split(' ')[1]);
            }
            catch (Exception)
            {
                System.Console.WriteLine("The grade isn't in the expected format please review your data. Expected format: 'Grade #'.");
                throw;
            }
        }

        public static string ReadGradeFile(int grade)
        {
            switch (grade)
            {
                case 1:
                    return ReadFile(Grade1File);
                case 2:
                    return ReadFile(Grade2File);
                case 3:
                    return ReadFile(Grade3File);
                case 4:
                    return ReadFile(Grade4File);
                case 5:
                    return ReadFile(Grade5File);
                case 6:
                    return ReadFile(Grade6File);
                case 7:
                    return ReadFile(Grade7File);
                case 8:
                    return ReadFile(Grade8File);
                case 9:
                    return ReadFile(Grade9File);
                case 10:
                    return ReadFile(Grade10File);
                case 11:
                    return ReadFile(Grade11File);
                case 12:
                    return ReadFile(Grade12File);
                default:
                    throw new FormatException("The grade isn't in the expected format please review your data. Expected format: 'Grade #'.");
            }
        }

        public static string ReadFile(string fileName)
        {
            try
            {
                return File.ReadAllText($"{BasePath}{fileName}");
            }
            catch (Exception)
            {
                System.Console.WriteLine($"There was a issue while reading the file: {fileName}");

                throw;
            }
        }

    }
}
