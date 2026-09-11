using Aplicacion.Servicios.Dto;
using Aplicacion.Servicios.Dto.Comando;

namespace Aplicacion.Puertos.Entrada;

public interface IBuscarContactosCasoUso
{
    ContactoRespuestaDto[] Execute(
        BuscarContactosComando comando
    );
}