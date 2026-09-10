using Dominio.Modelo;
using Infraestructura.Adaptador.Persistencia.Dto;
using Infraestructura.Adaptador.Persistencia.Entidad;

namespace Infraestructura.Adaptador.Persistencia.Mapeador;

public static class ContactoPersistenciaMapeador
{
    public static ContactoPersistenceDto FromModelToDto(
        Contacto contacto,
        byte indice)
    {
        return new ContactoPersistenceDto(
            contacto.GetId(indice).Value,
            contacto.GetNombre(indice).Value,
            contacto.GetTelefono(indice).Value,
            contacto.GetCorreo(indice).Value
        );
    }

    public static ContactoEntidad FromDtoToEntity(
        ContactoPersistenceDto dto)
    {
        return new ContactoEntidad(
            dto.Id,
            dto.Nombre,
            dto.Telefono,
            dto.Correo
        );
    }

    public static ContactoPersistenceDto FromEntityToDto(
        ContactoEntidad entidad)
    {
        return new ContactoPersistenceDto(
            entidad.Id,
            entidad.Nombre,
            entidad.Telefono,
            entidad.Correo
        );
    }
}