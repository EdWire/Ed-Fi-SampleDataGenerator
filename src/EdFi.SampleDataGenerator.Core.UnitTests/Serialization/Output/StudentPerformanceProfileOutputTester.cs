using EdFi.SampleDataGenerator.Core.Serialization.Output;
using NUnit.Framework;
using Shouldly;

namespace EdFi.SampleDataGenerator.Core.UnitTests.Serialization.Output
{
    [TestFixture]
    public class StudentPerformanceProfileOutputTester
    {
        [Test]
        public void ShouldBuildValidOutputXml()
        {
            var spOutput = new StudentPerformanceProfileOutput();

            // note: had to remove a digit of precision as it is not valid on other operating systems
            spOutput.Add("000016", 0.2883090373683189);
            spOutput.Add("000017", 0.5695602104389406);

            spOutput.ToXml().ToString().ShouldBe(Expectation);
        }

        private static string Expectation => @"
<StudentPerformanceProfile>
  <Student>
    <StudentUniqueId>000016</StudentUniqueId>
    <PerformanceIndex>0.2883090373683189</PerformanceIndex>
  </Student>
  <Student>
    <StudentUniqueId>000017</StudentUniqueId>
    <PerformanceIndex>0.5695602104389406</PerformanceIndex>
  </Student>
</StudentPerformanceProfile>".Trim();
    }
}
