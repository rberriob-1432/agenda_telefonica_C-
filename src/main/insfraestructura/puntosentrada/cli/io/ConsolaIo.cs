using System;
using System.Text.RegularExpressions;

namespace Infraestructura.PuntosEntrada.Cli.IO;

public sealed class ConsolaIo
{
    private readonly TextReader _reader;
    private readonly TextWriter _out;

    public ConsolaIo(
        TextReader reader,
        TextWriter output)
    {
        _reader = reader;
        _out = output;
    }

    public string ReadRequired(string prompt)
    {
        string value;

        do
        {
            _out.Write(prompt);
            value = _reader.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                _out.WriteLine(
                    "El valor no puede estar vacío. Intente nuevamente."
                );
            }

        } while (string.IsNullOrWhiteSpace(value));

        return value;
    }

    public string ReadOptional(string prompt)
    {
        _out.Write(prompt);

        return _reader.ReadLine()?.Trim()
               ?? string.Empty;
    }

    public int ReadInt(string prompt)
    {
        while (true)
        {
            _out.Write(prompt);

            string raw =
                _reader.ReadLine()?.Trim()
                ?? string.Empty;

            if (int.TryParse(raw, out int value))
            {
                return value;
            }

            _out.WriteLine(
                "Entrada inválida. Ingrese un número."
            );
        }
    }

    public void Println(string message)
    {
        _out.WriteLine(message);
    }

    public void Println()
    {
        _out.WriteLine();
    }

    public void Printf(
        string format,
        params object[] args)
    {
        string formatoCSharp =
            ConvertirFormatoJava(format);

        _out.Write(
            string.Format(
                formatoCSharp,
                args
            )
        );
    }

    private static string ConvertirFormatoJava(
        string formato)
    {
        int indice = 0;

        formato =
            Regex.Replace(
                formato,
                @"%[ds]",
                _ => "{" + indice++ + "}"
            );

        return formato.Replace(
            "%n",
            Environment.NewLine
        );
    }
}