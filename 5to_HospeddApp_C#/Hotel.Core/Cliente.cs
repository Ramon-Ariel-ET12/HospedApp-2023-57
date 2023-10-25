namespace HotelApp;

public class Cliente{
    public 
        int cliente;
        string nombre;
        string apellido;
        string email;
        string contraseña;
    public Cliente (int cliente, string nombre, string apellido, string email, string contraseña){
        this.cliente = cliente;
        this.nombre = nombre;
        this.apellido = apellido;
        this.email = email;
        this.contraseña = contraseña;
    }
}