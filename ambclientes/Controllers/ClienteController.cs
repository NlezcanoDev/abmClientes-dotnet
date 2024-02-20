using Amb.Clientes.Application.Admin;
using Amb.Clientes.Domain.Filters;
using Amb.Clientes.Domain.Models;
using Amb.Clientes.Persistence.Repository;

namespace Amb.Clientes.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("api/clientes")]
[ApiController]
public class ClienteController: ControllerBase
{
    private ClienteAdmin _admin;

    public ClienteController(IClienteRepository repository)
    {
        _admin = new ClienteAdmin
        {
            Repository = repository
        };
    }
    
    [HttpGet]
    public IActionResult Get([FromQuery]BaseFilter filter)
    {
        var data = _admin.Get(filter);
        return Ok(data);
    }
    
    [HttpGet("all")]
    public IActionResult GetAll(){
        var data = _admin.GetAll();
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _admin.GetById(id);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClienteDto dto)
    {
        var data = await _admin.Create(dto);
        return Ok(data);    
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClienteDto dto)
    {
        var data = await _admin.Update(id, dto);
        return Ok(data);
    }
}