using System.Data;
using Dapper;
using MySqlConnector;
using HotelApp;

namespace HotelApp.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    private readonly string _queryHotel
        = "SELECT hotel AS ID, nombre AS Hotel FROM Hotel";
    private readonly string _queryCliente
        = @"SELECT  cliente, nombre AS Nombre FROM Cliente";

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;

    //Este constructor usa por defecto la cadena para un conector MySQL
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);

    public void AltaCliente(Cliente cliente)
    {
        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        parametros.Add("@unIdCliente", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", cliente.Nombre);
        parametros.Add("@unApellido", cliente.Apellido);
        parametros.Add("@unEmail", cliente.Email);
        parametros.Add("@unContraseña", cliente.Contraseña);

        _conexion.Execute("altaCliente", parametros);

        //Obtengo el valor de parametro de tipo salida
        cliente.IdCliente = parametros.Get<byte>("@unIdRubro");
    }

    public void AltaHotel(Hotel hotel)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdHotel", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", hotel.Nombre);
        parametros.Add("@unDomicilio", hotel.Domicilio);
        parametros.Add("@unEmail", hotel.Email);
        parametros.Add("@unContraseña", hotel.Contraseña);
        parametros.Add("@unEstrella", hotel.Estrella);

        _conexion.Execute("altaProducto", parametros);

        //Obtengo el valor de parametro de tipo salida
        hotel.IdHotel = parametros.Get<short>("@unIdProducto");
    }

    public List<Cliente> ObtenerCliente() => _conexion.Query<Cliente>(_queryCliente).ToList();

    public Cliente? ObtenerCliente(short id)
    {
        throw new NotImplementedException();
    }

    public List<Hotel> ObtenerHotel() => _conexion.Query<Hotel>(_queryHotel).ToList();

    public Hotel? ObtenerHotel(short id)
    {
        throw new NotImplementedException();
    }
}
