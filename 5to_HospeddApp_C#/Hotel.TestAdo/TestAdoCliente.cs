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
    public void ClientesPorCorreoContrasena(string Email, string Contrasena)
    {
        var cliente = Ado.ObtenerClientePorCorreoContrasña(Email, Contrasena);

        Assert.NotNull(cliente);
    }

    [Fact]
    public void AltaCliente()
    {
        var nuevocliente = new Cliente()
        {
            Dni = 95205995,
            Nombre = "Kakaroto",
            Apellido = "Gohan",
            Email = "Goku12345678@gmail.com",
            Contrasena = "Vegueta777",
        };

        Ado.AltaCliente(nuevocliente);
        
    }
}
