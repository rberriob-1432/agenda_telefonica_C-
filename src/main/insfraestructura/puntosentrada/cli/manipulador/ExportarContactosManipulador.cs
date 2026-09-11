using Aplicacion.Puertos.Entrada;
using Infraestructura.PuntosEntrada.Cli.IO;

namespace Infraestructura.PuntosEntrada.Cli.Manipulador;

public sealed class ExportarContactosManipulador
    : IOperacionManipulador
{
    private readonly IExportarContactosCasoUso _exportarContactosCasoUso;
    private readonly ConsolaIo _consola;

    public ExportarContactosManipulador(
        IExportarContactosCasoUso exportarContactosCasoUso,
        ConsolaIo consola)
    {
        _exportarContactosCasoUso = exportarContactosCasoUso;
        _consola = consola;
    }

    public void Manejar()
    {
        try
        {
            _exportarContactosCasoUso.Execute();

            _consola.Println(
                "Contactos exportados correctamente."
            );
        }
        catch (Exception excepcion)
        {
            _consola.Println(
                "Error al exportar: "
                + excepcion.Message
            );
        }
    }
}