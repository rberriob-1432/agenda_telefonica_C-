using System.Collections.Generic;
using System.IO;
using Aplicacion.Puertos.Salida;
using Dominio.Modelo;
using Dominio.OV;
using Infraestructura.Adaptador.Persistencia.Dto;
using Infraestructura.Adaptador.Persistencia.Entidad;
using Infraestructura.Adaptador.Persistencia.Excepcion;
using Infraestructura.Adaptador.Persistencia.Mapeador;
using Infraestructura.Configuraciones;

namespace Infraestructura.Adaptador.Persistencia.Archivo;

public sealed class ContactoRepositorioTxt :
    IGuardarContactoPuerto,
    IActualizarContactoPuerto,
    IConseguirContactoPorIdPuerto,
    IConseguirContactoPorCorreoPuerto,
    IConseguirTodosContactosPuerto,
    IEliminarContactoPuerto
{
    private const string FormatoLinea =
        "{0}|{1}|{2}|{3}";

    private readonly PropiedadesApp _propiedadesApp;
    private readonly Contacto _contacto;

    public ContactoRepositorioTxt(
        PropiedadesApp propiedadesApp,
        Contacto contacto)
    {
        _propiedadesApp = propiedadesApp;
        _contacto = contacto;
    }

    private string ObtenerRutaArchivo()
    {
        return _propiedadesApp.Obtener("agenda.archivo");
    }

    public Contacto Save(Contacto contacto)
    {
        try
        {
            EscribirArchivo(contacto);
            return contacto;
        }
        catch (IOException excepcion)
        {
            throw ExcepcionPersistencia.PorqueGuardarFallo(
                ObtenerUltimoId(contacto),
                excepcion
            );
        }
    }

    public Contacto Update(Contacto contacto)
    {
        try
        {
            EscribirArchivo(contacto);
            return contacto;
        }
        catch (IOException excepcion)
        {
            throw ExcepcionPersistencia.PorqueActualizarFallo(
                ObtenerUltimoId(contacto),
                excepcion
            );
        }
    }

    public byte? GetIndicePorId(Id id)
    {
        try
        {
            return BuscarIndicePorId(id);
        }
        catch (IOException excepcion)
        {
            throw ExcepcionPersistencia.PorqueBuscarPorIdFallo(
                id.Value,
                excepcion
            );
        }
    }

    public bool ExistePorCorreo(Correo correo)
    {
        try
        {
            return BuscarPorCorreo(correo);
        }
        catch (IOException excepcion)
        {
            throw ExcepcionPersistencia.PorqueBuscarPorCorreoFallo(
                correo.Value,
                excepcion
            );
        }
    }

    public Contacto GetAll()
    {
        try
        {
            CargarArchivo();
            return _contacto;
        }
        catch (IOException excepcion)
        {
            throw ExcepcionPersistencia.PorqueBuscarTodosFallo(
                excepcion
            );
        }
    }

    public void Delete(Id id)
    {
        try
        {
            byte? indice = BuscarIndicePorId(id);

            if (indice is null)
            {
                return;
            }

            EliminarIndice(indice.Value);
            EscribirArchivo(_contacto);
        }
        catch (IOException excepcion)
        {
            throw ExcepcionPersistencia.PorqueEliminarFallo(
                id.Value,
                excepcion
            );
        }
    }

    private void EscribirArchivo(Contacto contacto)
    {
        var lineas = new List<string>();

        for (
            byte i = 0;
            i < contacto.GetCantidadContactos();
            i++)
        {
            string linea = string.Format(
                FormatoLinea,
                contacto.GetId(i).Value,
                contacto.GetNombre(i).Value,
                contacto.GetTelefono(i).Value,
                contacto.GetCorreo(i).Value
            );

            lineas.Add(linea);
        }

        File.WriteAllLines(
            ObtenerRutaArchivo(),
            lineas
        );
    }

    private void CargarArchivo()
    {
        string ruta = ObtenerRutaArchivo();

        if (!File.Exists(ruta))
        {
            return;
        }

        string[] lineas =
            File.ReadAllLines(ruta);

        _contacto.SetCantidadContactos(0);

        foreach (string linea in lineas)
        {
            if (string.IsNullOrWhiteSpace(linea))
            {
                continue;
            }

            string[] datos =
                linea.Split('|');

            ContactoPersistenceDto dto =
                new ContactoPersistenceDto(
                    datos[0],
                    datos[1],
                    datos[2],
                    datos[3]
                );

            ContactoEntidad entidad =
                ContactoPersistenciaMapeador
                    .FromDtoToEntity(dto);

            byte indice =
                _contacto.GetCantidadContactos();

            _contacto.SetId(
                indice,
                new Id(entidad.Id)
            );

            _contacto.SetNombre(
                indice,
                new Nombre(entidad.Nombre)
            );

            _contacto.SetTelefono(
                indice,
                new Telefono(entidad.Telefono)
            );

            _contacto.SetCorreo(
                indice,
                new Correo(entidad.Correo)
            );

            _contacto.SetCantidadContactos(
                (byte)(indice + 1)
            );
        }
    }

    private byte? BuscarIndicePorId(Id id)
    {
        CargarArchivo();

        for (
            byte i = 0;
            i < _contacto.GetCantidadContactos();
            i++)
        {
            if (_contacto.GetId(i).Equals(id))
            {
                return i;
            }
        }

        return null;
    }

    private bool BuscarPorCorreo(Correo correo)
    {
        CargarArchivo();

        for (
            byte i = 0;
            i < _contacto.GetCantidadContactos();
            i++)
        {
            if (_contacto.GetCorreo(i).Equals(correo))
            {
                return true;
            }
        }

        return false;
    }

    private void EliminarIndice(byte indice)
    {
        byte cantidad =
            _contacto.GetCantidadContactos();

        for (
            byte i = indice;
            i < cantidad - 1;
            i++)
        {
            _contacto.SetId(
                i,
                _contacto.GetId((byte)(i + 1))
            );

            _contacto.SetNombre(
                i,
                _contacto.GetNombre((byte)(i + 1))
            );

            _contacto.SetTelefono(
                i,
                _contacto.GetTelefono((byte)(i + 1))
            );

            _contacto.SetCorreo(
                i,
                _contacto.GetCorreo((byte)(i + 1))
            );
        }

        byte ultimoIndice =
            (byte)(cantidad - 1);

        _contacto.SetId(
            ultimoIndice,
            null
        );

        _contacto.SetNombre(
            ultimoIndice,
            null
        );

        _contacto.SetTelefono(
            ultimoIndice,
            null
        );

        _contacto.SetCorreo(
            ultimoIndice,
            null
        );

        _contacto.SetCantidadContactos(
            (byte)(cantidad - 1)
        );
    }

    private string ObtenerUltimoId(
        Contacto contacto)
    {
        if (contacto.GetCantidadContactos() == 0)
        {
            return "desconocido";
        }

        return contacto
            .GetId(
                (byte)(
                    contacto.GetCantidadContactos() - 1
                )
            )
            .Value;
    }
}