namespace HotelApp.Core;

public class Hotel
{
    public ushort IdHotel;
    public string Nombre;
    public string Domicilio;
    public string Email;
    public string Contraseña;
    public byte Estrella;

    public Hotel (ushort IdHotel, string Nombre, string Domicilio, string Email, string Contraseña, byte Estrella){
        this.IdHotel = IdHotel;
        this.Nombre = Nombre;
        this.Domicilio = Domicilio;
        this.Email = Email;
        this.Contraseña = Contraseña;
        this.Estrella = Estrella;
    }
}
