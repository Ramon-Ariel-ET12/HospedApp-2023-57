using HotelApp.Core;
namespace HotelApp.Test;
public class TestAdoCliente : TestAdo
{
    [Fact]
    public void TraerClientes()
    {
        var cliente = Ado.ObtenerCliente();

        Assert.NotEmpty(cliente);
    }
    [Fact]
    public void ClientesPorCorreoContraseña()
    {
        var cliente = Ado.ObtenerCliente("Quemirabobo@gmail.com", "Andapalla");

        Assert.Null(cliente);
    }
}
