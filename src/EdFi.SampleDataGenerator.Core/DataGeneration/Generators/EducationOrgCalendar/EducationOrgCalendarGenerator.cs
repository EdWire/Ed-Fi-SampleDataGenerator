using EdFi.SampleDataGenerator.Core.ApplicationPerformanceLog;
using EdFi.SampleDataGenerator.Core.DataGeneration.Common;
using EdFi.SampleDataGenerator.Core.DataGeneration.InterchangeEntities;

namespace EdFi.SampleDataGenerator.Core.DataGeneration.Generators.EducationOrgCalendar
{
    public class EducationOrgCalendarGenerator : GlobalDataGenerator
    {
        public override InterchangeEntity InterchangeEntity => InterchangeEntity.EducationOrgCalendar;
        private PerformanceLogger _performanceLogger;

        public EducationOrgCalendarGenerator(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator, EmptyGeneratorFactory)
        {
            _performanceLogger = new PerformanceLogger();
        }

        public override void Generate(GlobalDataGeneratorContext context)
        {
            var tracker = _performanceLogger.Start("Generate EducationOrgCalendar");
            context.GlobalData.EducationOrgCalendarData = Configuration.EducationOrgCalendarData;
            tracker.End();
        }
    }
}
