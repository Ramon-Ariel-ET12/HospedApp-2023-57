using System.Data;
using Dapper;
using MySqlConnector;
using HotelApp.Core;

namespace HotelApp.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    //Este constructor usa por defecto la cadena para un conector MySQL
    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);
    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;


    #region 'Hotel'
    private readonly string _queryHotel
        = "SELECT * FROM Hotel";
    
    private readonly string _queryHotelPorId
        = "SELECT * FROM Hotel WHERE IdHotel = @unIdhotel";
    
    public List<Hotel> ObtenerHotel() => _conexion.Query<Hotel>(_queryHotel).ToList();

    public Hotel? ObtenerHotelPorId(ushort IdHotel) => 
    _conexion.QueryFirstOrDefault<Hotel>(_queryHotelPorId, new { unIdhotel = IdHotel });

    public void AltaHotel(Hotel hotel)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdHotel", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", hotel.Nombre);
        parametros.Add("@unDomicilio", hotel.Domicilio);
        parametros.Add("@unEmail", hotel.Email);
        parametros.Add("@unContraseña", hotel.Contraseña);
        parametros.Add("@unEstrella", hotel.Estrella);

        _conexion.Execute("AltaHotel", parametros);

        //Obtengo el valor de parametro de tipo salida
        hotel.IdHotel = parametros.Get<ushort>("@unIdHotel");
    }
    #endregion

    #region 'Cliente'
    private readonly string _queryCliente
        = "SELECT * FROM Cliente";
    private readonly string _queryClienteCorreoContraseña
        = @"SELECT * FROM Cliente WHERE Email = @unEmail AND Contraseña = SHA2(@unContraseña, 256)";

        
    public List<Cliente> ObtenerCliente() => _conexion.Query<Cliente>(_queryCliente).ToList();

    public Cliente? ObtenerClientePorCorreoContrasña(string Email, string Contraseña) => 
    _conexion.QueryFirstOrDefault<Cliente>(_queryClienteCorreoContraseña, new { unEmail = Email, unContraseña = Contraseña });


    public void AltaCliente(Cliente cliente)
    {
        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        parametros.Add("@unDni", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", cliente.Nombre);
        parametros.Add("@unApellido", cliente.Apellido);
        parametros.Add("@unEmail", cliente.Email);
        parametros.Add("@unContraseña", cliente.Contraseña);
        try
        {
            _conexion.Execute("RegistrarCliente", parametros);

            //Obtengo el valor de parametro de tipo salida
            cliente.Dni = parametros.Get<ushort>("@unDni");
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                throw new ConstraintException(cliente.Email + " ya se encuentra en uso.");
            }
            throw;
        }
    }
    #endregion
}