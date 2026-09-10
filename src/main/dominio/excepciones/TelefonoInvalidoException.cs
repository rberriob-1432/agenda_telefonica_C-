namespace Dominio.Excepciones;
public sealed class TelefonoInvalidoException : DomainException
{
    private const string TELEFONO_VACIO =
        "El teléfono no puede estar vacío.";

    private const string TELEFONO_MAL_FORMATO =
        "El formato del teléfono no es válido: {0}.";

    private TelefonoInvalidoException(string message)
        : base(message)
    {
    }

    public static TelefonoInvalidoException BecauseValueIsEmpty()
    {
        return new TelefonoInvalidoException(TELEFONO_VACIO);
    }

    public static TelefonoInvalidoException BecauseFormatIsInvalid(
        string value)
    {
        return new TelefonoInvalidoException(
            string.Format(TELEFONO_MAL_FORMATO, value)
        );
    }
}