using Aplicacion.Puertos.Entrada;
using Aplicacion.Servicios;
using Dominio.Modelo;
using Infraestructura.Adaptador.Persistencia.Archivo;
using Infraestructura.PuntosEntrada.Cli;
using Infraestructura.PuntosEntrada.Cli.IO;
using Infraestructura.PuntosEntrada.Cli.Manipulador;

namespace Infraestructura.Configuraciones;

public sealed class ContainerDependencia
{
    private readonly PropiedadesApp _propiedadesApp;
    private readonly Contacto _contacto;
    private readonly ContactoRepositorioTxt _repositorio;

    public ContainerDependencia()
    {
        _propiedadesApp =
            new PropiedadesApp();

        _contacto =
            new Contacto();

        _repositorio =
            new ContactoRepositorioTxt(
                _propiedadesApp,
                _contacto
            );
    }

    public AgendaTelefonicaCli AgendaTelefonicaCli(
        ConsolaIo consola)
    {
        ICrearContactoCasoUso crearContactoCasoUso =
            new CrearContactoServicio(
                _repositorio,
                _repositorio,
                _contacto
            );

        IActualizarContactoCasoUso
            actualizarContactoCasoUso =
            new ActualizarContactoServicio(
                _repositorio,
                _repositorio,
                _repositorio,
                _contacto
            );

        IConseguirContactoPorIdCasoUso
            conseguirContactoPorIdCasoUso =
            new ConseguirContactoPorIdServicio(
                _repositorio,
                _contacto
            );

        IConseguirTodosContactosCasoUso
            conseguirTodosContactosCasoUso =
            new ConseguirTodosContactosServicio(
                _repositorio
            );

        IEliminarContactoCasoUso
            eliminarContactoCasoUso =
            new EliminarContactoServicio(
                _repositorio,
                _repositorio
            );

        IBuscarContactosCasoUso
            buscarContactosCasoUso =
            new BuscarContactosServicio(
                _repositorio
            );

        ExportarContactosTxt
            exportarContactosTxt =
            new ExportarContactosTxt(
                _propiedadesApp
            );

        IExportarContactosCasoUso
            exportarContactosCasoUso =
            new ExportarContactosServicio(
                _repositorio,
                exportarContactosTxt
            );

        ContactoRespuestaImpresor impresor =
            new ContactoRespuestaImpresor(
                consola
            );

        CrearContactoManipulador
            crearManipulador =
            new CrearContactoManipulador(
                crearContactoCasoUso,
                consola
            );

        ActualizarContactoManipulador
            actualizarManipulador =
            new ActualizarContactoManipulador(
                actualizarContactoCasoUso,
                consola
            );

        ConseguirContactoPorIdManipulador
            buscarManipulador =
            new ConseguirContactoPorIdManipulador(
                conseguirContactoPorIdCasoUso,
                consola,
                impresor
            );

        BuscarContactosManipulador
            buscarContactosManipulador =
            new BuscarContactosManipulador(
                buscarContactosCasoUso,
                consola
            );

        ListarContactosManipulador
            listarManipulador =
            new ListarContactosManipulador(
                conseguirTodosContactosCasoUso,
                consola
            );

        EliminarContactoManipulador
            eliminarManipulador =
            new EliminarContactoManipulador(
                eliminarContactoCasoUso,
                consola
            );

        ExportarContactosManipulador
            exportarContactosManipulador =
            new ExportarContactosManipulador(
                exportarContactosCasoUso,
                consola
            );

        return new AgendaTelefonicaCli(
            consola,
            listarManipulador,
            buscarManipulador,
            crearManipulador,
            actualizarManipulador,
            buscarContactosManipulador,
            eliminarManipulador,
            exportarContactosManipulador
        );
    }

    public AgendaTelefonicaCli AgendaTelefonicaCli()
    {
        ConsolaIo consola =
            new ConsolaIo(
                Console.In,
                Console.Out
            );

        return AgendaTelefonicaCli(consola);
    }
}

