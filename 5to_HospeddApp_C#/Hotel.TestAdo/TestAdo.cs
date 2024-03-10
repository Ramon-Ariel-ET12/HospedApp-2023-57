using HotelApp.Core;
using HotelApp.Dapper;

namespace HotelApp.Test;
public class TestAdo
{
    protected readonly IAdo Ado;
    private const string _cadena = "Server=localhost;Database=5to_HospeddApp2023;Uid=root;pwd=root;Allow User Variables=True";
    public TestAdo() => Ado = new AdoDapper(_cadena);
    public TestAdo(string cadena) => Ado = new AdoDapper(cadena);
}
