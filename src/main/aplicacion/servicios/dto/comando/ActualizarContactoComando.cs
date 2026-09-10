using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Servicios.Dto.Comando;

public record ActualizarContactoComando(
    [property: Required(ErrorMessage = "id no debe ser vacio")]
    string Id,

    [property: Required(ErrorMessage = "nombre no debe ser vacio")]
    [property: MinLength(3, ErrorMessage = "nombre debe tener al menos 3 caracteres")]
    string Nombre,

    [property: Required(ErrorMessage = "correo no debe ser vacio")]
    [property: EmailAddress(ErrorMessage = "correo debe tener un formato valido")]
    string Correo,

    [property: Required(ErrorMessage = "telefono no puede estar vacío")]
    string Telefono
);