using Amb.Clientes.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Amb.Clientes.Persistence.Configuration;

public class ClienteConfiguration
{
    public ClienteConfiguration(EntityTypeBuilder<Cliente> entityBuilder)
    {
        entityBuilder.HasKey(x => x.Id);
        entityBuilder.Property(x => x.Nombre).IsRequired();
        entityBuilder.Property(x => x.Apellido).IsRequired();
        entityBuilder.Property(x => x.FechaNacimiento);
        entityBuilder.Property(x => x.Cuit).IsRequired().HasMaxLength(15);
        entityBuilder.Property(x => x.Telefono).IsRequired().HasMaxLength(10);
        entityBuilder.Property(x => x.Mail).IsRequired();

        entityBuilder.HasIndex(x => x.Cuit).IsUnique();
        entityBuilder.HasIndex(x => x.Mail).IsUnique();
    }
}