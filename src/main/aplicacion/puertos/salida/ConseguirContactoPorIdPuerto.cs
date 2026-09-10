using Dominio.OV;

namespace Aplicacion.Puertos.Salida;

public interface IConseguirContactoPorIdPuerto
{
    byte? GetIndicePorId(Id id);
}