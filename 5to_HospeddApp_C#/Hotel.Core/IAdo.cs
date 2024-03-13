namespace HotelApp.Core;

public interface IAdo
{
    List<Hotel> ObtenerHotel();
    void AltaHotel(Hotel hotel);
    Hotel? ObtenerHotelPorId(ushort IdHotel);

    List<Cliente> ObtenerCliente();
    Cliente? ObtenerClientePorCorreoContrasña(string Email, string Contraseña);
    void AltaCliente(Cliente cliente);

    List<Cama> ObtenerCama();
    Cama? ObtenerCamaPorId(byte IdCama);
    void AltaCama(Cama cama);

    List<Cuarto> ObtenerCuarto();
    Cuarto? ObtenerCuartoPorId(byte IdCuarto);
    void AltaCuarto(Cuarto cuarto);

    List<Cuarto_Cama> ObtenerCuarto_Cama();
    Cuarto_Cama? ObtenerCuarto_CamaPorIdCuarto(byte IdCuarto);
    void AltaCuarto_Cama (Cuarto_Cama cuarto_Cama);
}