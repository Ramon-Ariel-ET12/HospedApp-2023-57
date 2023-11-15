namespace HotelApp.Core;

public class Cuarto
{
    public uint IdCuarto;
    public bool Cochera;
    public double Noche;
    public string Descripcion;

    public Cuarto (uint IdCuarto, bool Cochera, double Noche, string Descripcion){
        this.IdCuarto = IdCuarto;
        this.Cochera = Cochera;
        this.Noche = Noche;
        this.Descripcion = Descripcion;
    }
}
