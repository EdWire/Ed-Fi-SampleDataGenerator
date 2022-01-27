using CommandLine;

namespace EdFi.MasterScheduleGenerator.Console
{
    public class CommandLineOptions
    {
        [Option('o', "outputPath", Default = "./", HelpText = "Path to the Session.csv file")]
        public string OutputPath { get; set; }

        [Option('y', "schoolYear", Required = true, HelpText = "School year to generate (in form 2016-2017)")]
        public string SchoolYear { get; set; }

        [Option('s', "schoolFile", Default = "./School.csv", HelpText = "Path to the School.csv file")]
        public string SchoolFilePath { get; set; }

        [Option('c', "courseFile", Default = "./Course.csv", HelpText = "Path to the Course.csv file")]
        public string CourseFilePath { get; set; }

        [Option('p', "classPeriodFile", Default = "./ClassPeriod.csv", HelpText = "Path to the ClassPeriod.csv file")]
        public string ClassPeriodFilePath { get; set; }

        [Option('l', "locationFile", Default = "./Location.csv", HelpText = "Path to the Location file")]
        public string LocationFilePath { get; set; }

        [Option('e', "sessionFile", Default = "./Session.csv", HelpText = "Path to the Session.csv file")]
        public string SessionFilePath { get; set; }
    }
}
