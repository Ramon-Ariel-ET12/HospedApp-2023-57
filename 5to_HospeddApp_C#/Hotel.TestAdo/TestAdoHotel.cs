using HotelApp.Core;
namespace HotelApp.Test;
public class TestAdoHotel : TestAdo
{
    [Fact]
    public void TraerHoteles()
    {
        var hotel = Ado.ObtenerHotel();

        Assert.NotEmpty(hotel);
    }

    [Theory]
    [InlineData(1)]
    public void TraerHotelesPorId(ushort IdHotel)
    {
        var hotel = Ado.ObtenerHotelPorId(IdHotel);

        Assert.NotNull(hotel);
    }

    [Fact]
        public void AltaHotel()
    {
        var nuevohotel = new Hotel()
        {
            Nombre = "hotel",
            Domicilio = "por ahi",
            Email = "atencionalcliente@hotel.com",
            Contrasena = "contrasena",
            Estrella = 5,
        };

        Ado.AltaHotel(nuevohotel);
        
    }
}