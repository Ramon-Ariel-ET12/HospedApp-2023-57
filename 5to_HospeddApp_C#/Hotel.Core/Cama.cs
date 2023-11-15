namespace HotelApp.Core;

public class Cama
{
    public uint Tipo_de_cama;
    public string Nombre;
    public uint Pueden_dormir;
    public Cama (uint Tipo_de_cama, string Nombre, uint Pueden_dormir){
        this.Tipo_de_cama = Tipo_de_cama;
        this.Nombre = Nombre;
        this.Pueden_dormir = Pueden_dormir;
    }
}
