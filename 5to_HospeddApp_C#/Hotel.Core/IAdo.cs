namespace HotelApp;

public interface IAdo
{
    void AltaHotel(Hotel hotel);
    List<Hotel> ObtenerHotel();
    void AltaCliente(Cliente cliente);
    List<Cliente> ObtenerCliente();
    Cliente? ObtenerCliente(short id);
    Hotel? ObtenerHotel(short id);
}