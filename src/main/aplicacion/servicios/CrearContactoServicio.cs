using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aplicacion.Puertos.Entrada;
using Aplicacion.Puertos.Salida;
using Aplicacion.Servicios.Dto.Comando;
using Aplicacion.Servicios.Dto.Mapeador;
using Dominio.Excepciones;
using Dominio.Modelo;
using Dominio.OV;

namespace Aplicacion.Servicios;

public sealed class CrearContactoServicio
    : ICrearContactoCasoUso
{
    private readonly IGuardarContactoPuerto guardarContactoPuerto;
    private readonly IConseguirContactoPorCorreoPuerto
        conseguirContactoPorCorreoPuerto;
    private readonly Contacto contacto;

    public CrearContactoServicio(
        IGuardarContactoPuerto guardarContactoPuerto,
        IConseguirContactoPorCorreoPuerto conseguirContactoPorCorreoPuerto,
        Contacto contacto)
    {
        this.guardarContactoPuerto = guardarContactoPuerto;
        this.conseguirContactoPorCorreoPuerto =
            conseguirContactoPorCorreoPuerto;
        this.contacto = contacto;
    }

    public Contacto Execute(
        AgregarContactoComando comando)
    {
        ValidateCommand(comando);

        Correo correo =
            ContactoAplicacionMapeador
                .FromCreateCommandToCorreo(comando);

        EnsureCorreoNoRegistrado(correo);

        Nombre nombre =
            ContactoAplicacionMapeador
                .FromCreateCommandToNombre(comando);

        Telefono telefono =
            ContactoAplicacionMapeador
                .FromCreateCommandToTelefono(comando);

        Id id = GenerarId();

        byte indice = contacto.GetCantidadContactos();

        contacto.SetNombre(indice, nombre);
        contacto.SetTelefono(indice, telefono);
        contacto.SetCorreo(indice, correo);
        contacto.SetId(indice, id);

        contacto.SetCantidadContactos(
            (byte)(indice + 1)
        );

        return guardarContactoPuerto.Save(contacto);
    }

    private static void ValidateCommand(
        AgregarContactoComando comando)
    {
        var context = new ValidationContext(comando);
        var violations = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            comando,
            context,
            violations,
            validateAllProperties: true
        );

        if (!isValid)
        {
            throw new ValidationException(
                "El comando contiene datos inválidos."
            );
        }
    }

    private void EnsureCorreoNoRegistrado(
        Correo correo)
    {
        if (conseguirContactoPorCorreoPuerto
                .ExistePorCorreo(correo))
        {
            throw CorreoYaRegistradoException
                .BecauseEmailWasAlreadyRegistered(
                    correo.Value
                );
        }
    }

    private Id GenerarId()
    {
        int mayorId = 0;

        for (byte i = 0;
             i < contacto.GetCantidadContactos();
             i++)
        {
            string valorId =
                contacto.GetId(i).Value;

            try
            {
                int idActual =
                    int.Parse(valorId);

                if (idActual > mayorId)
                {
                    mayorId = idActual;
                }
            }
            catch (FormatException)
            {
                // El ID no numérico se ignora
                // para generar el siguiente ID.
            }
        }

        return new Id(
            (mayorId + 1).ToString()
        );
    }
}