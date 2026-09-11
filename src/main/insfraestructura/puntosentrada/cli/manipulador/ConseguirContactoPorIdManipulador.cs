using Aplicacion.Puertos.Entrada;
using Aplicacion.Servicios.Dto;
using Aplicacion.Servicios.Dto.Query;
using Dominio.Excepciones;
using Infraestructura.PuntosEntrada.Cli.IO;

namespace Infraestructura.PuntosEntrada.Cli.Manipulador;

public sealed class ConseguirContactoPorIdManipulador
    : IOperacionManipulador
{
    private readonly IConseguirContactoPorIdCasoUso
        _conseguirContactoPorIdCasoUso;

    private readonly ConsolaIo _consola;

    private readonly ContactoRespuestaImpresor _impresor;

    public ConseguirContactoPorIdManipulador(
        IConseguirContactoPorIdCasoUso conseguirContactoPorIdCasoUso,
        ConsolaIo consola,
        ContactoRespuestaImpresor impresor)
    {
        _conseguirContactoPorIdCasoUso =
            conseguirContactoPorIdCasoUso;

        _consola = consola;
        _impresor = impresor;
    }

    public void Manejar()
    {
        string id =
            _consola.ReadRequired(
                "ID del contacto: "
            );

        ConseguirContactoPorIdQuery query =
            new(id);

        try
        {
            ContactoRespuestaDto contacto =
                _conseguirContactoPorIdCasoUso.Execute(query);

            _impresor.Imprimir(contacto);//error
        }
        catch (ContactoNoEncontradoException excepcion)
        {
            _consola.Println(
                "No encontrado: " + excepcion.Message
            );
        }
    }
}

