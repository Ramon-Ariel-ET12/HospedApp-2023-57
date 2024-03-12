using HotelApp.Core;
namespace HotelApp.Test;
public class TestAdoCama : TestAdo
{
    [Fact]
    public void ObtenerCama()
    {
        var cama = Ado.ObtenerCama();

        Assert.NotEmpty(cama);
    }

    [Theory]
    [InlineData(1)]
    public void ObtenerCamaPorId(byte Tipo_de_cama)
    {
        var cama = Ado.ObtenerCamaPorId(Tipo_de_cama);

        Assert.NotNull(cama);
    }

    [Fact]
    public void AltaCama()
    {
        var nuevacama = new Cama()
        {
            Nombre = "Cucheta",
            Pueden_dormir = 2,
        };

        Ado.AltaCama(nuevacama);
    }
}
