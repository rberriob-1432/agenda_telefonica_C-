using Aplicacion.Puertos.Entrada;
using Dominio.Modelo;
using Infraestructura.PuntosEntrada.Cli.IO;

namespace Infraestructura.PuntosEntrada.Cli.Manipulador;

public sealed class ListarContactosManipulador
    : IOperacionManipulador
{
    private readonly IConseguirTodosContactosCasoUso
        _conseguirTodosContactosCasoUso;

    private readonly ConsolaIo _consola;

    public ListarContactosManipulador(
        IConseguirTodosContactosCasoUso conseguirTodosContactosCasoUso,
        ConsolaIo consola)
    {
        _conseguirTodosContactosCasoUso =
            conseguirTodosContactosCasoUso;

        _consola = consola;
    }

    public void Manejar()
    {
        Contacto contactos =
            _conseguirTodosContactosCasoUso.Execute();

        if (contactos.GetCantidadContactos() == 0)
        {
            _consola.Println(
                "No hay contactos registrados."
            );

            return;
        }

        _consola.Printf(
            "%nTotal: %d contacto(s)%n",
            contactos.GetCantidadContactos()
        );

        for (
            byte i = 0;
            i < contactos.GetCantidadContactos();
            i++)
        {
            _consola.Println(
                "--------------------"
            );

            _consola.Printf(
                "ID       : %s%n",
                contactos.GetId(i).Value
            );

            _consola.Printf(
                "Nombre   : %s%n",
                contactos.GetNombre(i).Value
            );

            _consola.Printf(
                "Teléfono : %s%n",
                contactos.GetTelefono(i).Value
            );

            _consola.Printf(
                "Correo   : %s%n",
                contactos.GetCorreo(i).Value
            );
        }

        _consola.Println(
            "--------------------"
        );
    }
}
