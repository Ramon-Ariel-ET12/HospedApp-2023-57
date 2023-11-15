namespace HotelApp.Core;

public class ReservaCancelado{
    public uint IdReserva;
    public List<Hotel> IdHotel;
    public DateOnly Inicio;
    public DateOnly Fin;
    public List<Cliente> IdCliente;
    public List<Cuarto> IdCuarto;
        public ReservaCancelado (uint IdReserva, DateOnly Inicio, DateOnly Fin){
            this.IdReserva = IdReserva;
            IdHotel = new List<Hotel>();
            this.Inicio = Inicio;
            this.Fin = Fin;
            IdCliente = new List<Cliente>();
            IdCuarto = new List<Cuarto>();
        }
}