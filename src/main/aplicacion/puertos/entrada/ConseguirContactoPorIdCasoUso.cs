using Aplicacion.Servicios.Dto;
using Aplicacion.Servicios.Dto.Query;

namespace Aplicacion.Puertos.Entrada;

public interface IConseguirContactoPorIdCasoUso
{
    ContactoRespuestaDto Execute(
        ConseguirContactoPorIdQuery query
    );
}