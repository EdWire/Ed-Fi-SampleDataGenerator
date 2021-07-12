using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Globalization;
using EdFi.SampleDataGenerator.Console.Config;
using EdFi.SampleDataGenerator.Core.Config.Xml;
using EdFi.SampleDataGenerator.Core.DataGeneration.Coordination;
using log4net;
using log4net.Config;
using Microsoft.Data.Sqlite;

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

        // Method to convert common prefixes into full wording
        private static string Schoolifier(string schoolName)
        {
            if (schoolName.Substring(schoolName.Length - 3, 3) == " EL") return schoolName.Substring(1, schoolName.Length - 3) + "ELEMENTARY SCHOOL";
            if (schoolName.Substring(schoolName.Length - 4, 4) == " M S") return schoolName.Substring(1, schoolName.Length - 4) + "MIDDLE SCHOOL";
            if (schoolName.Substring(schoolName.Length - 4, 4) == " H S") return schoolName.Substring(1, schoolName.Length - 4) + "HIGH SCHOOL";
            return schoolName;
        }

        private static string TitleCase(string name)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(name);
        }

        private static SampleDataGeneratorConfig LoadXmlConfig(SampleDataGeneratorConsoleConfig commandLineConfig)
        {
            try
            {
                System.Console.WriteLine("Please enter a NCES District ID and hit ENTER: ");  //4808940 = Austin ISD
                string districtID = System.Console.ReadLine();

                System.Console.WriteLine(); System.Console.WriteLine(); System.Console.WriteLine();

                commandLineConfig.ConfigXmlPath = @"..\..\Samples\SampleDataGenerator\NCESBlankConfig.xml";
                SqliteConnection sqliteConnection = new SqliteConnection(@"Filename=..\..\Samples\SampleDataGenerator\DataFiles\nces-2019.db");
                sqliteConnection.Open();
                SqliteCommand sqliteCommand = new SqliteCommand(String.Format("SELECT * FROM lea WHERE LEAID='{0}';", districtID), sqliteConnection);
                SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();

                Entities.District district;
                List<Entities.School> schools;
                

                district = FillDistrict(sqliteDataReader);

                sqliteCommand = new SqliteCommand(String.Format("SELECT RACE_ETHNICITY, SUM(STUDENT_COUNT) from school_student where LEAID ='{0}' GROUP BY RACE_ETHNICITY;", district.ID), sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();
                var districtStatistics = FillDistrictStatistics(district, sqliteDataReader);

                sqliteCommand = new SqliteCommand(String.Format("SELECT SEX, SUM(STUDENT_COUNT) from school_student where LEAID ='{0}' GROUP BY SEX;", district.ID), sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();
                districtStatistics = FillDistrictStatistics(district, sqliteDataReader);


                sqliteCommand = new SqliteCommand(String.Format("SELECT * FROM school WHERE LEAID='{0}';", district.ID), sqliteConnection);
                sqliteDataReader = sqliteCommand.ExecuteReader();
                schools = FillSchools(district, sqliteDataReader);


            }
            catch (Exception e)
            {
                throw new Exception("Error when trying to create configration", e);
            }

            System.Console.ReadLine();


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
                throw new Exception("Error when trying to read config file", e);
            }
        }

        private static Entities.District FillDistrict(SqliteDataReader sqliteDataReader)
        {
            Entities.District district = new Entities.District();

            if (sqliteDataReader.Read())
            {
                district.Name = TitleCase(sqliteDataReader["LEA_NAME"].ToString());
                district.ID = sqliteDataReader["LEAID"].ToString();
                district.City = sqliteDataReader["LCITY"].ToString();
                district.State = sqliteDataReader["LSTATE"].ToString();
                district.PostalCode = sqliteDataReader["LZIP"].ToString();
                district.AreaCode = sqliteDataReader["PHONE"].ToString().Substring(1, 3);
            }

            return district;
        }

        private static Entities.District FillDistrictStatistics(Entities.District district, SqliteDataReader sqliteDataReader)
        {
            if (district != null && sqliteDataReader != null)
            {
                
            }

            return district;

        }

        private static List<Entities.School> FillSchools(Entities.District district, SqliteDataReader sqliteDataReader)
        {
            //before your loop
            var csv = new StringBuilder();

            string[] row = { };

            while (sqliteDataReader.Read())
            {
                Entities.School school = new Entities.School();
                school.Name = sqliteDataReader["SCH_NAME"].ToString();
                //school.Name = Schoolifier(sqliteDataReader["SCH_NAME"].ToString());
                school.City = sqliteDataReader["LCITY"].ToString();
                var newLine = string.Format("{0},{1}", school.Name, school.City);
                csv.AppendLine(newLine);
                System.Console.WriteLine("  School -- " + newLine.ToString());
            }



            return new List<Entities.School>();
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

