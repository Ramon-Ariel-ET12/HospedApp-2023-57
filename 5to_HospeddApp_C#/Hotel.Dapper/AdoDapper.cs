using System.Data;
using Dapper;
using MySqlConnector;
using HotelApp.Core;
using System.Reflection.Metadata;

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

        try
        {
            _conexion.Execute("AltaHotel", parametros);

            //Obtengo el valor de parametro de tipo salida
            hotel.IdHotel = parametros.Get<ushort>("@unIdHotel");
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                // Verificar si el error es por el Email
                if (error.Message.Contains("Email"))
                {
                    throw new ConstraintException("El Email " + hotel.Email + " ya se encuentra en uso.");
                }
            }
            throw;
        }
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
        parametros.Add("@unDni", cliente.Dni);
        parametros.Add("@unNombre", cliente.Nombre);
        parametros.Add("@unApellido", cliente.Apellido);
        parametros.Add("@unEmail", cliente.Email);
        parametros.Add("@unContraseña", cliente.Contraseña);
        try
        {
            _conexion.Execute("RegistrarCliente", parametros);

            //Obtengo el valor de parametro de tipo salida
            cliente.Dni = parametros.Get<uint>("@unDni");
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                // Verificar si el error es por el Email o por el Dni
                if (error.Message.Contains("Email"))
                {
                    throw new ConstraintException("El Email " + cliente.Email + " ya se encuentra en uso.");
                }
                else if (error.Message.Contains("PRIMARY"))
                {
                    throw new ConstraintException("El Dni " + cliente.Dni + " ya se encuentra registrado.");
                }
            }
            throw;
        }
    }
    #endregion

    #region 'Cama'

    private readonly string _queryCama
        = "SELECT * FROM Cama";

    private readonly string _queryCamaPorId
        = "SELECT * FROM Cama WHERE IdCama = @unIdCama";

    public List<Cama> ObtenerCama() => _conexion.Query<Cama>(_queryCama).ToList();
    public Cama? ObtenerCamaPorId(byte IdCama) => 
    _conexion.QueryFirstOrDefault<Cama>(_queryCamaPorId, new { unIdCama = IdCama });
    public void AltaCama(Cama cama)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdCama", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", cama.Nombre);
        parametros.Add("@unPueden_dormir", cama.Pueden_dormir);

        try
        {
            _conexion.Execute("AltaCama", parametros);

            //Obtengo el valor de parametro de tipo salida
            cama.IdCama = parametros.Get<byte>("@unIdCama");
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                // Verificar si el error es por el Nombre
                if (error.Message.Contains("Nombre"))
                {
                    throw new ConstraintException("El nombre " + cama.Nombre + " ya se encuentra en uso.");
                }
            }
            throw;
        }
    }

    #endregion

    #region 'Cuarto'
    private readonly string _queryCuarto
    = "SELECT * FROM Cuarto";
    private readonly string _queryCuartoPorId
    = "SELECT * FROM Cuarto WHERE IdCuarto = @unIdCuarto";
    
    public List<Cuarto> ObtenerCuarto() => _conexion.Query<Cuarto>(_queryCuarto).ToList();

    public Cuarto? ObtenerCuartoPorId(byte IdCuarto) => _conexion.QueryFirstOrDefault<Cuarto>(_queryCuartoPorId, new { unIdCuarto = IdCuarto });

    public void AltaCuarto(Cuarto cuarto)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdCuarto", direction: ParameterDirection.Output);
        parametros.Add("@unCochera", cuarto.Cochera);
        parametros.Add("@unNoche", cuarto.Noche);
        parametros.Add("@unDescripcion", cuarto.Descripcion);

        try
        {
            _conexion.Execute("AltaCuarto", parametros);

            //Obtengo el valor de parametro de tipo salida
            cuarto.IdCuarto = parametros.Get<byte>("@unIdCuarto");
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (error.Message.Contains("Descripcion"))
                {
                    throw new ConstraintException("La Descripcion " + cuarto.Descripcion + " ya se encuentra en uso.");
                }
            }
            throw;
        }
    }

    #endregion

    #region 'Cuarto_Cama'

    private readonly string _queryCuarto_Cama 
    = "SELECT * FROM Cuarto_Cama";
    private readonly string _queryCuarto_CamaPorIdCuarto
    = "SELECT * FROM Cuarto_Cama WHERE IdCuarto = @unIdCuarto";

    public List<Cuarto_Cama> ObtenerCuarto_Cama() => _conexion.Query<Cuarto_Cama>(_queryCuarto_Cama).ToList();

    public Cuarto_Cama? ObtenerCuarto_CamaPorIdCuarto(byte IdCuarto) => _conexion.QueryFirstOrDefault<Cuarto_Cama>(_queryCuarto_CamaPorIdCuarto, new { unIdCuarto = IdCuarto });
    
    public void AltaCuarto_Cama(Cuarto_Cama cuarto_Cama) 
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdCuarto", cuarto_Cama.IdCuarto);
        parametros.Add("@unIdCama", cuarto_Cama.IdCama);
        parametros.Add("@unCantidad_de_cama", cuarto_Cama.Cantidad_de_cama);

        try
        {
            _conexion.Execute("AltaCuarto_Cama", parametros);

            //Obtengo el valor de parametro de tipo salida
            cuarto_Cama.IdCuarto = parametros.Get<byte>("@unIdCuarto");
            cuarto_Cama.IdCama = parametros.Get<byte>("@unIdCama");
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (error.Message.Contains("PRIMARY"))
                {
                    throw new ConstraintException("El IdCuarto " + cuarto_Cama.IdCuarto + " y el IdCama " + cuarto_Cama.IdCama + " ya se encuentra en uso.");
                }
            }
            throw;
        }
    }

    #endregion

    //SELECT Cuarto.IdCuarto, IdCama, Noche, Cochera, Cantidad_de_cama, Descripcion FROM Cuarto INNER JOIN Cuarto_Cama ON Cuarto.IdCuarto = Cuarto_Cama.IdCuarto WHERE Cuarto.IdCuarto = @unIdCuarto
}