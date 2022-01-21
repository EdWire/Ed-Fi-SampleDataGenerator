using System.IO;
using System.Linq;
using EdFi.SampleDataGenerator.Core.DataGeneration.Common;
using FluentValidation;

namespace EdFi.SampleDataGenerator.Console
{
    public class CommandLineValidator : AbstractValidator<SampleDataGeneratorConsoleConfig>
    {
        public CommandLineValidator()
        {
            RuleFor(x => x.DataFilePath)
                .Must((config, path) => Directory.Exists(path))
                .WithMessage(config => $"DataFilePath '{config.DataFilePath}' does not exist");

            RuleFor(x => x.ConfigXmlPath)
                .Must((config, path) => File.Exists(path))
                .When(x => x.ConfigurationType == ConfigurationType.ConfigurationFile)
                .WithMessage(config => $"No config file found at '{config.ConfigXmlPath}'");

            RuleFor(x => x.OutputMode)
                .Must((config, mode) => !string.IsNullOrWhiteSpace(config.SeedFilePath))
                .When(x => x.OutputMode == OutputMode.Seed)
                .WithMessage("When running in Seed mode, a SeedFilePath must be provided");

            RuleFor(x => x.OutputMode)
                .Must((config, mode) => File.Exists(config.SeedFilePath))
                .When(x => x.OutputMode == OutputMode.Standard && !string.IsNullOrWhiteSpace(x.SeedFilePath))
                .WithMessage(config => $"No seed file found at '{config.SeedFilePath}'");

            RuleFor(x => x.AllowOverwrite)
                .Must((config, allowOverwrite) => !Directory.GetFiles(config.OutputPath).Any())
                .When(x => !x.AllowOverwrite && Directory.Exists(x.OutputPath))
                .WithMessage(config =>
                    $"One or more files exist in {config.OutputPath}, but -AllowOverwrite argument not specified");

            RuleFor(x => x.AllowOverwrite)
                .Must((config, mode) => !File.Exists(config.SeedFilePath))
                .When(x => !x.AllowOverwrite && x.OutputMode == OutputMode.Seed)
                .WithMessage(config =>
                    $"Seed file '{config.SeedFilePath}' exists, but -AllowOverwrite argument not specified");

            RuleFor(x => x.NCESDatabasePath)
                .Must((config, path) => File.Exists(path))
                .When(x => x.ConfigurationType == ConfigurationType.Database)
                .WithMessage(config => $"No database file found at '{config.NCESDatabasePath}'");

            RuleFor(x => x.NCESDistrictId)
                .Must((config, type) => !string.IsNullOrWhiteSpace(config.NCESDistrictId))
                .When(x => x.ConfigurationType == ConfigurationType.Database)
                .WithMessage("District id can not empty");
        }
    }
}
