using Amb.Clientes.Application.Validations;
using Amb.Clientes.Domain.Entities;
using Amb.Clientes.Domain.Filters;
using Amb.Clientes.Domain.Models;
using Amb.Clientes.Persistence.Repository;
using FluentValidation;

namespace Amb.Clientes.Application.Admin;

public class ClienteAdmin
{
    public IClienteRepository Repository;
    private readonly IValidator<ClienteDto> _validator = new ClienteValidator();

    public PaginatedModel<Cliente> Get(BaseFilter filter)
    {
        return Repository.Get(filter);
    }

    public List<Cliente> GetAll()
    {
        var data = Repository.GetAll(100);
        return data.ToList();
    }

    public async Task<Cliente> GetById(int id)
    {
        return await Repository.GetById(id);
    }

    public async Task<Cliente> Create(ClienteDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ArgumentException(validation.Errors[0].ErrorMessage);

        dto.Nombre = Capitalize(dto.Nombre);
        dto.Apellido = Capitalize(dto.Apellido);
        dto.Mail = dto.Mail.ToLower();
        dto.Cuit = ValidateCuit(dto.Cuit);
        
        return await Repository.Create(dto);
    }
    
    public async Task<Cliente> Update(int id, ClienteDto dto)
    {
        var validation = await _validator.ValidateAsync(dto);
        if (!validation.IsValid)
            throw new ArgumentException(validation.Errors[0].ErrorMessage);

        dto.Nombre = Capitalize(dto.Nombre);
        dto.Apellido = Capitalize(dto.Apellido);
        dto.Mail = dto.Mail.ToLower();
        dto.Cuit = ValidateCuit(dto.Cuit);
        
        return await Repository.Update(id, dto);
    }
    
    public string Capitalize(string text)
    {
        var fullText = "";
        var words = text
            .Split(" ").ToList();
        
        words.ForEach(w =>
            fullText += w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower() + " ");

        return fullText.Trim();
    }

    private string ValidateCuit(string cuit)
    {
        var cuitLength = cuit.Length;
        if (cuitLength < 10 || cuitLength > 11)
            throw new ArgumentException("CUIT Inválido");
        
        var docLength = cuitLength - 3;
        string prefix = cuit.Substring(0, 2);
        string dni = cuit.Substring(2, docLength);
        string sufix = cuit.Substring(cuitLength - 1);
        
        return $"{prefix}-{dni}-{sufix}";
    }
    
}