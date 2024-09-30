namespace HotelApp.Core;

public class Cuarto_Cama
{
    public byte? IdCuarto { get; set; }
    public byte IdCama { get; set; }
    public byte Cantidad_de_cama { get; set; }

    public Cuarto Cuarto { get; set; }
    public Cama Cama { get; set; }
}
