using System;
using System.IO;
using System.Reflection;
using CommandLine;
using log4net;
using log4net.Config;

namespace EdFi.InterchangeXmlToCsv.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            PrintCopyrightMessageToConsole();

            var assembly = typeof(Program).GetTypeInfo().Assembly;

            var configPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "log4net.config");

            XmlConfigurator.Configure(LogManager.GetRepository(assembly), new FileInfo(configPath));

            InterchangeXmlToCsvConfig config = new InterchangeXmlToCsvConfig();

            var parser = new Parser(
                    c =>
                    {
                        c.CaseInsensitiveEnumValues = true;
                        c.CaseSensitive = false;
                        c.HelpWriter = System.Console.Out;
                        c.IgnoreUnknownArguments = true;
                    })
                .ParseArguments<InterchangeXmlToCsvConfig>(args)
                .WithParsed(a => config = a)
                .WithNotParsed(
                    errs =>
                    {
                        System.Console.WriteLine("Invalid options were entered.");

                        System.Console.WriteLine(string.Join(Environment.NewLine, errs));

                        Environment.ExitCode = -1;
                        Environment.Exit(Environment.ExitCode);
                    });

            var converter = new InterchangeXmlToCsvConverter();
            converter.Convert(config);

#if DEBUG
            System.Console.Write("Press any key to continue...");
            System.Console.ReadKey();
#endif

            return 0;
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
    }
}
