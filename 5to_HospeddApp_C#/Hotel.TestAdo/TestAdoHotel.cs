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
}

// cliente: si existe el correo y la contraseña