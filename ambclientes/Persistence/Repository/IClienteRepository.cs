using Amb.Clientes.Domain.Entities;
using Amb.Clientes.Domain.Filters;
using Amb.Clientes.Domain.Models;

namespace Amb.Clientes.Persistence.Repository;

public interface IClienteRepository
{
    PaginatedModel<Cliente> Get(BaseFilter filter);
    IQueryable<Cliente> GetAll(int limit);
    Task<Cliente> GetById(int id);
    
    Task<Cliente> Create(ClienteDto data);
    Task<Cliente> Update(int id, ClienteDto data);
}