namespace HotelApp.Core;

public class ReservaCancelado{
    public ushort? IdReserva { get; set; }
    public ushort? IdHotel { get; set; }
    public string Inicio { get; set; }
    public string Fin { get; set; }
    public uint? Dni { get; set; }
    public byte? IdCuarto { get; set; }

    public Hotel Hotel { get; set; } 
    public Cliente Cliente { get; set; }
    public Cuarto Cuarto { get; set; }
    
    public Hotel_Cuarto HotelCuarto { get; set; }  
}