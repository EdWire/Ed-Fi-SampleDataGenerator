using FluentValidation;

namespace EdFi.SampleDataGenerator.Core.Config
{
    public interface IImmigrantPopulationProfile
    {
        ICountryOfOrigin[] CountriesOfOrigin { get; }
    }

    public interface ICountryOfOrigin
    {
        string Name { get; }
        string Race { get; }
        double Frequency { get; }
    }

    public class ImmigrantPopulationProfileValidator : AbstractValidator<IImmigrantPopulationProfile>
    {
        public ImmigrantPopulationProfileValidator(
            ISampleDataGeneratorConfig sampleDataGeneratorConfig,
            IStudentProfile studentProfile)
        {
            RuleFor(x => x.CountriesOfOrigin)
                .NotEmpty()
                .WithMessage(
                    $"At least one CountryOfOrigin must be defined in ImmigrationProfile for StudentProfile '{studentProfile.Name}'");

            RuleForEach(x => x.CountriesOfOrigin)
                .SetValidator(x => new CountryOfOriginValidator(sampleDataGeneratorConfig, studentProfile));
        }
    }

    public class CountryOfOriginValidator : AbstractValidator<ICountryOfOrigin>
    {
        public CountryOfOriginValidator(
            ISampleDataGeneratorConfig sampleDataGeneratorConfig,
            IStudentProfile studentProfile)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage($"Name cannot be empty for CountryOfOrigin in Student Profile '{studentProfile.Name}'");

            RuleFor(x => x.Race)
                .NotEmpty()
                .WithMessage(
                    x =>
                        $"Race cannot be empty for CountryOfOrigin '{x.Name}' in Student Profile '{studentProfile.Name}'");

            RuleFor(x => x.Race)
                .Must(sampleDataGeneratorConfig.IsValidRaceOption)
                .WithMessage(
                    x =>
                        $"'{x.Race}' is not a valid RaceType for CountryOfOrigin '{x.Name}' in Student Profile '{studentProfile.Name}'");

            RuleFor(x => x.Frequency)
                .Must(f => f > 0.0 && f <= 1.0)
                .WithMessage(
                    x =>
                        $"Frequency for CountryOfOrigin '{x.Name}' / Race '{x.Race}' in Student Profile '{studentProfile.Name}' must be greater than 0.0 and less than or equal to 1.0");
        }
    }
}
