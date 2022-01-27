using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace EdFi.SampleDataGenerator.Core.Config
{
    public interface ISchoolProfile
    {
        int SchoolId { get; }
        string SchoolName { get; }
        IGradeProfile[] GradeProfiles { get; }
        IStaffProfile StaffProfile { get; }
        IDisciplineProfile DisciplineProfile { get; }
        ISchoolAttendanceProfile AttendanceProfile { get; }
        int CourseLoad { get; }

        int InitialStudentCount { get; }
    }

    public class SchoolProfileValidator : AbstractValidator<ISchoolProfile>
    {
        public SchoolProfileValidator(ISampleDataGeneratorConfig globalConfig)
        {
            RuleFor(x => x.SchoolName)
                .NotEmpty()
                .WithMessage("SchoolProfile SchoolName must be defined and non-empty");

            RuleFor(x => x.SchoolName)
                .Must(HaveFileSystemSafeNames)
                .WithMessage(
                    "SchoolProfile SchoolName '{SchoolName}' must be safe for use in filenames. Invalid characters: {InvalidCharacters}");

            RuleFor(x => x.GradeProfiles)
                .NotEmpty()
                .WithMessage(x => $"At least one GradeProfile must be defined for School '{x.SchoolName}'");

            RuleForEach(x => x.GradeProfiles)
                .SetValidator(x => new GradeProfileValidator(x.SchoolName));

            RuleFor(x => x.DisciplineProfile)
                .NotEmpty()
                .WithMessage(x => $"DisciplineProfile must be defined for school '{x.SchoolName}'")
                .SetValidator(x => new DisciplineProfileValidator());

            RuleFor(x => x.AttendanceProfile)
                .SetValidator(x => new SchoolAttendanceProfileValidator(x.SchoolName));

            RuleFor(x => x.InitialStudentCount)
                .GreaterThan(0)
                .WithMessage(x => $"SchoolProfile '{x.SchoolName}' must contain at least 1 student");

            RuleFor(x => x.StaffProfile)
                .NotEmpty()
                .WithMessage(x => $"Staff profile must be defined for school '{x.SchoolName}'")
                .SetValidator(x => new StaffProfileValidator(x.SchoolName, globalConfig));

            RuleFor(x => x.CourseLoad)
                .GreaterThan(0)
                .WithMessage(x => $"SchoolProfile '{x.SchoolName}' must define a course load");
        }

        private bool HaveFileSystemSafeNames(
            ISchoolProfile profile,
            string schoolName,
            ValidationContext<ISchoolProfile> context)
        {
            var invalidCharacters = schoolName.FileSystemUnsafeCharacters();

            if (invalidCharacters.Any())
            {
                context.MessageFormatter.AppendArgument("SchoolName", schoolName);
                context.MessageFormatter.AppendArgument("InvalidCharacters", new string(invalidCharacters));
                return false;
            }

            return true;
        }
    }
}
