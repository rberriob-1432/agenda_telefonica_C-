using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Infraestructura.Configuraciones;

public sealed class PropiedadesApp
{
    private const string ARCHIVO_PROPIEDADES =
        "application.properties";

    private readonly Dictionary<string, string> propiedades;

    public PropiedadesApp()
    {
        Assembly assembly =
            typeof(PropiedadesApp).Assembly;

        using Stream? flujo =
            assembly.GetManifestResourceStream(
                ARCHIVO_PROPIEDADES
            );

        propiedades = Cargar(flujo);
    }

    internal PropiedadesApp(Stream flujo)
    {
        propiedades = Cargar(flujo);
    }

    private static Dictionary<string, string> Cargar(
        Stream? flujo)
    {
        if (flujo is null)
        {
            throw new InvalidOperationException(
                "Archivo no encontrado: "
                + ARCHIVO_PROPIEDADES
            );
        }

        var propiedades =
            new Dictionary<string, string>();

        using (flujo)
        using (var lector = new StreamReader(flujo))
        {
            while (lector.ReadLine() is string linea)
            {
                linea = linea.Trim();

                if (string.IsNullOrEmpty(linea)
                    || linea.StartsWith("#"))
                {
                    continue;
                }

                string[] partes =
                    linea.Split(
                        '=',
                        2,
                        StringSplitOptions.TrimEntries
                    );

                if (partes.Length == 2)
                {
                    propiedades[partes[0]] = partes[1];
                }
            }
        }

        return propiedades;
    }

    public string Obtener(string clave)
    {
        if (!propiedades.TryGetValue(
                clave,
                out string? valor))
        {
            throw new InvalidOperationException(
                "Propiedad no encontrada en "
                + ARCHIVO_PROPIEDADES
                + ": "
                + clave
            );
        }

        return valor;
    }

    public int ObtenerEntero(string clave)
    {
        return int.Parse(Obtener(clave));
    }
}