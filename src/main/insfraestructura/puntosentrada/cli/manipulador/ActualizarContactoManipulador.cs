using Aplicacion.Puertos.Entrada;
using Aplicacion.Servicios.Dto.Comando;
using Dominio.Excepciones;
using Infraestructura.PuntosEntrada.Cli.IO;

namespace Infraestructura.PuntosEntrada.Cli.Manipulador;

public sealed class ActualizarContactoManipulador
    : IOperacionManipulador
{
    private readonly IActualizarContactoCasoUso _actualizarContactoCasoUso;
    private readonly ConsolaIo _consola;

    public ActualizarContactoManipulador(
        IActualizarContactoCasoUso actualizarContactoCasoUso,
        ConsolaIo consola)
    {
        _actualizarContactoCasoUso = actualizarContactoCasoUso;
        _consola = consola;
    }

    public void Manejar()
    {
        string id =
            _consola.ReadRequired(
                "ID del contacto                         : "
            );

        string nombre =
            _consola.ReadRequired(
                "Nuevo nombre                            : "
            );

        string correo =
            _consola.ReadRequired(
                "Nuevo correo                            : "
            );

        string telefono =
            _consola.ReadRequired(
                "Nuevo teléfono                          : "
            );

        ActualizarContactoComando comando =
            new(
                id,
                nombre,
                correo,
                telefono
            );

        try
        {
            _actualizarContactoCasoUso.Execute(comando);

            _consola.Println(
                "\nContacto actualizado correctamente."
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

