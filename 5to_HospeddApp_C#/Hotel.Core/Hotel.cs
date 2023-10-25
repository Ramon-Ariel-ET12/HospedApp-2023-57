namespace HotelApp;

public class Hotel
{
    public 
        int hotel;
        string nombre;
        string domicilio;
        string email;
        string contraseña;
        int estrella;

    public Hotel (int hotel, string nombre, string domicilio, string email, string contraseña, int estrella){
        this.hotel = hotel;
        this.nombre = nombre;
        this.domicilio = domicilio;
        this.email = email;
        this.contraseña = contraseña;
        this.estrella = estrella;
    }
}
