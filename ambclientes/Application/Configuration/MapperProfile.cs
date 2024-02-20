using Amb.Clientes.Domain.Entities;
using Amb.Clientes.Domain.Models;
using AutoMapper;

namespace Amb.Clientes.Application.Configuration;

public class MapperProfile: Profile
{
    public MapperProfile()
    {
        CreateMap<Cliente, ClienteDto>().ReverseMap();
    }
}