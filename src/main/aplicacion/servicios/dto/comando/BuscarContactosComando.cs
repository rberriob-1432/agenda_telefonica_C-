using System.ComponentModel.DataAnnotations;

namespace Aplicacion.Servicios.Dto.Comando;

public record BuscarContactosComando(
    [property: Required]
    string Criterio,

    [property: Required]
    TipoBusqueda TipoBusqueda
);