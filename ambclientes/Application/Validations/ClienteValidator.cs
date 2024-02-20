using Amb.Clientes.Domain.Models;
using FluentValidation;

namespace Amb.Clientes.Application.Validations;

public class ClienteValidator: AbstractValidator<ClienteDto>
{
    public ClienteValidator()
    {
        
        RuleFor(x => x.Nombre).NotNull().NotEmpty().MaximumLength(50);
        
        RuleFor(x => x.Apellido).NotNull().NotEmpty().MaximumLength(50);
        
        RuleFor(x => x.Cuit)
            .NotNull()
            .MinimumLength(10)
            .MaximumLength(11);
        
        RuleFor(x => x.FechaNacimiento)
            .Configure(x => x.SetDisplayName("Fecha de nacimiento"));
        
        RuleFor(x => x.Mail)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.Telefono)
            .NotNull()
            .NotEmpty()
            .Must(x => x.ToString().StartsWith("11") && x.ToString().Length == 10)
            .WithMessage("Número de celular debe comenzar con 11");
    }
}