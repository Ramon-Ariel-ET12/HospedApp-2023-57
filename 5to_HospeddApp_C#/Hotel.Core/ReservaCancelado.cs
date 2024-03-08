namespace HotelApp.Core;

public class ReservaCancelado{
    public ulong IdReserva;
    public ushort IdHotel;
    public string Inicio;
    public string Fin;
    public ushort IdCliente;
    public byte IdCuarto;
    public ReservaCancelado (ulong IdReserva, ushort IdHotel, string Inicio, string Fin, ushort IdCliente, byte IdCuarto){
        this.IdReserva = IdReserva;
        this.IdHotel = IdHotel;
        this.Inicio = Inicio;
        this.Fin = Fin;
        this.IdCliente = IdCliente;
        this.IdCuarto = IdCuarto;
    }
}