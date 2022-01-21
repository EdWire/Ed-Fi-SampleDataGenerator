using System.Text.RegularExpressions;
using FluentValidation;

namespace EdFi.SampleDataGenerator.Core.Config
{
    public interface IAssessmentParticipationConfiguration
    {
        string AssessmentTitle { get; set; }
        bool RegexMatch { get; set; }
        Regex AssessmentTitleMatchExpression { get; }
        IAssessmentParticipationRate[] ParticipationRates { get; }
    }

    public interface IAssessmentParticipationRate
    {
        double LowerPerformancePercentile { get; set; }
        double UpperPerformancePercentile { get; set; }
        double Probability { get; set; }
    }

    public class AssessmentParticipationConfigurationValidator
        : AbstractValidator<IAssessmentParticipationConfiguration>
    {
        public AssessmentParticipationConfigurationValidator(string schoolName, string gradeName)
        {
            RuleFor(a => a.AssessmentTitle)
                .NotNull()
                .NotEmpty()
                .WithMessage(
                    x =>
                        $"A assessment for {schoolName}, {gradeName} has an empty title; Assessment Title must be defined and non-empty.");
            RuleFor(a => a.ParticipationRates)
                .NotNull()
                .WithMessage(
                    x =>
                        $"The configuration must include at least one assessment for School {schoolName}, {gradeName}.");
            RuleForEach(a => a.ParticipationRates)
                .SetValidator(x => new AssessmentParticipationRateValidator(schoolName, gradeName, x.AssessmentTitle));
        }
    }

    public class AssessmentParticipationRateValidator : AbstractValidator<IAssessmentParticipationRate>
    {
        public AssessmentParticipationRateValidator(string schoolName, string gradeName, string assessmentTitle)
        {
            RuleFor(a => a.LowerPerformancePercentile)
                .InclusiveBetween(0, 1)
                .WithMessage(x =>
                    $"The assessment {assessmentTitle} for {schoolName}, {gradeName} has an invalid value; LowerPerformancePercentile must be between 0 and 1.");
            RuleFor(a => a.UpperPerformancePercentile)
                .InclusiveBetween(0, 1)
                .WithMessage(x =>
                    $"The assessment {assessmentTitle} for {schoolName}, {gradeName} has an invalid value; UpperPerformancePercentile must be between 0 and 1.");
            RuleFor(a => a.Probability)
                .InclusiveBetween(0, 1)
                .WithMessage(x =>
                    $"The assessment {assessmentTitle} for {schoolName}, {gradeName} has an invalid value; Probability must be between 0 and 1.");
            RuleFor(a => a.LowerPerformancePercentile)
                .LessThan(b => b.UpperPerformancePercentile)
                .WithMessage(x =>
                    $"The assessment {assessmentTitle} for {schoolName}, {gradeName} has an invalid value; LowerPerformancePercentile must be less than UpperPerformancePercentile.");
        }
    }
}
