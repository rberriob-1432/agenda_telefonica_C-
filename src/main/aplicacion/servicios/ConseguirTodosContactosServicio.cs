using Aplicacion.Puertos.Entrada;
using Aplicacion.Puertos.Salida;
using Dominio.Modelo;

namespace Aplicacion.Servicios;

public sealed class ConseguirTodosContactosServicio
    : IConseguirTodosContactosCasoUso
{
    private readonly IConseguirTodosContactosPuerto
        conseguirTodosContactosPuerto;

    public ConseguirTodosContactosServicio(
        IConseguirTodosContactosPuerto conseguirTodosContactosPuerto)
    {
        this.conseguirTodosContactosPuerto =
            conseguirTodosContactosPuerto;
    }

    public Contacto Execute()
    {
        return conseguirTodosContactosPuerto.GetAll();
    }
}