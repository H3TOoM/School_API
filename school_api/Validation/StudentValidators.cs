using school_api.DTOs;

namespace school_api.Validation
{
    public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
    {
        protected override Task ValidateInternal(StudentCreateDto dto, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                errors.Add("Student name is required.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                errors.Add("Email is required.");
            else if (!dto.Email.Contains('@'))
                errors.Add("Email must be a valid email address.");

            if (dto.DateOfBirth == default)
                errors.Add("Date of birth is required.");

            if (dto.UserId <= 0)
                errors.Add("Valid User ID is required.");

            return Task.CompletedTask;
        }
    }

    public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
    {
        protected override Task ValidateInternal(StudentUpdateDto dto, List<string> errors)
        {
            if (!string.IsNullOrWhiteSpace(dto.Email) && !dto.Email.Contains('@'))
                errors.Add("Email must be a valid email address.");

            if (dto.DateOfBirth.HasValue && dto.DateOfBirth.Value == default)
                errors.Add("Date of birth must be a valid date.");

            return Task.CompletedTask;
        }
    }
}
