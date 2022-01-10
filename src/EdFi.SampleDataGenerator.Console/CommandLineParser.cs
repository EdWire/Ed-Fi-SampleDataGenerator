using System;
using EdFi.SampleDataGenerator.Core.DataGeneration.Common;
using Fclp;

namespace EdFi.SampleDataGenerator.Console
{
    public class CommandLineParser : FluentCommandLineParser<SampleDataGeneratorConsoleConfig>
    {
        public CommandLineParser()
        {
            SetupHelp("?", "Help").Callback(text =>
            {
                System.Console.WriteLine(text);
                Environment.Exit(0);
            });

            Setup(a => a.ConfigXmlPath)
                .As('c', "configXmlPath")
                .WithDescription("Path to XML configuration file");

            Setup(a => a.DataFilePath)
                .As('d', "dataFilePath")
                .WithDescription("Path to directory containing input CSV data files")
                .SetDefault(".\\DataFiles\\");

            Setup(a => a.OutputPath)
                .As('o', "outputPath")
                .WithDescription("Path where output XML files will be placed")
                .SetDefault(".\\");

            Setup(a => a.SeedFilePath)
                .As('s', "seedFilePath")
                .WithDescription("Path to seed data file")
                .SetDefault(string.Empty);

            Setup(a => a.OutputMode)
                .As('m', "outputMode")
                .WithDescription("Mode of data generation/output.  One of {Standard, Seed}")
                .SetDefault(OutputMode.Standard);

            Setup(a => a.AllowOverwrite)
                .As('w', "allowOverwrite")
                .WithDescription("Allow for files in the OutputPath and SeedFilePath to be overwritten");

            Setup(a => a.CreatePerformanceFile)
                .As('p', "createPerformanceFile")
                .WithDescription("Useful for debugging, this enables the output of each student's performance index.")
                .SetDefault(false);

            Setup(a => a.ConfigurationType)
                .As('t', "ConfigurationType")
                .WithDescription("Type of configuration. One of {ConfigurationFile, Database")
                .SetDefault(ConfigurationType.ConfigurationFile);

            Setup(a => a.NCESDatabasePath)
                .As('u', "useNCESDatabase")
                .WithDescription("Activates logic to generate the xml config through the NCES Database file.")
                .SetDefault(string.Empty);

            Setup(a => a.NCESDistrictId)
                .As('i', "NCESDistrictId")
                .WithDescription("NCES district Id")
                .SetDefault(string.Empty);
        }
    }
}
