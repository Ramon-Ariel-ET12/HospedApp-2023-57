using HotelApp.Core;
namespace HotelApp.Test;

public class TestAdoHotel_Cuarto : TestAdo
{
    [Fact]
    public void ObtenerHotel_Cuarto()
    {
        var hotel_Cuarto = Ado.ObtenerHotel_Cuarto();
        Assert.NotEmpty(hotel_Cuarto);
    }

    [Theory]
    [InlineData(1, 1)]
    public void ObtenerHotel_CuartoPorId(ushort IdHotel, byte IdCuarto)
    {
        var hotel_Cuarto = Ado.ObtenerHotel_CuartoPorId(IdHotel, IdCuarto);
        Assert.NotNull(hotel_Cuarto);
    }

    [Fact]
    public void AltaHotel_Cuarto()
    {
        var nuevahotel_Cuarto = new Hotel_Cuarto()
        {
            IdHotel = 1,
            IdCuarto = 2,
        };
        Ado.AltaHotel_Cuarto(nuevahotel_Cuarto);
    }
}