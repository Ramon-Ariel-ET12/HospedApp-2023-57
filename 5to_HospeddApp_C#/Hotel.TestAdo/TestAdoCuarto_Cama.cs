using HotelApp.Core;
namespace HotelApp.Test;
public class TestAdoCuarto_Cama : TestAdo
{
    [Fact]
    public void ObtenerCuarto_Cama()
    {
        var cuarto_cama = Ado.ObtenerCama();

        Assert.NotEmpty(cuarto_cama);
    }

    [Theory]
    [InlineData(1)]
    public void ObtenerCuarto_CamaPorIdCuarto(byte IdCuarto)
    {
        var cuarto_cama = Ado.ObtenerCuarto_CamaPorIdCuarto(IdCuarto);

        Assert.NotNull(cuarto_cama);
    }

    [Fact]
    public void AltaCuarto_Cama()
    {
        var nuevocuarto_Cama = new Cuarto_Cama()
        {
            IdCuarto = 2,
            IdCama = 2,
            Cantidad_de_cama = 3,
        };

        Ado.AltaCuarto_Cama(nuevocuarto_Cama);
    }
}