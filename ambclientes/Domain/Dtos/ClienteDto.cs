namespace Amb.Clientes.Domain.Models;

public class ClienteDto
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public string Cuit { get; set; }
    public int Telefono { get; set; }
    public string Mail { get; set; }
}