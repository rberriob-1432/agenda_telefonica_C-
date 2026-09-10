namespace Infraestructura.Adaptador.Persistencia.Dto;

public record ContactoPersistenceDto(
    string Id,
    string Nombre,
    string Telefono,
    string Correo
);