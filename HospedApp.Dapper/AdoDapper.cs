using System.Data;
using Dapper;
using MySqlConnector;
namespace HospedApp.Dapper;

public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    //Este constructor usa por defecto la cadena para un conector MySQL
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);
    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;


}
