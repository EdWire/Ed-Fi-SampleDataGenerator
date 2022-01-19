using EdFi.SampleDataGenerator.Core.ApplicationPerformanceLog;
using EdFi.SampleDataGenerator.Core.DataGeneration.Common;
using EdFi.SampleDataGenerator.Core.DataGeneration.InterchangeEntities;

namespace EdFi.SampleDataGenerator.Core.DataGeneration.Generators.Descriptors
{
    public class DescriptorsGenerator : GlobalDataGenerator
    {
        public override InterchangeEntity InterchangeEntity => InterchangeEntity.Descriptors;

        private PerformanceLogger _performanceLogger;

        public DescriptorsGenerator() : this(new RandomNumberGenerator())
        {
           
        }

        public DescriptorsGenerator(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator, EmptyGeneratorFactory)
        {
            _performanceLogger = new PerformanceLogger();
        }

        public override void Generate(GlobalDataGeneratorContext context)
        {
            var tracker = _performanceLogger.Start("Generate Descriptors");
            context.GlobalData.Descriptors.AddRange(Configuration.DescriptorFiles);
            tracker.End();
        }
    }
}
