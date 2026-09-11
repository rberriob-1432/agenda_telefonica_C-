namespace Infraestructura.Configuraciones;

public sealed class ExcepcionConfiguracion : Exception
{
    private const string MensajeCarga =
        "No se pudo cargar la configuración de la aplicación.";

    private ExcepcionConfiguracion(
        string mensaje,
        Exception causa)
        : base(mensaje, causa)
    {
    }

    public static ExcepcionConfiguracion PorqueNoSePudoCargar(
        Exception causa)
    {
        return new ExcepcionConfiguracion(
            MensajeCarga,
            causa
        );
    }
}