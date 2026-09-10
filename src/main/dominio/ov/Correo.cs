using System;
using System.Text.RegularExpressions;
using Dominio.Excepciones;
namespace Dominio.OV;
public record Correo
{
    private static readonly Regex PATRON_CORREO =
        new(@"^[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$");

    public string Value { get; }

    public Correo(string value)
    {
        string normalizedValue = value
            ?? throw new ArgumentNullException(nameof(value),
                "Correo no puede ser null");

        normalizedValue = normalizedValue
            .Trim()
            .ToLower();

        ValidateNotEmpty(normalizedValue);
        ValidateFormat(normalizedValue);

        Value = normalizedValue;
    }

    private static void ValidateNotEmpty(string normalizedValue)
    {
        if (string.IsNullOrEmpty(normalizedValue))
        {
            throw CorreoInvalidoException.BecauseValueIsEmpty();
        }
    }

    private static void ValidateFormat(string normalizedValue)
    {
        if (!PATRON_CORREO.IsMatch(normalizedValue))
        {
            throw CorreoInvalidoException
                .BecauseFormatIsInvalid(normalizedValue);
        }
    }

    public override string ToString()
    {
        return Value;
    }
}