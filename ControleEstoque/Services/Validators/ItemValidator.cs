using ControleEstoque.Models.DTOs;
using ControleEstoque.Models.Enums;
using FluentValidation;

namespace ControleEstoque.Services.Validators
{
    public sealed class ItemValidator : AbstractValidator<ItemDto>
    {
        public ItemValidator()
        {
            RuleFor(item => item.Name)
                .NotEmpty().WithMessage("O nome do item é obrigatório.")
                .NotNull().WithMessage("O nome do item não pode ser nulo.");

            RuleFor(item => item.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade do item deve ser um número positivo ou zero.");

            RuleFor(item => item.Description)
                .NotEmpty().WithMessage("A descrição do item é obrigatória.")
                .NotNull().WithMessage("A descrição do item não pode ser nula.");

            RuleFor(item => item.Class)
                .NotEmpty().WithMessage("A classe do item é obrigatória.")
                .NotNull().WithMessage("A classe do item não pode ser nula.")
                .Must(BeAValidClass).WithMessage("A classe do item deve ser válida.");

            RuleFor(item => item.StaffId)
                .NotEmpty().WithMessage("O ID do staff é obrigatório.")
                .NotNull().WithMessage("O ID do staff não pode ser nulo.");

            RuleFor(item => item.Start)
                .NotEmpty().WithMessage("A data de início é obrigatória.")
                .NotNull().WithMessage("A data de início não pode ser nula.")
                .LessThanOrEqualTo(item => item.End).WithMessage("A data de início deve ser menor ou igual à data de fim.");

            RuleFor(item => item.End)
                .NotEmpty().WithMessage("A data de fim é obrigatória.")
                .NotNull().WithMessage("A data de fim não pode ser nula.")
                .GreaterThanOrEqualTo(item => item.Start).WithMessage("A data de fim deve ser maior ou igual à data de início.");
        }

        private bool BeAValidClass(string? className)
        {
            if (string.IsNullOrEmpty(className))
                return false;

            return Enum.TryParse(typeof(ItemClass), className, out var result) && Enum.IsDefined(typeof(ItemClass), result);
        }
    }
}