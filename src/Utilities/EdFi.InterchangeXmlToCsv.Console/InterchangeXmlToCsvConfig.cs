using System.Text;
using CommandLine;

namespace EdFi.InterchangeXmlToCsv.Console
{
    public class InterchangeXmlToCsvConfig
    {
        [Option('i', "inputPath", Required = true, HelpText = "Path to the input XML file or directory of XML files")]
        public string InputPath { get; set; }

        [Option('r', "recurse", Default = false, HelpText = "If specified and inputPath is a folder, all XML files contained in inputPath and its subfolders will be processed")]
        public bool Recurse { get; set; }

        [Option('o', "outputPath", Required = true, HelpText = "Path where output CSV file(s) will be written")]
        public string OutputPath { get; set; }

        [Option('n', "interchangeName", Required = true, HelpText = "Name of Ed-Fi Interchange for which the file(s) contain data, e.g. Students, Descriptors, EducationOrganization")]
        public string InterchangeName { get; set; }
    }
}
