using Dominio.OV;

namespace Aplicacion.Puertos.Salida;

public interface ConseguirContactoPorIdPuerto
{
    byte? GetIndicePorId(Id id);
}