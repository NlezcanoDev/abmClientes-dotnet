namespace Amb.Clientes.Domain.Entities;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string Cuit { get; set; }
    public int Telefono { get; set; }
    public string Mail { get; set; }
}