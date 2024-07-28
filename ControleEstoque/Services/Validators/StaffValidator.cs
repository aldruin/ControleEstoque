using ControleEstoque.Models.DTOs;
using ControleEstoque.Models.Enums;
using FluentValidation;

namespace ControleEstoque.Services.Validators;

public sealed class StaffValidator : AbstractValidator<StaffDto>
{
    public StaffValidator()
    {
        RuleFor(staff => staff.Name)
            .NotEmpty().WithMessage("O nome do staff é obrigatório.")
            .NotNull().WithMessage("O nome do staff não pode ser nulo.");

        RuleFor(staff => staff.Role)
            .NotEmpty().WithMessage("O papel do staff é obrigatório.")
            .NotNull().WithMessage("O papel do staff não pode ser nulo.")
            .Must(BeAValidRole).WithMessage("O papel do staff deve ser válido.");
    }

    private bool BeAValidRole(StaffRole? role)
    {
        if (!role.HasValue)
            return false;

        return Enum.IsDefined(typeof(StaffRole), role.Value);
    }
}