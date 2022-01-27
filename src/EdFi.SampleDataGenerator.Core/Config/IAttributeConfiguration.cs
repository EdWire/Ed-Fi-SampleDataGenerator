using System.Linq;
using EdFi.SampleDataGenerator.Core.Helpers;
using FluentValidation;

namespace EdFi.SampleDataGenerator.Core.Config
{
    public interface IAttributeConfiguration
    {
        string Name { get; }
        IAttributeGeneratorConfigurationOption[] AttributeGeneratorConfigurationOptions { get; }
    }

    public interface IAttributeGeneratorConfigurationOption
    {
        string Value { get; }
        double Frequency { get; }
    }

    public class AttributeConfigurationValidator : AbstractValidator<IAttributeConfiguration>
    {
        public AttributeConfigurationValidator(string containerDescription, bool requireFullOptionDistribution)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage($"AttributeConfiguration Name property cannot be empty ({containerDescription})");

            RuleForEach(x => x.AttributeGeneratorConfigurationOptions)
                .NotNull()
                .SetValidator(config => new AttributeGeneratorConfigurationOptionValidator(containerDescription, config.Name));

            if (requireFullOptionDistribution)
            {
                RuleFor(x => x.AttributeGeneratorConfigurationOptions)
                    .Must(x => x.Sum(o => o.Frequency).IsEqualWithinTolerance(1.0))
                    .WithMessage(x =>
                        $"Options for '{x.Name}' must have Frequency values totaling 1.0 ({containerDescription})");
            }
        }
    }

    public class AttributeGeneratorConfigurationOptionValidator : AbstractValidator<IAttributeGeneratorConfigurationOption>
    {
        public AttributeGeneratorConfigurationOptionValidator(string containerDescription, string attributeName)
        {
            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage($"Value cannot be empty for Attribute '{attributeName}' ({containerDescription})");

            RuleFor(x => x.Frequency)
                .Must(f => f > 0.0 && f <= 1.0)
                .WithMessage(x =>
                    $"Frequency for Value '{x.Value}' must be greater than 0.0 and less than or equal to 1.0 for Attribute '{attributeName}' ({containerDescription})");
        }
    }
}
