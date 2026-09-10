namespace Dominio.Excepciones;

public sealed class ContactoNoEncontradoException : DomainException
{
    private ContactoNoEncontradoException(string message)
        : base(message)
    {
    }

    public static ContactoNoEncontradoException BecauseIdWasNotFound(
        string id)
    {
        return new ContactoNoEncontradoException(
            $"No existe un contacto con el ID: {id}"
        );
    }
}