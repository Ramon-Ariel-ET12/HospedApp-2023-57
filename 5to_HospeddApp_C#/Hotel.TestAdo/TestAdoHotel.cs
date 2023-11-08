using HotelApp.Core;
namespace HotelApp.Test;
public class TestAdoCategoria : TestAdo
{
    [Fact]
    public void TraerHoteles()
    {
        var hotel = Ado.ObtenerHotel();

        Assert.NotEmpty(hotel);
        //Pregunto por rubros que se dan de alta en "scripts/bd/MySQL/03 Inserts.sql"
        Assert.Contains(hotel, h => h.Nombre == "Hoteldeprueba");
        Assert.Contains(hotel, h => h.Domicilio == "En el Hotel");
    }
}