namespace HotelApp.Core;

public class Cuarto_Cama
{
    public List<Cuarto> IdCuarto;
    public List<Cama> Tipo_de_cama;
    public int Cantidad_de_cama;
    public Cuarto_Cama (int Cantidad_de_cama){
        IdCuarto = new List<Cuarto>();
        Tipo_de_cama = new List<Cama>();
        this.Cantidad_de_cama = Cantidad_de_cama;
    }
}
