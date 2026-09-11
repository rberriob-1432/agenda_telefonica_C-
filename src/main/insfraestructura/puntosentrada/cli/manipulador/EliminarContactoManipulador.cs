using Aplicacion.Puertos.Entrada;
using Aplicacion.Servicios.Dto.Comando;
using Dominio.Excepciones;
using Infraestructura.PuntosEntrada.Cli.IO;

namespace Infraestructura.PuntosEntrada.Cli.Manipulador;

public sealed class EliminarContactoManipulador
    : IOperacionManipulador
{
    private readonly IEliminarContactoCasoUso _eliminarContactoCasoUso;
    private readonly ConsolaIo _consola;

    public EliminarContactoManipulador(
        IEliminarContactoCasoUso eliminarContactoCasoUso,
        ConsolaIo consola)
    {
        _eliminarContactoCasoUso = eliminarContactoCasoUso;
        _consola = consola;
    }

    public void Manejar()
    {
        string id =
            _consola.ReadRequired(
                "ID del contacto a eliminar: "
            );

        EliminarContactoComando comando =
            new(id);

        try
        {
            _eliminarContactoCasoUso.Execute(comando);

            _consola.Println(
                "Contacto eliminado correctamente."
            );
        }
        catch (ContactoNoEncontradoException excepcion)
        {
            _consola.Println(
                "No encontrado: " + excepcion.Message
            );
        }
    }
}