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

    private DynamicParameters ParametrosAltaHotel(Hotel hotel)
    {
        var parametros = new DynamicParameters();

        parametros.Add("@unIdHotel", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", hotel.Nombre);
        parametros.Add("@unDomicilio", hotel.Domicilio);
        parametros.Add("@unEmail", hotel.Email);
        parametros.Add("@unContraseña", hotel.Contraseña);
        parametros.Add("@unEstrella", hotel.Estrella);

        return parametros;
    }

    private readonly string _queryHotel
        = "SELECT * FROM Hotel";
    private readonly string _queryHotelPorId
        = "SELECT * FROM Hotel WHERE IdHotel = @unIdhotel";

    public List<Hotel> ObtenerHotel() => _conexion.Query<Hotel>(_queryHotel).ToList();

    public async Task<List<Hotel>> ObtenerHotelAsync()
    {
        var hotel = (await _conexion.QueryAsync<Hotel>(_queryHotel)).ToList();
        return hotel;
    }

    public Hotel? ObtenerHotelPorId(ushort IdHotel) =>
    _conexion.QueryFirstOrDefault<Hotel>(_queryHotelPorId, new { unIdhotel = IdHotel });

    public async Task<Hotel?> ObtenerHotelPorIdAsync(ushort IdHotel)
    {
        var hotel = await _conexion.QueryFirstOrDefaultAsync<Hotel>(_queryHotelPorId, new { unIdhotel = IdHotel });
        return hotel;
    }

    public void AltaHotel(Hotel hotel)
    {
        var parametros = ParametrosAltaHotel(hotel);
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

    public async Task AltaHotelAsync(Hotel hotel)
    {
        var parametros = ParametrosAltaHotel(hotel);
        try
        {
            await _conexion.ExecuteAsync("AltaHotel", parametros);

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

    private DynamicParameters ParametrosAltaCliente(Cliente cliente)
    {
        var parametros = new DynamicParameters();

        parametros.Add("@unDni", cliente.Dni);
        parametros.Add("@unNombre", cliente.Nombre);
        parametros.Add("@unApellido", cliente.Apellido);
        parametros.Add("@unEmail", cliente.Email);
        parametros.Add("@unContraseña", cliente.Contraseña);

        return parametros;
    }

    private readonly string _queryCliente
        = "SELECT * FROM Cliente";

    private readonly string _searchCliente
        = @"CALL BuscarCliente(@Busqueda)";
    private readonly string _queryClienteCorreoContraseña
        = @"SELECT * FROM Cliente WHERE Email = @unEmail AND Contraseña = SHA2(@unContraseña, 256)";


    public List<Cliente> ObtenerCliente() => _conexion.Query<Cliente>(_queryCliente).ToList();

    public async Task<List<Cliente>> ObtenerClienteAsync()
    {
        var cliente = (await _conexion.QueryAsync<Cliente>(_queryCliente)).ToList();
        return cliente;
    }
    public async Task<IEnumerable<Cliente>> BuscarClienteAsync(string Busqueda)
        => await _conexion.QueryAsync<Cliente>(_searchCliente, new { Busqueda = Busqueda });
    public Cliente? ObtenerClientePorCorreoContrasña(string Email, string Contraseña) =>
    _conexion.QueryFirstOrDefault<Cliente>(_queryClienteCorreoContraseña, new { unEmail = Email, unContraseña = Contraseña });

    public async Task<Cliente?> ObtenerClientePorCorreoContrasñaAsync(string Email, string Contraseña)
    {
        var cliente = await _conexion.QueryFirstOrDefaultAsync<Cliente>(_queryClienteCorreoContraseña, new { unEmail = Email, unContraseña = Contraseña });
        return cliente;
    }

    public void AltaCliente(Cliente cliente)
    {
        //Preparo los parametros del Stored Procedure
        var parametros = ParametrosAltaCliente(cliente);
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

    public async Task AltaClienteAsync(Cliente cliente)
    {
        //Preparo los parametros del Stored Procedure
        var parametros = ParametrosAltaCliente(cliente);
        try
        {
            await _conexion.ExecuteAsync("RegistrarCliente", parametros);

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

    private DynamicParameters ParametrosAltaCama(Cama cama)
    {
        var parametros = new DynamicParameters();

        parametros.Add("@unIdCama", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", cama.Nombre);
        parametros.Add("@unPueden_dormir", cama.Pueden_dormir);

        return parametros;
    }

    private readonly string _queryCama
        = "SELECT * FROM Cama";

    private readonly string _queryCamaPorId
        = "SELECT * FROM Cama WHERE IdCama = @unIdCama";

    public List<Cama> ObtenerCama() => _conexion.Query<Cama>(_queryCama).ToList();

    public async Task<List<Cama>> ObtenerCamaAsync()
    {
        var cama = (await _conexion.QueryAsync<Cama>(_queryCama)).ToList();
        return cama;
    }
    public Cama? ObtenerCamaPorId(byte IdCama) =>
    _conexion.QueryFirstOrDefault<Cama>(_queryCamaPorId, new { unIdCama = IdCama });

    public async Task<Cama?> ObtenerCamaPorIdAsync(byte IdCama)
    {
        var cama = await _conexion.QueryFirstOrDefaultAsync<Cama>(_queryCamaPorId, new { unIdCama = IdCama });
        return cama;
    }

    public void AltaCama(Cama cama)
    {
        var parametros = ParametrosAltaCama(cama);

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

    public async Task AltaCamaAsync(Cama cama)
    {
        var parametros = ParametrosAltaCama(cama);

        try
        {
            await _conexion.ExecuteAsync("AltaCama", parametros);

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

    private DynamicParameters ParametrosAltaCuarto(Cuarto cuarto)
    {
        var parametros = new DynamicParameters();

        parametros.Add("@unIdCuarto", direction: ParameterDirection.Output);
        parametros.Add("@unCochera", cuarto.Cochera);
        parametros.Add("@unNoche", cuarto.Noche);
        parametros.Add("@unDescripcion", cuarto.Descripcion);

        return parametros;
    }
    private readonly string _queryCuarto
    = "SELECT * FROM Cuarto";
    private readonly string _queryCuartoPorId
    = "SELECT * FROM Cuarto WHERE IdCuarto = @unIdCuarto";

    public List<Cuarto> ObtenerCuarto() => _conexion.Query<Cuarto>(_queryCuarto).ToList();

    public async Task<List<Cuarto>> ObtenerCuartoAsync()
    {
        var cuarto = (await _conexion.QueryAsync<Cuarto>(_queryCuarto)).ToList();
        return cuarto;
    }

    public Cuarto? ObtenerCuartoPorId(byte IdCuarto)
    => _conexion.QueryFirstOrDefault<Cuarto>(_queryCuartoPorId, new { unIdCuarto = IdCuarto });

    public async Task<Cuarto?> ObtenerCuartoPorIdAsync(byte IdCuarto)
    {
        var cuarto = await _conexion.QueryFirstOrDefaultAsync<Cuarto>(_queryCuartoPorId, new { unIdCuarto = IdCuarto });
        return cuarto;
    }

    public void AltaCuarto(Cuarto cuarto)
    {
        var parametros = ParametrosAltaCuarto(cuarto);

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

    public async Task AltaCuartoAsync(Cuarto cuarto)
    {
        var parametros = ParametrosAltaCuarto(cuarto);

        try
        {
            await _conexion.ExecuteAsync("AltaCuarto", parametros);

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

    private DynamicParameters ParametrosAltaCuarto_Cama(Cuarto_Cama cuarto_Cama)
    {
        var parametros = new DynamicParameters();

        parametros.Add("@unIdCuarto", cuarto_Cama.IdCuarto);
        parametros.Add("@unIdCama", cuarto_Cama.IdCama);
        parametros.Add("@unCantidad_de_cama", cuarto_Cama.Cantidad_de_cama);

        return parametros;
    }

    private readonly string _queryCuarto_Cama
    = "SELECT * FROM Cuarto_Cama";
    private readonly string _queryCuarto_CamaPorIdCuarto
    = "SELECT * FROM Cuarto_Cama WHERE IdCuarto = @unIdCuarto";

    public List<Cuarto_Cama> ObtenerCuarto_Cama() =>
    _conexion.Query<Cuarto_Cama>(_queryCuarto_Cama).ToList();

    public async Task<List<Cuarto_Cama>> ObtenerCuarto_CamaAsync()
    {
        var cuarto_Cama = (await _conexion.QueryAsync<Cuarto_Cama>(_queryCuarto_Cama)).ToList();
        return cuarto_Cama;
    }

    public Cuarto_Cama? ObtenerCuarto_CamaPorIdCuarto(byte IdCuarto) =>
    _conexion.QueryFirstOrDefault<Cuarto_Cama>(_queryCuarto_CamaPorIdCuarto, new { unIdCuarto = IdCuarto });

    public async Task<Cuarto_Cama?> ObtenerCuarto_CamaPorIdCuartoAsync(byte IdCuarto)
    {
        var cuarto_Cama = await _conexion.QueryFirstOrDefaultAsync<Cuarto_Cama>(_queryCuarto_CamaPorIdCuarto, new { unIdCuarto = IdCuarto });
        return cuarto_Cama;
    }

    public void AltaCuarto_Cama(Cuarto_Cama cuarto_Cama)
    {
        var parametros = ParametrosAltaCuarto_Cama(cuarto_Cama);

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

    public async Task AltaCuarto_CamaAsync(Cuarto_Cama cuarto_Cama)
    {
        var parametros = ParametrosAltaCuarto_Cama(cuarto_Cama);

        try
        {
            await _conexion.ExecuteAsync("AltaCuarto_Cama", parametros);

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

    #region 'Hotel_Cuarto'

    private DynamicParameters ParametrosAltaHotel_Cuarto(Hotel_Cuarto hotel_Cuarto)
    {
        var parametros = new DynamicParameters();

        parametros.Add("@unIdHotel", hotel_Cuarto.IdHotel);
        parametros.Add("@unIdCuarto", hotel_Cuarto.IdCuarto);
        parametros.Add("@unPrecio", hotel_Cuarto.Numero);

        return parametros;
    }

    private readonly string _queryHotel_Cuarto
    = "SELECT * FROM Hotel_Cuarto";
    private readonly string _queryHotel_CuartoPorId
    = "SELECT * FROM Hotel_Cuarto WHERE IdHotel = @unIdHotel";
    public List<Hotel_Cuarto> ObtenerHotel_Cuarto() =>
    _conexion.Query<Hotel_Cuarto>(_queryHotel_Cuarto).ToList();

    public async Task<List<Hotel_Cuarto>> ObtenerHotel_CuartoAsync()
    {
        var hotel_Cuarto = (await _conexion.QueryAsync<Hotel_Cuarto>(_queryHotel_Cuarto)).ToList();
        return hotel_Cuarto;
    }

    public Hotel_Cuarto? ObtenerHotel_CuartoPorId(ushort IdHotel, byte IdCuarto)
    => _conexion.QueryFirstOrDefault<Hotel_Cuarto>(_queryHotel_CuartoPorId, new { unIdHotel = IdHotel, unIdCuarto = IdCuarto });

    public async Task<Hotel_Cuarto?> ObtenerHotel_CuartoPorIdAsync(ushort IdHotel, byte IdCuarto)
    {
        var hotel_Cuarto = await _conexion.QueryFirstOrDefaultAsync<Hotel_Cuarto>(_queryHotel_CuartoPorId, new { unIdHotel = IdHotel, unIdCuarto = IdCuarto });
        return hotel_Cuarto;
    }

    public void AltaHotel_Cuarto(Hotel_Cuarto hotel_Cuarto)
    {
        var parametros = ParametrosAltaHotel_Cuarto(hotel_Cuarto);

        try
        {
            _conexion.Execute("AltaHotel_Cuarto", parametros);
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (error.Message.Contains("PRIMARY"))
                {
                    throw new ConstraintException("El IdHotel " + hotel_Cuarto.IdHotel + " y el IdCuarto " + hotel_Cuarto.IdCuarto + " ya se encuentra en uso.");
                }
            }
        }
    }

    public async Task AltaHotel_CuartoAsync(Hotel_Cuarto hotel_Cuarto)
    {
        var parametros = ParametrosAltaHotel_Cuarto(hotel_Cuarto);

        try
        {
            await _conexion.ExecuteAsync("AltaHotel_Cuarto", parametros);
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (error.Message.Contains("PRIMARY"))
                {
                    throw new ConstraintException("El IdHotel " + hotel_Cuarto.IdHotel + " y el IdCuarto " + hotel_Cuarto.IdCuarto + " ya se encuentra en uso.");
                }
            }
        }
    }
    #endregion

    #region 'Reserva'
    private DynamicParameters ParametrosAltaReserva(Reserva reserva)
    {
        var parametros = new DynamicParameters();

        parametros.Add("@unIdReserva", direction: ParameterDirection.Output);
        parametros.Add("@unIdHotel", reserva.IdHotel);
        parametros.Add("@unInicio", reserva.Inicio);
        parametros.Add("@unFin", reserva.Fin);
        parametros.Add("@unDni", reserva.Dni);
        parametros.Add("@unIdCuarto", reserva.IdCuarto);

        return parametros;
    }
    private readonly string _queryReserva
    = "SELECT * FROM Reserva";
    private readonly string _queryReservaPorId
    = "SELECT * FROM Reserva WHERE IdReserva = @unIdReserva";
    public List<Reserva> ObtenerReserva() => _conexion.Query<Reserva>(_queryReserva).ToList();

    public async Task<List<Reserva>> ObtenerReservaAsync()
    {
        var reserva = (await _conexion.QueryAsync<Reserva>(_queryReserva)).ToList();
        return reserva;
    }

    public Reserva? ObtenerReservaId(ushort IdReserva) => _conexion.QueryFirstOrDefault<Reserva>(_queryReservaPorId, new { unIdReserva = IdReserva});

    public async Task<Reserva?> ObtenerReservaIdAsync(ushort IdRreserva)
    {
        var reserva = await _conexion.QueryFirstOrDefaultAsync<Reserva>(_queryReservaPorId, new{ unIdReserva = IdRreserva });
        return reserva;
    }

    public void AltaReserva(Reserva reserva)
    {
        var parametros = ParametrosAltaReserva(reserva);

        try
        {
            _conexion.Execute("AltaReserva", parametros);
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (error.Message.Contains("IdHotel"))
                {
                    throw new ConstraintException("El IdCuarto " + reserva.IdCuarto + "No existe");
                }
            }
            throw;
        }
    }

    public async Task AltaReservaAsync(Reserva reserva)
    {
        var parametros = ParametrosAltaReserva(reserva);

        try
        {
            await _conexion.ExecuteAsync("AltaReserva", parametros);
        }
        catch (MySqlException error)
        {
            if (error.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
            {
                if (error.Message.Contains("IdHotel"))
                {
                    throw new ConstraintException("El IdCuarto " + reserva.IdCuarto + "No existe");
                }
            }
            throw;
        }
    }
    #endregion

}