namespace Dominio.Excepciones;
public sealed class InvalidoIdExcepcion : DomainException
{
    private const string MESSAGE_EMPTY = "Id no puede ser vacío";

    private InvalidoIdExcepcion(string message)
        : base(message)
    {
    }

    public static InvalidoIdExcepcion BecauseValueIsEmpty()
    {
        return new InvalidoIdExcepcion(MESSAGE_EMPTY);
    }
}