using HotelApp.Core;

namespace HotelApp.Test;
public class TestAdoProducto : TestAdo
{
    [Fact]
    public void TraerClientes()
    {
        var cliente = Ado.ObtenerCliente();

        Assert.NotEmpty(cliente);
        Assert.Contains(cliente, c => c.Nombre == "Leonel" && c.Apellido == "Messi");
    }
    [Fact]
    public void ClientesPorId()
    {
        var cliente = Ado.ObtenerCliente(1);

        Assert.NotNull(cliente);
        Assert.Equal("Leonel", cliente.Nombre);
    }
}
