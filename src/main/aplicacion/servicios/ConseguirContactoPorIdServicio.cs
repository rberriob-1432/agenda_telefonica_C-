using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aplicacion.Puertos.Entrada;
using Aplicacion.Puertos.Salida;
using Aplicacion.Servicios.Dto;
using Aplicacion.Servicios.Dto.Mapeador;
using Aplicacion.Servicios.Dto.Query;
using Dominio.Excepciones;
using Dominio.Modelo;
using Dominio.OV;

namespace Aplicacion.Servicios;

public sealed class ConseguirContactoPorIdServicio
    : IConseguirContactoPorIdCasoUso
{
    private readonly IConseguirContactoPorIdPuerto
        conseguirContactoPorIdPuerto;

    private readonly Contacto contacto;

    public ConseguirContactoPorIdServicio(
        IConseguirContactoPorIdPuerto conseguirContactoPorIdPuerto,
        Contacto contacto)
    {
        this.conseguirContactoPorIdPuerto =
            conseguirContactoPorIdPuerto;

        this.contacto = contacto;
    }

    public ContactoRespuestaDto Execute(
        ConseguirContactoPorIdQuery query)
    {
        ValidateQuery(query);

        Id id =
            ContactoAplicacionMapeador
                .FromGetByIdQueryToId(query);

        byte? indice =
            conseguirContactoPorIdPuerto
                .GetIndicePorId(id);

        if (!indice.HasValue)
        {
            throw ContactoNoEncontradoException
                .BecauseIdWasNotFound(id.Value);
        }

        return new ContactoRespuestaDto(
            contacto.GetId(indice.Value).Value,
            contacto.GetNombre(indice.Value).Value,
            contacto.GetTelefono(indice.Value).Value,
            contacto.GetCorreo(indice.Value).Value
        );
    }

    private static void ValidateQuery(
        ConseguirContactoPorIdQuery query)
    {
        var context = new ValidationContext(query);
        var violations = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(
            query,
            context,
            violations,
            validateAllProperties: true
        );

        if (!isValid)
        {
            throw new ValidationException(
                "La consulta contiene datos inválidos."
            );
        }
    }
}