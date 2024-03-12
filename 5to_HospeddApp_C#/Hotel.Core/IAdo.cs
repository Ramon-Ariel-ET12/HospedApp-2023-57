namespace HotelApp.Core;

public interface IAdo
{
    List<Hotel> ObtenerHotel();
    void AltaHotel(Hotel hotel);
    Hotel? ObtenerHotelPorId(ushort id);

    List<Cliente> ObtenerCliente();
    Cliente? ObtenerClientePorCorreoContrasña(string Email, string Contraseña);
    void AltaCliente(Cliente cliente);

    List<Cama> ObtenerCama();
    Cama? ObtenerCamaPorId(byte IdCama);
    void AltaCama(Cama cama);
}