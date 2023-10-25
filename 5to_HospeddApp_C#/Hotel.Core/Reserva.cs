namespace HotelApp;

public class Reserva{
    public
        int reserva;
        List<Hotel> hotel;
        DateOnly inicio;
        DateOnly fin;
        List<Cliente> cliente;
        List<Cuarto> cuarto;
        int calificacion_del_cliente;
        int calificacion_del_hotel;
        int comentario_del_cliente;
        public Reserva (int reserva, DateOnly inicio, DateOnly fin, int calificacion_del_cliente, int calificacion_del_hotel, int comentario_del_cliente){
            this.reserva = reserva;
            hotel = new List<Hotel>();
            this.inicio = inicio;
            this.fin = fin;
            cliente = new List<Cliente>();
            cuarto = new List<Cuarto>();
            this.calificacion_del_cliente = calificacion_del_cliente;
            this.calificacion_del_hotel = calificacion_del_hotel;
            this.comentario_del_cliente = comentario_del_cliente;
        }
}