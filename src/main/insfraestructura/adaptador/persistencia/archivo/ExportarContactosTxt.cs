using System;
using System.Collections.Generic;
using System.IO;
using Aplicacion.Puertos.Salida;
using Dominio.Modelo;
using Infraestructura.Adaptador.Persistencia.Excepcion;
using Infraestructura.Configuraciones;

namespace Infraestructura.Adaptador.Persistencia.Archivo;

public sealed class ExportarContactosTxt
    : IExportarContactosPuerto
{
    private const string FORMATO_LINEA =
        "{0}|{1}|{2}|{3}";

    private readonly PropiedadesApp propiedadesApp;

    public ExportarContactosTxt(
        PropiedadesApp propiedadesApp)
    {
        this.propiedadesApp = propiedadesApp;
    }

    public void Exportar(Contacto contacto)
    {
        string ruta =
            propiedadesApp.Obtener(
                "agenda.exportacion"
            );

        var lineas = new List<string>();

        for (byte i = 0;
             i < contacto.GetCantidadContactos();
             i++)
        {
            lineas.Add(
                string.Format(
                    FORMATO_LINEA,
                    contacto.GetId(i).Value,
                    contacto.GetNombre(i).Value,
                    contacto.GetTelefono(i).Value,
                    contacto.GetCorreo(i).Value
                )
            );
        }

        try
        {
            File.WriteAllLines(
                ruta,
                lineas
            );
        }
        catch (IOException excepcion)
        {
            throw ExcepcionPersistencia
                .PorqueArchivoFallo(excepcion);
        }
    }
}