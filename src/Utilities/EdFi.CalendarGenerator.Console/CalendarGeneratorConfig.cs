using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace EdFi.CalendarGenerator.Console
{
    public enum TermType
    {
        Semester
    }

    public class CalendarGeneratorConfig
    {
        [Option('t', "termType", Default = TermType.Semester, HelpText = "Type of term used by school (e.g. semester)")]
        public TermType TermType { get; set; }

        [Option(
            'g',
            "gradingPeriodLength",
            Required = true,
            HelpText = "Length (in weeks) of grading period.  Must be either 6 or 9")]
        public int GradingPeriodLengthInWeeks { get; set; }

        [Option('s', "schoolStartDate", Required = true, HelpText = "Date on which school year begins")]
        public DateTime SchoolYearStartDate { get; set; }

        [Option('f', "schoolFile", HelpText = "Path to School.csv file that contains target School Ids")]
        public string SchoolFilePath { get; set; }

        [Option('i', "schoolId", HelpText = "Id(s) of schools for which you want calendar data to be generated")]
        public List<string> SchoolIds { get; set; }

        [Option('w', "workDays", Default = 0, HelpText = "Number of teacher-only work days per grading period")]
        public int TeacherWorkdaysPerGradingPeriod { get; set; }

        [Option('b', "badWeatherDays", Default = 0, HelpText = "Number of bad weather days per grading period")]
        public int BadWeatherDaysPerGradingPeriod { get; set; }

        [Option('o', "outputPath", Default = "", HelpText = "Path where output files will be stored")]
        public string OutputPath { get; set; }
    }
}
