namespace Infraestructura.PuntosEntrada.Cli.Menu;
public sealed class OpcionMenu
{
    public static readonly OpcionMenu ListarContactos =
        new(1, "Listar todos los contactos");

    public static readonly OpcionMenu BuscarContacto =
        new(2, "Buscar contacto por ID");

    public static readonly OpcionMenu BuscarContactos =
        new(3, "Búsqueda avanzada");

    public static readonly OpcionMenu CrearContacto =
        new(4, "Crear contacto");

    public static readonly OpcionMenu ActualizarContacto =
        new(5, "Actualizar contacto");

    public static readonly OpcionMenu EliminarContacto =
        new(6, "Eliminar contacto");

    public static readonly OpcionMenu ExportarContactos =
        new(7, "Exportar contactos");

    public static readonly OpcionMenu Salir =
        new(0, "Salir");

    public int Numero { get; }

    public string Descripcion { get; }

    private OpcionMenu(
        int numero,
        string descripcion)
    {
        Numero = numero;
        Descripcion = descripcion;
    }

    public static OpcionMenu? DesdeNumero(int numero)
    {
        return numero switch
        {
            1 => ListarContactos,
            2 => BuscarContacto,
            3 => BuscarContactos,
            4 => CrearContacto,
            5 => ActualizarContacto,
            6 => EliminarContacto,
            7 => ExportarContactos,
            0 => Salir,
            _ => null
        };
    }

    public static OpcionMenu[] Todas()
    {
        return
        [
            ListarContactos,
            BuscarContacto,
            BuscarContactos,
            CrearContacto,
            ActualizarContacto,
            EliminarContacto,
            ExportarContactos,
            Salir
        ];
    }
}

