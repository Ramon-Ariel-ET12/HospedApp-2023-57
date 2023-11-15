namespace HotelApp.Core;

public class Cliente{
    public UInt32 IdCliente;
    public string Nombre;
    public string Apellido;
    public string Email;
    public string Contraseña;
    public Cliente (UInt32 IdCliente, string Nombre, string Apellido, string Email, string Contraseña){
        this.IdCliente = IdCliente;
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Email = Email;
        this.Contraseña = Contraseña;
    }
}