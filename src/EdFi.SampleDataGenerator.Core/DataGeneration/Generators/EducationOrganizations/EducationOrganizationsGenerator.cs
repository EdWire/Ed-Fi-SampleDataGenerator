using EdFi.SampleDataGenerator.Core.ApplicationPerformanceLog;
using EdFi.SampleDataGenerator.Core.DataGeneration.Common;
using EdFi.SampleDataGenerator.Core.DataGeneration.InterchangeEntities;

namespace EdFi.SampleDataGenerator.Core.DataGeneration.Generators.EducationOrganizations
{
    public class EducationOrganizationsGenerator : GlobalDataGenerator
    {
        public override InterchangeEntity InterchangeEntity => InterchangeEntity.EducationOrganization;
        private PerformanceLogger _performanceLogger;

        public EducationOrganizationsGenerator(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator, EmptyGeneratorFactory)
        {
            _performanceLogger = new PerformanceLogger();
        }

        public override void Generate(GlobalDataGeneratorContext context)
        {
            var tracker = _performanceLogger.Start("Generate EducationOrganizations");
            context.GlobalData.EducationOrganizationData = Configuration.EducationOrganizationData;
            tracker.End();
        }
    }
}
