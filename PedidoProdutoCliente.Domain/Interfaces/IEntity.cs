namespace PedidoProdutoCliente.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime? DataExclusao { get; set; }
    }
}
