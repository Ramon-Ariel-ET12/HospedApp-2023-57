namespace HotelApp.Core;

public class Reserva{
    public ulong IdReserva;
    public ushort IdHotel;
    public string Inicio;
    public string Fin;
    public ushort IdCliente;
    public byte IdCuarto;
    public uint Calificacion_del_cliente;
    public uint Calificacion_del_hotel;
    public uint Comentario_del_cliente;
        public Reserva (ulong IdReserva, ushort IdHotel, string Inicio, string Fin, ushort IdCliente, byte IdCuarto, uint Calificacion_del_cliente, uint Calificacion_del_hotel, uint Comentario_del_cliente){
            this.IdReserva = IdReserva;
            this.IdHotel = IdHotel;
            this.Inicio = Inicio;
            this.Fin = Fin;
            this.IdCliente = IdCliente;
            this.IdCuarto = IdCuarto;
            this.Calificacion_del_cliente = Calificacion_del_cliente;
            this.Calificacion_del_hotel = Calificacion_del_hotel;
            this.Comentario_del_cliente = Comentario_del_cliente;
        }
}