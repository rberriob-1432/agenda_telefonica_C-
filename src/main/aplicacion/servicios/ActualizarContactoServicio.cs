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

public sealed class ActualizarContactoServicio
    : IActualizarContactoCasoUso
{
    private readonly IActualizarContactoPuerto actualizarContactoPuerto;
    private readonly IConseguirContactoPorIdPuerto conseguirContactoPorIdPuerto;
    private readonly IConseguirContactoPorCorreoPuerto conseguirContactoPorCorreoPuerto;
    private readonly Contacto contacto;

    public ActualizarContactoServicio(
        IActualizarContactoPuerto actualizarContactoPuerto,
        IConseguirContactoPorIdPuerto conseguirContactoPorIdPuerto,
        IConseguirContactoPorCorreoPuerto conseguirContactoPorCorreoPuerto,
        Contacto contacto)
    {
        this.actualizarContactoPuerto = actualizarContactoPuerto;
        this.conseguirContactoPorIdPuerto = conseguirContactoPorIdPuerto;
        this.conseguirContactoPorCorreoPuerto = conseguirContactoPorCorreoPuerto;
        this.contacto = contacto;
    }

    public Contacto Execute(
        ActualizarContactoComando comando)
    {
        ValidateCommand(comando);

        Id id =
            ContactoAplicacionMapeador
                .FromUpdateCommandToId(comando);

        byte indice = ConseguirIndicePorId(id);

        Correo correo =
            ContactoAplicacionMapeador
                .FromUpdateCommandToCorreo(comando);

        EnsureCorreoNoRegistrado(correo, indice);

        Nombre nombre =
            ContactoAplicacionMapeador
                .FromUpdateCommandToNombre(comando);

        Telefono telefono =
            ContactoAplicacionMapeador
                .FromUpdateCommandToTelefono(comando);

        contacto.SetNombre(indice, nombre);
        contacto.SetTelefono(indice, telefono);
        contacto.SetCorreo(indice, correo);

        return actualizarContactoPuerto.Update(contacto);
    }

    private static void ValidateCommand(
        ActualizarContactoComando comando)
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

    private byte ConseguirIndicePorId(Id id)
    {
        byte? indice =
            conseguirContactoPorIdPuerto
                .GetIndicePorId(id);

        if (!indice.HasValue)
        {
            throw ContactoNoEncontradoException
                .BecauseIdWasNotFound(id.Value);
        }

        return indice.Value;
    }

    private void EnsureCorreoNoRegistrado(
        Correo correo,
        byte indice)
    {
        if (!conseguirContactoPorCorreoPuerto
                .ExistePorCorreo(correo))
        {
            return;
        }

        if (!contacto.GetCorreo(indice).Equals(correo))
        {
            throw CorreoYaRegistradoException
                .BecauseEmailWasAlreadyRegistered(correo.Value);
        }
    }
}