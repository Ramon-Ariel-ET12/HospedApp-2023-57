﻿using System.Data;
using Dapper;
using MySqlConnector;
using HotelApp.Core;

namespace HotelApp.Dapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    private readonly string _queryHotel
        = "SELECT * FROM Hotel";
    
    private readonly string _queryCliente
        = "SELECT * FROM Cliente";
    private readonly string _queryClienteCorreoContraseña
        = @"SELECT * FROM Cliente WHERE Email = @unEmail AND Contraseña = SHA2(@unContraseña, 256) Limit 1";
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
        cliente.IdCliente = parametros.Get<UInt32>("@unIdCliente");
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

        _conexion.Execute("altaHotel", parametros);

        //Obtengo el valor de parametro de tipo salida
        hotel.IdHotel = parametros.Get<UInt16>("@unIdHotel");
    }

    public List<Cliente> ObtenerCliente() => _conexion.Query<Cliente>(_queryCliente).ToList();

    public Cliente? ObtenerClientePorCorreoContrasña(string Email, string Contraseña) => 
    _conexion.QueryFirstOrDefault<Cliente>(_queryClienteCorreoContraseña, new { unEmail = Email, unContraseña = Contraseña });

    public List<Hotel> ObtenerHotel() => _conexion.Query<Hotel>(_queryHotel).ToList();

    public Hotel? ObtenerHotel(uint IdHotel) => 
    _conexion.QueryFirstOrDefault<Hotel>(_queryHotel, new { unIdhotel = IdHotel });
}