namespace Dominio.Excepciones;

public abstract class DomainException : Exception
{
    protected DomainException(string message)
        : base(message)
    {
    }
}
