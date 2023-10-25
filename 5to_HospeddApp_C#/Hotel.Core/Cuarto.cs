namespace HotelApp;

public class Cuarto
{
    public 
        int cuarto;
        bool cochera;
        double noche;
        string descripcion;

    public Cuarto (int cuarto, bool cochera, double noche, string descripcion){
        this.cuarto = cuarto;
        this.cochera = cochera;
        this.noche = noche;
        this.descripcion = descripcion;
    }
}
