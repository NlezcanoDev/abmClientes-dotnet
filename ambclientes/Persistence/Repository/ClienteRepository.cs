using Amb.Clientes.Domain.Entities;
using Amb.Clientes.Domain.Filters;
using Amb.Clientes.Domain.Models;
using Amb.Clientes.Persistence.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Amb.Clientes.Persistence.Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly DatabaseService _service;
    private readonly IMapper _mapper;
    private DbSet<Cliente> Entity => _service.Set<Cliente>();
    
    public ClienteRepository(DatabaseService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }


    public PaginatedModel<Cliente> Get(BaseFilter filter)
    {
        var pageSize = filter.PageSize ?? 10;
        var currentPage = filter.CurrentPage ?? 1;
        var searchText = filter.SearchText?.ToLower();

        var entities = _service.Cliente.AsNoTracking();

        var total = entities.Count();

        entities = entities
                .Where(s => string.IsNullOrEmpty(searchText) 
                             || s.Nombre.ToLower().Contains(searchText)
                             || s.Apellido.ToLower().Contains(searchText));

        var count = entities.Count();
        
        var results = entities
            .Skip(pageSize * (currentPage - 1))
            .Take(pageSize);

        return new PaginatedModel<Cliente>
        {
            Total = total,
            Count = count,
            Result = results
        };
    }

    public virtual IQueryable<Cliente> GetAll(int limit = 100)
    {
        return Entity.Take(limit).AsNoTracking().AsQueryable();
    }

    public virtual async Task<Cliente> GetById(int id)
    {
        var entity = await Entity
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (entity is null)
            throw new FileNotFoundException("Entity with Id was not found");

        return entity;
    }

    public async Task<Cliente> Create(ClienteDto data)
    {
        var entity = _mapper.Map<ClienteDto, Cliente>(data);
        if (entity is null)
            throw new ArgumentException("Data value is not assignable to Entity");
        
        await Entity.AddAsync(entity);
        var result = await _service.SaveChangesAsync();
        if (result < 1) throw new DbUpdateException();
        
        return entity;
    }

    public async Task<Cliente> Update(int id, ClienteDto data)
    {
        if (data is null) 
            throw new ArgumentNullException(nameof(data), "Data must have a value");
        
        var entity = await Entity.FirstOrDefaultAsync(e => e.Id == id);

        if (entity is null)
            throw new FileNotFoundException("Entity with Id was not found");

        _mapper.Map<ClienteDto, Cliente>(data, entity);
        
        var result = await _service.SaveChangesAsync();
        if (result < 1) throw new DbUpdateException();
        
        return entity;
    }
}