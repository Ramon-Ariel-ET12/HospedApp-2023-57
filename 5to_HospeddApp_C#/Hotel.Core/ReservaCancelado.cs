namespace HotelApp.Core;

public class ReservaCancelado{
    public ulong IdReserva { get; set; }
    public ushort IdHotel { get; set; }
    public string Inicio { get; set; }
    public string Fin { get; set; }
    public ushort Dni { get; set; }
    public byte IdCuarto { get; set; }
}