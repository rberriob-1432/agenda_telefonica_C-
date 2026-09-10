using Aplicacion.Puertos.Entrada;
using Aplicacion.Puertos.Salida;
using Dominio.Modelo;

namespace Aplicacion.Servicios;

public sealed class ExportarContactosServicio
    : IExportarContactosCasoUso
{
    private readonly IConseguirTodosContactosPuerto
        conseguirTodosContactosPuerto;

    private readonly IExportarContactosPuerto
        exportarContactosPuerto;

    public ExportarContactosServicio(
        IConseguirTodosContactosPuerto conseguirTodosContactosPuerto,
        IExportarContactosPuerto exportarContactosPuerto)
    {
        this.conseguirTodosContactosPuerto =
            conseguirTodosContactosPuerto;

        this.exportarContactosPuerto =
            exportarContactosPuerto;
    }

    public void Execute()
    {
        Contacto contacto =
            conseguirTodosContactosPuerto.GetAll();

        exportarContactosPuerto.Exportar(contacto);
    }
}