using CommandLine;
using EdFi.SampleDataGenerator.Core.DataGeneration.Common;

namespace EdFi.SampleDataGenerator.Console
{
    public class SampleDataGeneratorConsoleConfig
    {
        [Option('c', "configXmlPath", HelpText = "Path to XML configuration file")]
        public string ConfigXmlPath { get; set; }

        [Option(
            'd',
            "dataFilePath",
            Default = "./DataFiles/",
            HelpText = "Path to directory containing input CSV data files")]
        public string DataFilePath { get; set; }

        [Option('o', "outputPath", Default = "./", HelpText = "Path where output XML files will be placed")]
        public string OutputPath { get; set; }

        [Option('s', "seedFilePath", Default = "", HelpText = "Path to seed data file")]
        public string SeedFilePath { get; set; }

        [Option(
            'm',
            "outputMode",
            Default = OutputMode.Standard,
            HelpText = "Mode of data generation/output.  One of {Standard, Seed}")]
        public OutputMode OutputMode { get; set; }

        [Option(
            'w',
            "allowOverwrite",
            HelpText = "Allow for files in the OutputPath and SeedFilePath to be overwritten")]
        public bool AllowOverwrite { get; set; }

        [Option(
            'p',
            "createPerformanceFile",
            Default = false,
            HelpText = "Useful for debugging, this enables the output of each student's performance index.")]
        public bool CreatePerformanceFile { get; set; }

        [Option(
            'u',
            "useNCESDatabase",
            Default = "",
            HelpText = "Activates logic to generate the xml config through the NCES Database file.")]
        public string NCESDatabasePath { get; set; }

        [Option('i', "NCESDistrictId", Default = "", HelpText = "NCES district Id")]
        public string NCESDistrictId { get; set; }

        [Option(
            't',
            "ConfigurationType",
            Default = Console.ConfigurationType.ConfigurationFile,
            HelpText = "Type of configuration. One of {ConfigurationFile, Database")]
        public ConfigurationType ConfigurationType { get; set; }
    }

    public enum ConfigurationType
    {
        ConfigurationFile,
        Database,
    }
}
