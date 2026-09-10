using System;
using System.Text.RegularExpressions;
using Dominio.Excepciones;

namespace Dominio.OV;

public record Telefono
{
    private static readonly Regex TELEFONO_PATTERN =
        new(@"^\d+$");

    public string Value { get; }

    public Telefono(string value)
    {
        string normalizedValue = value
            ?? throw new ArgumentNullException(
                nameof(value),
                "Telefono no puede ser nulo");

        normalizedValue = normalizedValue.Trim();

        ValidateNotEmpty(normalizedValue);
        ValidateFormat(normalizedValue);

        Value = normalizedValue;
    }

    private static void ValidateNotEmpty(string normalizedValue)
    {
        if (string.IsNullOrEmpty(normalizedValue))
        {
            throw TelefonoInvalidoException.BecauseValueIsEmpty();
        }
    }

    private static void ValidateFormat(string normalizedValue)
    {
        if (!TELEFONO_PATTERN.IsMatch(normalizedValue))
        {
            throw TelefonoInvalidoException
                .BecauseFormatIsInvalid(normalizedValue);
        }
    }

    public override string ToString()
    {
        return Value;
    }
}