namespace HotelApp.Core;
public class Hotel_Cuarto
{
    public ushort? IdHotel { get; set; }
    public byte IdCuarto{ get; set; }
	public byte Numero { get; set; }

    public Hotel Hotel { get; set; }
    public Cuarto Cuarto { get; set; }
}