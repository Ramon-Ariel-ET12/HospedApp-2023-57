namespace HotelApp.Core;

public interface IAdo
{
    void AltaHotel(Hotel hotel);
    List<Hotel> ObtenerHotel();
    void AltaCliente(Cliente cliente);
    List<Cliente> ObtenerCliente();
    Cliente? ObtenerCliente(string Email, string Contraseña);
    Hotel? ObtenerHotel(uint id);
}