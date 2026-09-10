using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Servicios.Dto.Comando;

public record AgregarContactoComando(
    [property: Required(ErrorMessage = "nombre no puede estar vacío")]
    [property: MinLength(3, ErrorMessage = "nombre debe tener al menos 3 caracteres")]
    string Nombre,

    [property: Required(ErrorMessage = "telefono no puede estar vacío")]
    string Telefono,

    [property: Required(ErrorMessage = "correo no puede estar vacío")]
    [property: EmailAddress(ErrorMessage = "correo debe tener un formato válido")]
    string Correo
);