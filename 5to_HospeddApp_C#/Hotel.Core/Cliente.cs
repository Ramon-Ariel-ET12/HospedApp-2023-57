namespace HotelApp.Core;

public class Cliente{
    public int IdCliente;
    public string Nombre;
    public string Apellido;
    public string Email;
    public string Contraseña;
    public Cliente (int IdCliente, string Nombre, string Apellido, string Email, string Contraseña){
        this.IdCliente = IdCliente;
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Email = Email;
        this.Contraseña = Contraseña;
    }
}