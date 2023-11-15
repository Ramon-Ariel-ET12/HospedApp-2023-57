namespace HotelApp.Core;

public class Reserva{
    public uint IdReserva;
    public List<Hotel> IdHotel;
    public DateOnly Inicio;
    public DateOnly Fin;
    public List<Cliente> IdCliente;
    public List<Cuarto> IdCuarto;
    public uint Calificacion_del_cliente;
    public uint Calificacion_del_hotel;
    public uint Comentario_del_cliente;
        public Reserva (uint IdReserva, DateOnly Inicio, DateOnly Fin, uint Calificacion_del_cliente, uint Calificacion_del_hotel, uint Comentario_del_cliente){
            this.IdReserva = IdReserva;
            IdHotel = new List<Hotel>();
            this.Inicio = Inicio;
            this.Fin = Fin;
            IdCliente = new List<Cliente>();
            IdCuarto = new List<Cuarto>();
            this.Calificacion_del_cliente = Calificacion_del_cliente;
            this.Calificacion_del_hotel = Calificacion_del_hotel;
            this.Comentario_del_cliente = Comentario_del_cliente;
        }
}