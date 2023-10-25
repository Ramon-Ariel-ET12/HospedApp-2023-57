namespace HotelApp;

public class Cuarto_Cama
{
    public 
        List<Cuarto> cuarto;
        List<Cama> tipo_de_cama;
        int cantidad_de_cama;
    public Cuarto_Cama (int cantidad_de_cama){
        cuarto = new List<Cuarto>();
        tipo_de_cama = new List<Cama>();
        this.cantidad_de_cama = cantidad_de_cama;
    }
}
