using Infraestructura.PuntosEntrada.Cli.IO;
using Infraestructura.PuntosEntrada.Cli.Manipulador;
using Infraestructura.PuntosEntrada.Cli.Menu;

namespace Infraestructura.PuntosEntrada.Cli;

public sealed class AgendaTelefonicaCli
{
    private readonly ConsolaIo _consola;
    private readonly ListarContactosManipulador _listarManipulador;
    private readonly ConseguirContactoPorIdManipulador _buscarManipulador;
    private readonly CrearContactoManipulador _crearManipulador;
    private readonly ActualizarContactoManipulador _actualizarManipulador;
    private readonly BuscarContactosManipulador _buscarContactosManipulador;
    private readonly EliminarContactoManipulador _eliminarManipulador;
    private readonly ExportarContactosManipulador _exportarContactosManipulador;

    public AgendaTelefonicaCli(
        ConsolaIo consola,
        ListarContactosManipulador listarManipulador,
        ConseguirContactoPorIdManipulador buscarManipulador,
        CrearContactoManipulador crearManipulador,
        ActualizarContactoManipulador actualizarManipulador,
        BuscarContactosManipulador buscarContactosManipulador,
        EliminarContactoManipulador eliminarManipulador,
        ExportarContactosManipulador exportarContactosManipulador)
    {
        _consola = consola;
        _listarManipulador = listarManipulador;
        _buscarManipulador = buscarManipulador;
        _crearManipulador = crearManipulador;
        _actualizarManipulador = actualizarManipulador;
        _buscarContactosManipulador = buscarContactosManipulador;
        _eliminarManipulador = eliminarManipulador;
        _exportarContactosManipulador = exportarContactosManipulador;
    }

    public void Start()
    {
        bool ejecutando = true;

        while (ejecutando)
        {
            MostrarMenu();

            int numero =
                _consola.ReadInt(
                    "Seleccione una opción: "
                );

            OpcionMenu? opcion =
                OpcionMenu.DesdeNumero(numero);

            if (opcion == null)
            {
                _consola.Println(
                    "Opción inválida. Intente nuevamente."
                );

                continue;
            }

            switch (opcion)
            {
                case var _ when opcion == OpcionMenu.ListarContactos:
                    _listarManipulador.Manejar();
                    break;

                case var _ when opcion == OpcionMenu.BuscarContacto:
                    _buscarManipulador.Manejar();
                    break;

                case var _ when opcion == OpcionMenu.BuscarContactos:
                    _buscarContactosManipulador.Manejar();
                    break;

                case var _ when opcion == OpcionMenu.CrearContacto:
                    _crearManipulador.Manejar();
                    break;

                case var _ when opcion == OpcionMenu.ActualizarContacto:
                    _actualizarManipulador.Manejar();
                    break;

                case var _ when opcion == OpcionMenu.EliminarContacto:
                    _eliminarManipulador.Manejar();
                    break;

                case var _ when opcion == OpcionMenu.ExportarContactos:
                    _exportarContactosManipulador.Manejar();
                    break;

                case var _ when opcion == OpcionMenu.Salir:
                    _consola.Println(
                        "Saliendo de la Agenda Telefónica..."
                    );

                    ejecutando = false;
                    break;
            }

            if (ejecutando)
            {
                _consola.Println();
            }
        }
    }

    private void MostrarMenu()
    {
        _consola.Println(
            "=========================================="
        );

        _consola.Println(
            "          AGENDA TELEFÓNICA"
        );

        _consola.Println(
            "=========================================="
        );

        foreach (OpcionMenu opcion in OpcionMenu.Todas())//error
        {
            _consola.Printf(
                "%d. %s%n",
                opcion.Numero,
                opcion.Descripcion
            );
        }

        _consola.Println(
            "=========================================="
        );
    }
}

