namespace HotelApp.Core;

public class Hotel
{
    public int IdHotel;
    public string Nombre;
    public string Domicilio;
    public string Email;
    public string Contraseña;
    public int Estrella;

    public Hotel (int IdHotel, string Nombre, string Domicilio, string Email, string Contraseña, int Estrella){
        this.IdHotel = IdHotel;
        this.Nombre = Nombre;
        this.Domicilio = Domicilio;
        this.Email = Email;
        this.Contraseña = Contraseña;
        this.Estrella = Estrella;
    }
}
