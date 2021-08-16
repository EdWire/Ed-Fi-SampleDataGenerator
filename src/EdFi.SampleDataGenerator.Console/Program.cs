using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using EdFi.SampleDataGenerator.Console.Config;
using EdFi.SampleDataGenerator.Console.Entities.Csv;
using EdFi.SampleDataGenerator.Console.XMLTemplates;
using EdFi.SampleDataGenerator.Core.Config.Xml;
using EdFi.SampleDataGenerator.Core.DataGeneration.Coordination;
using log4net;
using log4net.Config;

namespace EdFi.SampleDataGenerator.Console
{
    class Program
    {
        private static ILog _log;

        static void Main(string[] args)
        {
            PrintCopyrightMessageToConsole();

            InitializeApp();

            try
            {

                var commandLineConfig = ParseCommandLine(args);

                if (commandLineConfig.ConfigurationType == ConfigurationType.Database &&
                    !string.IsNullOrEmpty(commandLineConfig.NCESDatabasePath) && !string.IsNullOrEmpty(commandLineConfig.NCESDistrictId))
                {
                    BuildXmlConfigFromDb(commandLineConfig.NCESDatabasePath, commandLineConfig.NCESDistrictId);
                    commandLineConfig.ConfigXmlPath = XmlTemplateHelper.WriteFilePath;
                }

                Directory.CreateDirectory(commandLineConfig.OutputPath);

                var sampleDataGeneratorConfig = LoadXmlConfig(commandLineConfig);
                Run(sampleDataGeneratorConfig);
            }
            catch (Exception e)
            {
                _log.Fatal(e);
            }

#if DEBUG
            System.Console.Write("Press any key to continue...");
            System.Console.ReadKey();
#endif
        }

        private static void BuildXmlConfigFromDb(string dbPath, string districtId)
        {
            var invalidGradeLevels = new List<string> { "Grade 1", "Grade 2", "Grade 3" };
            var invalidEthnicities = new List<string> { "No Category Codes", "Not Specified" };
            System.Console.WriteLine("Building the config file from db....");

            try
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder
                {
                    DataSource = dbPath,
                    Pooling = true,
                    BusyTimeout = 10
                };
                var sqliteConnection = new SQLiteConnection(builder.ToString());
                sqliteConnection.Open();
                var sqliteCommand = new SQLiteCommand($"SELECT * FROM lea WHERE LEAID='{districtId}';", sqliteConnection);
                var sqliteDataReader = sqliteCommand.ExecuteReader();

                var district = new Entities.District();

                if (sqliteDataReader.Read())
                {
                    district.Name = sqliteDataReader["LEA_NAME"].ToString();
                    district.Id = sqliteDataReader["LEAID"].ToString();
                    district.City = sqliteDataReader["LCITY"].ToString();
                    district.StateAbr = sqliteDataReader["LSTATE"].ToString();
                    district.State = sqliteDataReader["STATENAME"].ToString();
                    district.PostalCode = sqliteDataReader["LZIP"].ToString();
                    district.AreaCode = sqliteDataReader["PHONE"].ToString().Substring(1, 3);
                }

                System.Console.WriteLine("District -" + district.Name);

                System.Console.WriteLine("Reading schools...");


                sqliteCommand = new SQLiteCommand($"SELECT * FROM school WHERE LEAID='{districtId}';", sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();
                while (sqliteDataReader.Read())
                {
                    Entities.School school = new Entities.School
                    {
                        Id = sqliteDataReader["SCHID"].ToString(),
                        Name = sqliteDataReader["SCH_NAME"].ToString().Replace("(", "").Replace(")", "")
                            .Replace("'", "").Replace("&", ""),
                        Level = sqliteDataReader["LEVEL"].ToString(),
                        City = sqliteDataReader["LCITY"].ToString(),
                        StateAbr = sqliteDataReader["LSTATE"].ToString(),
                        State = sqliteDataReader["STATENAME"].ToString(),
                        PostalCode = sqliteDataReader["LZIP"].ToString(),
                        AreaCode = sqliteDataReader["PHONE"].ToString().Substring(1, 3),
                        LeaId = sqliteDataReader["LEAID"].ToString().Substring(1, 3)
                    };

                    district.Schools.Add(school);
                }

                System.Console.WriteLine("Reading grades and students...");

                foreach (var school in district.Schools)
                {
                    var ethnicities = new List<Entities.Ethnicity>();

                    // The query is only accepting Grades 1-12
                    sqliteCommand = new SQLiteCommand(
                        $"select Grade,RACE_ETHNICITY,SEX, sum(STUDENT_COUNT) as Students from school_student where Grade like '%Grade %' and SCHID = '{school.Id}' group by GRADE, RACE_ETHNICITY, SEX having Students > 0;", sqliteConnection);
                    sqliteDataReader = sqliteCommand.ExecuteReader();

                    while (sqliteDataReader.Read())
                    {
                        var currentEthnicity = sqliteDataReader["RACE_ETHNICITY"].ToString();

                        if (invalidEthnicities.Contains(currentEthnicity))
                            continue;

                        ethnicities.Add(new Entities.Ethnicity
                        {
                            Name = ParseRaceEthnicity(currentEthnicity),
                            Grade = sqliteDataReader["GRADE"].ToString(),
                            Sex = sqliteDataReader["SEX"].ToString(),
                            StudentCount = (long)sqliteDataReader["Students"]
                        });
                    }

                    school.GradeLevels = ethnicities
                        .GroupBy(e => e.Grade)
                        .Select(g => new Entities.GradeLevel
                        {
                            Grade = g.Key,
                            SchoolId = school.Id,
                            TotalStudents = g.Sum(x => x.StudentCount),
                            Ethnicities = g.GroupBy(e => new { e.Name, e.Grade, e.Sex })
                            .Select(e => new Entities.Ethnicity {
                                    Name = e.Key.Name,
                                    Grade = e.Key.Grade,
                                    Sex = e.Key.Sex,
                                    StudentCount = e.Sum( es => es.StudentCount)
                            }).ToList()
                        }).Where(x => !invalidGradeLevels.Contains(x.Grade)).ToList(); // TODO: Remove filter by adding the required data on course.csv
                }

                // A School must have a valid grade level
                district.Schools = district.Schools.Where(s => s.GradeLevels.Any()).ToList();

                if (!district.Schools.Any())
                    throw new Exception("There is missing data or gradelevels on all district schools.");

                System.Console.WriteLine("Writing File...");

                XmlTemplateHelper.BuildConfigFile(district);

                System.Console.WriteLine("The Config file has been generated successfully.");

                System.Console.WriteLine("Starting reading and writing csv files..");

                CsvWriteHelper.ModifyCsvFiles(district);

                System.Console.WriteLine("The csv files have been modified successfully.");
            }
            catch (Exception e)
            {
                throw new Exception("Error when trying to create configuration or modifying csv files", e);
            }
        }

