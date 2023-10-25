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
        parametros.Add("@uncliente", direction: ParameterDirection.Output);
        parametros.Add("@unnombre", categoria.Nombre);

        _conexion.Execute("altaCliente", parametros);

        //Obtengo el valor de parametro de tipo salida
        categoria.IdCategoria = parametros.Get<byte>("@unIdRubro");
    }

    public void AltaHotel(Hotel hotel)
    {
        throw new NotImplementedException();
    }

    public List<Cliente> ObtenerCliente()
    {
        throw new NotImplementedException();
    }

    public Cliente? ObtenerCliente(short id)
    {
        throw new NotImplementedException();
    }

    public List<Hotel> ObtenerHotel()
    {
        throw new NotImplementedException();
    }

    public Hotel? ObtenerHotel(short id)
    {
        throw new NotImplementedException();
    }
}
