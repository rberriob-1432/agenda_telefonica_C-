namespace Dominio.Excepciones;

public sealed class NombreInvalidoException : DomainException
{
    private const string NOMBRE_VACIO =
        "El nombre no puede estar vacío.";

    private const string NOMBRE_CORTO =
        "El nombre debe tener al menos {0} caracteres.";

    private NombreInvalidoException(string message)
        : base(message)
    {
    }

    public static NombreInvalidoException BecauseValueIsEmpty()
    {
        return new NombreInvalidoException(NOMBRE_VACIO);
    }

    public static NombreInvalidoException BecauseLengthIsTooShort(
        int minimumLength)
    {
        return new NombreInvalidoException(
            string.Format(NOMBRE_CORTO, minimumLength)
        );
    }
}