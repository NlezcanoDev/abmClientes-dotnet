namespace Amb.Clientes.Domain.Models;

public class PaginatedModel<T>
{
    public int Total { get; set; }
    public int Count { get; set; }
    public IQueryable<T> Result { get; set; }
}