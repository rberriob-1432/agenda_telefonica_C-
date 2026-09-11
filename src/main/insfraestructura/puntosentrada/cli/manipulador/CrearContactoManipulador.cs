using Aplicacion.Puertos.Entrada;
using Aplicacion.Servicios.Dto.Comando;
using Dominio.Excepciones;
using Infraestructura.PuntosEntrada.Cli.IO;

namespace Infraestructura.PuntosEntrada.Cli.Manipulador;

public sealed class CrearContactoManipulador
    : IOperacionManipulador
{
    private readonly ICrearContactoCasoUso _crearContactoCasoUso;
    private readonly ConsolaIo _consola;

    public CrearContactoManipulador(
        ICrearContactoCasoUso crearContactoCasoUso,
        ConsolaIo consola)
    {
        _crearContactoCasoUso = crearContactoCasoUso;
        _consola = consola;
    }

    public void Manejar()
    {
        string nombre =
            _consola.ReadRequired(
                "Nombre   : "
            );

        string telefono =
            _consola.ReadRequired(
                "Teléfono : "
            );

        string correo =
            _consola.ReadRequired(
                "Correo   : "
            );

        AgregarContactoComando comando =
            new(
                nombre,
                telefono,
                correo
            );

        try
        {
            _crearContactoCasoUso.Execute(comando);

            _consola.Println(
                "\nContacto creado correctamente."
            );
        }
        catch (CorreoYaRegistradoException excepcion)
        {
            _consola.Println(
                "Error: " + excepcion.Message
            );
        }
    }
}

