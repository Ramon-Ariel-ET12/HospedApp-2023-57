namespace HotelApp.Core;

public interface IAdo
{
    List<Hotel> ObtenerHotel();
    Task<List<Hotel>> ObtenerHotelAsync();
    void AltaHotel(Hotel hotel);
    Task AltaHotelAsync(Hotel hotel);
    Hotel? ObtenerHotelPorId(ushort IdHotel);
    Task<Hotel?> ObtenerHotelPorIdAsync(ushort IdHotel);

    List<Cliente> ObtenerCliente();
    Task<List<Cliente>> ObtenerClienteAsync();
    Cliente? ObtenerClientePorCorreoContrasña(string Email, string Contraseña);
    Task<Cliente?> ObtenerClientePorCorreoContrasñaAsync(string Email, string Contraseña);
    void AltaCliente(Cliente cliente);
    Task AltaClienteAsync(Cliente cliente);

    List<Cama> ObtenerCama();
    Task<List<Cama>> ObtenerCamaAsync();
    Cama? ObtenerCamaPorId(byte IdCama);
    Task<Cama?> ObtenerCamaPorIdAsync(byte IdCama);
    void AltaCama(Cama cama);
    Task AltaCamaAsync(Cama cama);

    List<Cuarto> ObtenerCuarto();
    Task<List<Cuarto>> ObtenerCuartoAsync();
    Cuarto? ObtenerCuartoPorId(byte IdCuarto);
    Task<Cuarto?> ObtenerCuartoPorIdAsync(byte IdCuarto);
    void AltaCuarto(Cuarto cuarto);
    Task AltaCuartoAsync(Cuarto cuarto);

    List<Cuarto_Cama> ObtenerCuarto_Cama();
    Task<List<Cuarto_Cama>> ObtenerCuarto_CamaAsync();
    Cuarto_Cama? ObtenerCuarto_CamaPorIdCuarto(byte IdCuarto);
    Task<Cuarto_Cama?> ObtenerCuarto_CamaPorIdCuartoAsync(byte IdCuarto);
    void AltaCuarto_Cama(Cuarto_Cama cuarto_Cama);
    Task AltaCuarto_CamaAsync(Cuarto_Cama cuarto_Cama);

    List<Hotel_Cuarto> ObtenerHotel_Cuarto();
    Task<List<Hotel_Cuarto>> ObtenerHotel_CuartoAsync();
    Hotel_Cuarto? ObtenerHotel_CuartoPorId(ushort IdHotel, byte IdCuarto);
    Task<Hotel_Cuarto?> ObtenerHotel_CuartoPorIdAsync(ushort IdHotel, byte IdCuarto);
    void AltaHotel_Cuarto(Hotel_Cuarto hotel_Cuarto);
    Task AltaHotel_CuartoAsync(Hotel_Cuarto hotel_Cuarto);

    List<Reserva> ObtenerReserva();
    Task<List<Reserva>> ObtenerReservaAsync();
    Reserva? ObtenerReservaId(ushort IdReserva);
    Task<Reserva?> ObtenerReservaIdAsync(ushort IdReserva);
    void AltaReserva(Reserva reserva);
    Task AltaReservaAsync(Reserva reserva);
}