namespace HotelApp.Core;

public class Reserva{
    public ushort IdReserva { get; set; }
    public ushort IdHotel { get; set; }
    public string Inicio { get; set; }
    public string Fin { get; set; }
    public uint Dni { get; set; }
    public byte IdCuarto { get; set; }
    public uint Calificacion_del_cliente { get; set; }
    public uint Calificacion_del_hotel { get; set; }
    public string Comentario_del_cliente { get; set; }
}