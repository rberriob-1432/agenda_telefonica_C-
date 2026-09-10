using System;
using Dominio.Excepciones;

namespace Dominio.OV;
public record Nombre
{
    private const int LONGITUD_MINIMA = 3;

    public string Value { get; }

    public Nombre(string value)
    {
        string normalizedValue = value
            ?? throw new ArgumentNullException(
                nameof(value),
                "Nombre no puede ser null");

        normalizedValue = normalizedValue.Trim();

        ValidateNotEmpty(normalizedValue);
        ValidateMinimumLength(normalizedValue);

        Value = normalizedValue;
    }

    private static void ValidateNotEmpty(string normalizedValue)
    {
        if (string.IsNullOrEmpty(normalizedValue))
        {
            throw NombreInvalidoException.BecauseValueIsEmpty();
        }
    }

    private static void ValidateMinimumLength(string normalizedValue)
    {
        if (normalizedValue.Length < LONGITUD_MINIMA)
        {
            throw NombreInvalidoException
                .BecauseLengthIsTooShort(LONGITUD_MINIMA);
        }
    }

    public override string ToString()
    {
        return Value;
    }
}