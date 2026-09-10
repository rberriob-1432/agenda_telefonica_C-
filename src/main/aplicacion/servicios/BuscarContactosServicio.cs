using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aplicacion.Puertos.Entrada;
using Aplicacion.Puertos.Salida;
using Aplicacion.Servicios.Dto;
using Aplicacion.Servicios.Dto.Comando;
using Dominio.Modelo;

namespace Aplicacion.Servicios;

public sealed class BuscarContactosServicio
    : IBuscarContactosCasoUso
{
    private readonly IConseguirTodosContactosPuerto
        conseguirTodosContactosPuerto;

    public BuscarContactosServicio(
        IConseguirTodosContactosPuerto conseguirTodosContactosPuerto)
    {
        this.conseguirTodosContactosPuerto =
            conseguirTodosContactosPuerto;
    }

    public List<ContactoRespuestaDto> Execute(
        BuscarContactosComando comando)
    {
        ValidateCommand(comando);

        Contacto contacto =
            conseguirTodosContactosPuerto.GetAll();

        var resultados = new List<ContactoRespuestaDto>();

        string criterio =
            comando.Criterio
                .Trim()
                .ToLower();

        for (byte i = 0;
             i < contacto.GetCantidadContactos();
             i++)
        {
            bool coincide = comando.TipoBusqueda switch
            {
                TipoBusqueda.NOMBRE =>
                    contacto.GetNombre(i)
                        .Value
                        .ToLower()
                        .StartsWith(criterio),

                TipoBusqueda.CORREO =>
                    contacto.GetCorreo(i)
                        .Value
                        .ToLower()
                        .Contains(criterio),

                _ => false
            };

            if (coincide)
            {
                resultados.Add(
                    new ContactoRespuestaDto(
                        contacto.GetId(i).Value,
                        contacto.GetNombre(i).Value,
                        contacto.GetTelefono(i).Value,
                        contacto.GetCorreo(i).Value
                    )
                );
            }
        }

        return resultados;
    }

    private static void ValidateCommand(
        BuscarContactosComando comando)
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
}