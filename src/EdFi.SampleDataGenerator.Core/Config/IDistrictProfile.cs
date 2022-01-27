using FluentValidation;

namespace EdFi.SampleDataGenerator.Core.Config
{
    public interface IDistrictProfile
    {
        string DistrictName { get; }
        double? HighPerformingStudentPercentile { get; }
        ISchoolProfile[] SchoolProfiles { get; }
        ILocationInfo LocationInfo { get; }
    }

    public class DistrictProfileValidator : AbstractValidator<IDistrictProfile>
    {
        public DistrictProfileValidator(ISampleDataGeneratorConfig globalConfig)
        {
            RuleFor(x => x.SchoolProfiles)
                .NotEmpty()
                .WithMessage(x => $"At least one School Profile must be defined for District {x.DistrictName}");

            RuleFor(a => a.HighPerformingStudentPercentile)
                .InclusiveBetween(0, 1)
                .WithMessage(x => $"HighPerformingStudentPercentile must be between 0 and 1 for District {x.DistrictName}");

            RuleForEach(x => x.SchoolProfiles)
                .SetValidator(new SchoolProfileValidator(globalConfig));

            RuleFor(x => x.LocationInfo)
                .SetValidator(d => new LocationInfoValidator(d.DistrictName));
        }
    }
}
