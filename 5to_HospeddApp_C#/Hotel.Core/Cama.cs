namespace HotelApp;

public class Cama
{
    public int Tipo_de_cama;
    public string Nombre;
    public int Pueden_dormir;
    public Cama (int Tipo_de_cama, string Nombre, int Pueden_dormir){
        this.Tipo_de_cama = Tipo_de_cama;
        this.Nombre = Nombre;
        this.Pueden_dormir = Pueden_dormir;
    }
}
