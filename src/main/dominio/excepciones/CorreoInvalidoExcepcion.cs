namespace Dominio.Excepciones;
public sealed class CorreoInvalidoException : DomainException
{
    private const string CORREO_VACIO =
        "El correo no puede estar vacío.";

    private const string CORREO_MAL_FORMATO =
        "El formato del correo no es válido: {0}.";

    private CorreoInvalidoException(string message)
        : base(message)
    {
    }

    public static CorreoInvalidoException BecauseValueIsEmpty()
    {
        return new CorreoInvalidoException(CORREO_VACIO);
    }

    public static CorreoInvalidoException BecauseFormatIsInvalid(
        string value)
    {
        return new CorreoInvalidoException(
            string.Format(CORREO_MAL_FORMATO, value)
        );
    }
}