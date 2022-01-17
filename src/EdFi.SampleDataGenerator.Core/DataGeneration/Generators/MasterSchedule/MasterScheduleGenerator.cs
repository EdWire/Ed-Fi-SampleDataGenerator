using EdFi.SampleDataGenerator.Core.ApplicationPerformanceLog;
using EdFi.SampleDataGenerator.Core.DataGeneration.Common;
using EdFi.SampleDataGenerator.Core.DataGeneration.InterchangeEntities;

namespace EdFi.SampleDataGenerator.Core.DataGeneration.Generators.MasterSchedule
{
    public class MasterScheduleGenerator : GlobalDataGenerator
    {
        public override InterchangeEntity InterchangeEntity => InterchangeEntity.MasterSchedule;
        private PerformanceLogger _performanceLogger;

        public MasterScheduleGenerator(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator, EmptyGeneratorFactory)
        {
            _performanceLogger = new PerformanceLogger();
        }

        public override void Generate(GlobalDataGeneratorContext context)
        {
            var tracker = _performanceLogger.Start("Generate MasterSchedule");
            context.GlobalData.MasterScheduleData = Configuration.MasterScheduleData;
            tracker.End();
        }
    }
}
