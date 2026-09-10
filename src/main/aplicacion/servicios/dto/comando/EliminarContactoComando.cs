using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Servicios.Dto.Comando;

public record EliminarContactoComando(
    [property: Required(ErrorMessage = "id no puede ser nulo")]
    string Id
);