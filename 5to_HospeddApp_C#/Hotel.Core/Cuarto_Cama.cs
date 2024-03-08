namespace HotelApp.Core;

public class Cuarto_Cama
{
    public byte IdCuarto;
    public byte Tipo_de_cama;
    public byte Cantidad_de_cama;
    public Cuarto_Cama (byte IdCuarto, byte Tipo_de_cama, byte Cantidad_de_cama){
        this.IdCuarto = IdCuarto;
        this.Tipo_de_cama = Tipo_de_cama;
        this.Cantidad_de_cama = Cantidad_de_cama;
    }
}
