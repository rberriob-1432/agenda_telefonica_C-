using System;
using Dominio.Excepciones;
namespace Dominio.OV;

public record Id
{
    public string Value { get; }

    public Id(string value)
    {
        string normalizedValue = value
            ?? throw new ArgumentNullException(
                nameof(value),
                "Id no puede ser nulo");

        normalizedValue = normalizedValue.Trim();

        ValidateNotEmpty(normalizedValue);

        Value = normalizedValue;
    }

    private static void ValidateNotEmpty(string normalizedValue)
    {
        if (string.IsNullOrEmpty(normalizedValue))
        {
            throw InvalidoIdExcepcion.BecauseValueIsEmpty();
        }
    }

    public override string ToString()
    {
        return Value;
    }
}