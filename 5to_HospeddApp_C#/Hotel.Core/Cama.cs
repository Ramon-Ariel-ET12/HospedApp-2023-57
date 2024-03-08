namespace HotelApp.Core;

public class Cama
{
    public byte Tipo_de_cama;
    public string Nombre;
    public byte Pueden_dormir;
    public Cama (byte Tipo_de_cama, string Nombre, byte Pueden_dormir){
        this.Tipo_de_cama = Tipo_de_cama;
        this.Nombre = Nombre;
        this.Pueden_dormir = Pueden_dormir;
    }
}
