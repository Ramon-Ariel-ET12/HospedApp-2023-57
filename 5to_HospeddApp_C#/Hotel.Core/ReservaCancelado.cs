namespace HotelApp;

public class ReservaCancelado{
    public
        int reserva;
        List<Hotel> hotel;
        DateOnly inicio;
        DateOnly fin;
        List<Cliente> cliente;
        List<Cuarto> cuarto;
        public ReservaCancelado (int reserva, DateOnly inicio, DateOnly fin){
            this.reserva = reserva;
            hotel = new List<Hotel>();
            this.inicio = inicio;
            this.fin = fin;
            cliente = new List<Cliente>();
            cuarto = new List<Cuarto>();
        }
}