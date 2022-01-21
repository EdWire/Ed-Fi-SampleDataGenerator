using CommandLine;

namespace EdFi.EducationOrganizationGenerator.Console
{
    public class CommandLineOptions
    {

        [Option('p', "configSnippets", Default = "./ConfigSnippets.xml", HelpText = "Path to XML file containing SDG configuration file snippets")]
        public string ConfigurationSnippetsFilePath { get; set; }

        [Option('s', "schoolNames", Default = "./SchoolNames.csv", HelpText = "Path to School names file")]
        public string SchoolNamesFilePath { get; set; }

        [Option('c', "courseTemplate", Default = "./Courses.csv", HelpText = "Path to Course template file")]
        public string CourseTemplateFilePath { get; set; }

        [Option('d', "districtConfig", Required = true, HelpText = "Path to district config file (e.g. MyDistrict.xml)")]
        public string DistrictConfigFilePath { get; set; }

        [Option('t', "streetNames", Default = "./StreetNames.csv", HelpText =  "Path to the Street names file")]
        public string StreetNamesFilePath { get; set; }

        [Option('o', "outputPath", Default = "./", HelpText = "Path where output files should be written")]
        public string OutputPath { get; set; }
    }
}
