namespace HotelApp.Core;

public class Reserva{
    public ulong IdReserva { get; set; }
    public ushort IdHotel { get; set; }
    public string Inicio { get; set; }
    public string Fin { get; set; }
    public ushort Dni { get; set; }
    public byte IdCuarto { get; set; }
    public uint Calificacion_del_cliente { get; set; }
    public uint Calificacion_del_hotel { get; set; }
    public uint Comentario_del_cliente { get; set; }
}