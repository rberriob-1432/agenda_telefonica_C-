using Aplicacion.Puertos.Entrada;
using Aplicacion.Servicios.Dto;
using Aplicacion.Servicios.Dto.Comando;
using Infraestructura.PuntosEntrada.Cli.IO;

namespace Infraestructura.PuntosEntrada.Cli.Manipulador;

public sealed class BuscarContactosManipulador
    : IOperacionManipulador
{
    private readonly IBuscarContactosCasoUso _buscarContactosCasoUso;
    private readonly ConsolaIo _consola;

    public BuscarContactosManipulador(
        IBuscarContactosCasoUso buscarContactosCasoUso,
        ConsolaIo consola)
    {
        _buscarContactosCasoUso = buscarContactosCasoUso;
        _consola = consola;
    }

    public void Manejar()
    {
        _consola.Println(
            "=========================================="
        );

        _consola.Println(
            "          BÚSQUEDA AVANZADA"
        );

        _consola.Println(
            "=========================================="
        );

        _consola.Println("1. Buscar por nombre");
        _consola.Println("2. Buscar por correo");
        _consola.Println("0. Volver");

        int opcion =
            _consola.ReadInt(
                "Seleccione una opción: "
            );

        TipoBusqueda tipoBusqueda;

        switch (opcion)
        {
            case 1:
                tipoBusqueda = TipoBusqueda.NOMBRE;
                break;

            case 2:
                tipoBusqueda = TipoBusqueda.CORREO; 
                break;

            case 0:
                return;

            default:
                _consola.Println(
                    "Opción inválida."
                );
                return;
        }

        string criterio =
            _consola.ReadRequired(
                "Ingrese el criterio de búsqueda: "
            );

        BuscarContactosComando comando =
            new(
                criterio,
                tipoBusqueda
            );

        try
        {
            ContactoRespuestaDto[] resultados =
                _buscarContactosCasoUso.Execute(comando);//error

            ImprimirResultados(resultados);
        }
        catch (Exception excepcion)
        {
            _consola.Println(
                "Error: " + excepcion.Message
            );
        }
    }

    private void ImprimirResultados(
        ContactoRespuestaDto[] resultados)
    {
        if (resultados.Length == 0)
        {
            _consola.Println(
                "No se encontraron contactos."
            );

            return;
        }

        _consola.Println();

        _consola.Printf(
            "Contactos encontrados: %d%n",
            resultados.Length
        );

        _consola.Println();

        foreach (ContactoRespuestaDto contacto in resultados)
        {
            _consola.Println(
                "----------------------------------------"
            );

            _consola.Printf(
                "ID       : %s%n",
                contacto.Id
            );

            _consola.Printf(
                "Nombre   : %s%n",
                contacto.Nombre
            );

            _consola.Printf(
                "Teléfono : %s%n",
                contacto.Telefono
            );

            _consola.Printf(
                "Correo   : %s%n",
                contacto.Correo
            );
        }

        _consola.Println(
            "----------------------------------------"
        );
    }
}
