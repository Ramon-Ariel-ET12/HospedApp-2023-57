using HotelApp.Core;
namespace HotelApp.Test;

public class TestAdoCuarto : TestAdo
{
    [Fact]
    public void ObtenerCuarto()
    {
        var cuarto = Ado.ObtenerCuarto();

        Assert.NotEmpty(cuarto);
    }

    [Theory]
    [InlineData(1)]
    public void ObtenerCuartoPorId(byte IdCuarto)
    {
        var cuarto = Ado.ObtenerCuartoPorId(IdCuarto);

        Assert.NotNull(cuarto);
    }

    [Fact]
    public void AltaCuarto()
    {
        var nuevocuarto = new Cuarto()
        {
            Cochera = true,
            Noche = 50,
            Descripcion = "Comodo xd",
        };

        Ado.AltaCuarto(nuevocuarto);
    }
}