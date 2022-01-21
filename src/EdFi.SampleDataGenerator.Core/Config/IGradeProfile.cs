using System.Linq;
using EdFi.SampleDataGenerator.Core.Entities;
using EdFi.SampleDataGenerator.Core.Helpers;
using FluentValidation;

namespace EdFi.SampleDataGenerator.Core.Config
{
    public interface IGradeProfile
    {
        string GradeName { get; }
        IStudentPopulationProfile[] StudentPopulationProfiles { get; }
        IGraduationPlanTemplateReference[] GraduationPlanTemplateReferences { get; }
        IAssessmentParticipationConfiguration[] AssessmentParticipationConfigurations { get; }

        int InitialStudentCount { get; }
    }

    public static class GradeProfileExtensions
    {
        public static GradeLevelDescriptor GetGradeLevel(this IGradeProfile gradeProfile)
        {
            return DescriptorHelpers.ParseFromCodeValue<GradeLevelDescriptor, IGradeProfile>(gradeProfile, p => p.GradeName, p => $"'{p.GradeName}' is not a valid GradeLevelDescriptor value");
        }

        public static SchoolYearType GetGraduationYear(this IGradeProfile gradeProfile, ISchoolProfile school, SchoolYearType currentSchoolYear)
        {
            var maxGradeLevel = school.GradeProfiles.Select(gp => gp.GetGradeLevel()).OrderBy(gl => gl.GetNumberOfSchoolYearsUntilGraduation()).First();
            var minYearsUntilSchoolGraduation = maxGradeLevel.GetNumberOfSchoolYearsUntilGraduation();

            var yearsUntilGraduation = gradeProfile.GetGradeLevel().GetNumberOfSchoolYearsUntilGraduation() - minYearsUntilSchoolGraduation;
            return currentSchoolYear.AddYears(yearsUntilGraduation);
        }
    }

    public class GradeProfileValidator : AbstractValidator<IGradeProfile>
    {
        private readonly GradeLevelDescriptor[] _acceptableGradeLevels =
        {
            GradeLevelDescriptor.Kindergarten,
            GradeLevelDescriptor.FirstGrade,
            GradeLevelDescriptor.SecondGrade,
            GradeLevelDescriptor.ThirdGrade,
            GradeLevelDescriptor.FourthGrade,
            GradeLevelDescriptor.FifthGrade,
            GradeLevelDescriptor.SixthGrade,
            GradeLevelDescriptor.SeventhGrade,
            GradeLevelDescriptor.EighthGrade,
            GradeLevelDescriptor.NinthGrade,
            GradeLevelDescriptor.TenthGrade,
            GradeLevelDescriptor.EleventhGrade,
            GradeLevelDescriptor.TwelfthGrade
        };

        public GradeProfileValidator(string schoolName)
        {
            RuleFor(x => x.GradeName)
                .NotEmpty()
                .Must(BeConvertibleToGradeLevelType)
                .WithMessage(p => $"'{p.GradeName}' is not a valid GradeName for SchoolProfile '{schoolName}'");

            RuleFor(x => x.GradeName)
                .NotEmpty()
                .Must(BeAKThroughTwelveGradeLevel)
                .WithMessage(p =>
                    $"'{p.GradeName}' is not a valid GradeName for SchoolProfile '{schoolName}' - only K-12 grades are allowed");

            RuleFor(x => x.AssessmentParticipationConfigurations).NotNull()
                .WithMessage(p =>
                    $"At least one AssessmentParticipationRate must be defined for {schoolName}, {p.GradeName}");

            RuleForEach(x => x.AssessmentParticipationConfigurations).SetValidator(a => new AssessmentParticipationConfigurationValidator(schoolName, a.GradeName));
        }

        private bool BeConvertibleToGradeLevelType(string gradeName)
        {
            return DescriptorHelpers.IsParseableToDescriptorFromCodeValue<GradeLevelDescriptor>(gradeName);
        }

        private bool BeAKThroughTwelveGradeLevel(string gradeName)
        {
            if (!BeConvertibleToGradeLevelType(gradeName))
            {
                return false;
            }

            return _acceptableGradeLevels.Contains(gradeName.ToDescriptorFromCodeValue<GradeLevelDescriptor>());
        }
    }
}