        private static string ParseRaceEthnicity(string race)
        {
            // Maps to the races available in the csv datafiles
            switch (race)
            {
                case "American Indian or Alaska Native":
                    return "AmericanIndianAlaskanNative";
                case "Asian":
                    return "Asian";
                case "Black or African American":
                    return "Black";
                case "Hispanic/Latino":
                    return "Hispanic";
                case "Native Hawaiian or Other Pacific Islander":
                    return "NativeHawaiianPacificIslander";
                case "White":
                case "Two or more races":
                        return "White";
                default:
                    throw new NotImplementedException($"The ethnicity {race} doesn't have a match for the datafiles.");
            }
        }

        private static void PrintCopyrightMessageToConsole()
        {
            const string copyrightText = 
                "\r\n" + 
                "Sample Data Generator is Copyright \u00a9 2018 Ed-Fi Alliance, LLC\r\n" + 
                "License info available at https://techdocs.ed-fi.org/display/SDG/Licensing \r\n";

            //Set encoding to UTF8 so copyright symbol in the above message prints correctly
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Console.WriteLine(copyrightText);
        }

        private static void InitializeApp()
        {
            BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof (Program));
        }

        private static SampleDataGeneratorConsoleConfig ParseCommandLine(string[] args)
        {
            var parser = new CommandLineParser();
            var parseResult = parser.Parse(args);

            if (parseResult.HasErrors)
            {
                throw new Exception(parseResult.ErrorText);
            }

            ValidateCommandLineConfig(parser.Object);

            return parser.Object;
        }

        private static void ValidateCommandLineConfig(SampleDataGeneratorConsoleConfig config)
        {
            var validator = new CommandLineValidator();
            var validationResult = validator.Validate(config);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    _log.Error(error);
                }

                throw new Exception("Invalid command line arguments");
            }
        }

        private static SampleDataGeneratorConfig LoadXmlConfig(SampleDataGeneratorConsoleConfig commandLineConfig)
        {
            try
            {
                var config = XmlConfigHelpers.ParseConfigFileToObject<SampleDataGeneratorConfig>(commandLineConfig.ConfigXmlPath);
                config.DataFilePath = commandLineConfig.DataFilePath;
                config.OutputPath = commandLineConfig.OutputPath;
                config.SeedFilePath = commandLineConfig.SeedFilePath;
                config.OutputMode = commandLineConfig.OutputMode;
                config.CreatePerformanceFile = commandLineConfig.CreatePerformanceFile;

                var validator = new SampleDataGeneratorConfigValidator();

                if (ShouldScanForDataFiles(config))
                {
                    var dataFileScanner = new DataFileScanner();
                    dataFileScanner.ScanForDataFiles(config);
                }

                var validationErrors = validator.Validate(config);
                if (validationErrors.Any())
                    throw new Exception("Invalid configuration.");

                return config;
            }
            catch (Exception e)
            {
                throw new Exception("Error when trying to create configuration", e);
            }
        }

        private static bool ShouldScanForDataFiles(SampleDataGeneratorConfig parsedConfig)
        {
            return !string.IsNullOrEmpty(parsedConfig.DataFilePath);
        }

        private static void Run(SampleDataGeneratorConfig config)
        {
            var sampleDataGenerator = new SampleDataGenerationService();
            sampleDataGenerator.Run(config);
        }
    }
}

