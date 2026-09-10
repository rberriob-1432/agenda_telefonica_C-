using System;

namespace Infraestructura.Adaptador.Persistencia.Excepcion;

public sealed class ExcepcionPersistencia : Exception
{
    private const string MENSAJE_GUARDAR =
        "No se pudo guardar el contacto con ID: '{0}'.";

    private const string MENSAJE_ACTUALIZAR =
        "No se pudo actualizar el contacto con ID: '{0}'.";

    private const string MENSAJE_BUSCAR =
        "No se pudo buscar el contacto con ID: '{0}'.";

    private const string MENSAJE_CORREO =
        "No se pudo buscar el contacto con correo: '{0}'.";

    private const string MENSAJE_TODOS =
        "No se pudieron recuperar todos los contactos.";

    private const string MENSAJE_ELIMINAR =
        "No se pudo eliminar el contacto con ID: '{0}'.";

    private const string MENSAJE_ARCHIVO =
        "No se pudo acceder al archivo de persistencia.";

    private ExcepcionPersistencia(
        string mensaje,
        Exception causa)
        : base(mensaje, causa)
    {
    }

    public static ExcepcionPersistencia PorqueGuardarFallo(
        string id,
        Exception causa)
    {
        return new ExcepcionPersistencia(
            string.Format(MENSAJE_GUARDAR, id),
            causa
        );
    }

    public static ExcepcionPersistencia PorqueActualizarFallo(
        string id,
        Exception causa)
    {
        return new ExcepcionPersistencia(
            string.Format(MENSAJE_ACTUALIZAR, id),
            causa
        );
    }

    public static ExcepcionPersistencia PorqueBuscarPorIdFallo(
        string id,
        Exception causa)
    {
        return new ExcepcionPersistencia(
            string.Format(MENSAJE_BUSCAR, id),
            causa
        );
    }

    public static ExcepcionPersistencia PorqueBuscarPorCorreoFallo(
        string correo,
        Exception causa)
    {
        return new ExcepcionPersistencia(
            string.Format(MENSAJE_CORREO, correo),
            causa
        );
    }

    public static ExcepcionPersistencia PorqueBuscarTodosFallo(
        Exception causa)
    {
        return new ExcepcionPersistencia(
            MENSAJE_TODOS,
            causa
        );
    }

    public static ExcepcionPersistencia PorqueEliminarFallo(
        string id,
        Exception causa)
    {
        return new ExcepcionPersistencia(
            string.Format(MENSAJE_ELIMINAR, id),
            causa
        );
    }

    public static ExcepcionPersistencia PorqueArchivoFallo(
        Exception causa)
    {
        return new ExcepcionPersistencia(
            MENSAJE_ARCHIVO,
            causa
        );
    }
}