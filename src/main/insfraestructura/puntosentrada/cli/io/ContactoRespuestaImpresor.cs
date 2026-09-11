using Aplicacion.Servicios.Dto;

namespace Infraestructura.PuntosEntrada.Cli.IO;

public sealed class ContactoRespuestaImpresor
{
    private const string Separador =
        "----------------------------------------------------";

    private const string FormatoFila =
        "  {0,-10} : {1}\n";

    private readonly ConsolaIo _consola;

    public ContactoRespuestaImpresor(ConsolaIo consola)
    {
        _consola = consola;
    }

    public void Imprimir(ContactoRespuestaDto respuesta)
    {
        _consola.Println(Separador);
        _consola.Printf(FormatoFila, "ID", respuesta.Id);
        _consola.Printf(FormatoFila, "Nombre", respuesta.Nombre);
        _consola.Printf(FormatoFila, "Teléfono", respuesta.Telefono);
        _consola.Printf(FormatoFila, "Correo", respuesta.Correo);
        _consola.Println(Separador);
    }
}