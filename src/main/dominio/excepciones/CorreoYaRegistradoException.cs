namespace Dominio.Excepciones;

public sealed class CorreoYaRegistradoException : DomainException
{
    private CorreoYaRegistradoException(string message)
        : base(message)
    {
    }

    public static CorreoYaRegistradoException BecauseEmailWasAlreadyRegistered(
        string correo)
    {
        return new CorreoYaRegistradoException(
            "El correo ya está registrado: " + correo
        );
    }
}