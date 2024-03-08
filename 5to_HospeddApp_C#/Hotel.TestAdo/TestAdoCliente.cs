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
    [Theory]
    [InlineData("Quemirabobo@gmail.com", "Andapalla")]
    public void ClientesPorCorreoContraseña(string Email, string Contraseña)
    {
        var cliente = Ado.ObtenerClientePorCorreoContrasña(Email, Contraseña);

        Assert.NotNull(cliente);
    }
}
