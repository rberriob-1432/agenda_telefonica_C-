using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Servicios.Dto.Query;

public record ConseguirContactoPorIdQuery(
    [property: Required(ErrorMessage = "El id no puede ser nulo")]
    string Id
);