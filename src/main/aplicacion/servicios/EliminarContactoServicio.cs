using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aplicacion.Puertos.Entrada;
using Aplicacion.Puertos.Salida;
using Aplicacion.Servicios.Dto.Comando;
using Aplicacion.Servicios.Dto.Mapeador;
using Dominio.Excepciones;
using Dominio.OV;

namespace Aplicacion.Servicios;

public sealed class EliminarContactoServicio
    : IEliminarContactoCasoUso
{
    private readonly IEliminarContactoPuerto
        eliminarContactoPuerto;

    private readonly IConseguirContactoPorIdPuerto
        conseguirContactoPorIdPuerto;

    public EliminarContactoServicio(
        IEliminarContactoPuerto eliminarContactoPuerto,
        IConseguirContactoPorIdPuerto conseguirContactoPorIdPuerto)
    {
        this.eliminarContactoPuerto = eliminarContactoPuerto;
        this.conseguirContactoPorIdPuerto =
            conseguirContactoPorIdPuerto;
    }

    public void Execute(
        EliminarContactoComando comando)
    {
        ValidateCommand(comando);

        Id id =
            ContactoAplicacionMapeador
                .FromDeleteCommandToId(comando);

        EnsureContactoExists(id);

        eliminarContactoPuerto.Delete(id);
    }

    private static void ValidateCommand(
        EliminarContactoComando comando)
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

    private void EnsureContactoExists(
        Id id)
    {
        byte? indice =
            conseguirContactoPorIdPuerto
                .GetIndicePorId(id);

        if (!indice.HasValue)
        {
            throw ContactoNoEncontradoException
                .BecauseIdWasNotFound(id.Value);
        }
    }
}