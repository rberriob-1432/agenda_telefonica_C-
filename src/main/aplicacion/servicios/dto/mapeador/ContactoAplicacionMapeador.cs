using Aplicacion.Servicios.Dto.Comando;
using Aplicacion.Servicios.Dto.Query;
using Dominio.OV;

namespace Aplicacion.Servicios.Dto.Mapeador;

public static class ContactoAplicacionMapeador
{
    public static Id FromGetByIdQueryToId(
        ConseguirContactoPorIdQuery query)
    {
        return new Id(query.Id);
    }

    public static Id FromDeleteCommandToId(
        EliminarContactoComando comando)
    {
        return new Id(comando.Id);
    }

    public static Nombre FromCreateCommandToNombre(
        AgregarContactoComando comando)
    {
        return new Nombre(comando.Nombre);
    }

    public static Telefono FromCreateCommandToTelefono(
        AgregarContactoComando comando)
    {
        return new Telefono(comando.Telefono);
    }

    public static Correo FromCreateCommandToCorreo(
        AgregarContactoComando comando)
    {
        return new Correo(comando.Correo);
    }

    public static Id FromUpdateCommandToId(
        ActualizarContactoComando comando)
    {
        return new Id(comando.Id);
    }

    public static Nombre FromUpdateCommandToNombre(
        ActualizarContactoComando comando)
    {
        return new Nombre(comando.Nombre);
    }

    public static Telefono FromUpdateCommandToTelefono(
        ActualizarContactoComando comando)
    {
        return new Telefono(comando.Telefono);
    }

    public static Correo FromUpdateCommandToCorreo(
        ActualizarContactoComando comando)
    {
        return new Correo(comando.Correo);
    }
}