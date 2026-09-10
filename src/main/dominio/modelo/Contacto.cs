using Dominio.OV;

namespace Dominio.Modelo;

public class Contacto
{
    private const byte MAX_CONTACTOS = 100;

    private readonly Nombre[] nombres;
    private readonly Telefono[] telefonos;
    private readonly Correo[] correos;
    private readonly Id[] ids;

    private byte cantidadContactos;

    public Contacto()
    {
        nombres = new Nombre[MAX_CONTACTOS];
        telefonos = new Telefono[MAX_CONTACTOS];
        correos = new Correo[MAX_CONTACTOS];
        ids = new Id[MAX_CONTACTOS];

        cantidadContactos = 0;
    }

    public byte GetCantidadContactos()
    {
        return cantidadContactos;
    }

    public void SetCantidadContactos(byte cantidadContactos)
    {
        this.cantidadContactos = cantidadContactos;
    }

    public Nombre GetNombre(byte indice)
    {
        return nombres[indice];
    }

    public void SetNombre(byte indice, Nombre nombre)
    {
        nombres[indice] = nombre;
    }

    public Id GetId(byte indice)
    {
        return ids[indice];
    }

    public void SetId(byte indice, Id id)
    {
        ids[indice] = id;
    }

    public Telefono GetTelefono(byte indice)
    {
        return telefonos[indice];
    }

    public void SetTelefono(byte indice, Telefono telefono)
    {
        telefonos[indice] = telefono;
    }

    public Correo GetCorreo(byte indice)
    {
        return correos[indice];
    }

    public void SetCorreo(byte indice, Correo correo)
    {
        correos[indice] = correo;
    }
}