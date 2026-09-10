using Dominio.OV;

namespace Aplicacion.Puertos.Salida;

public interface IConseguirContactoPorCorreoPuerto
{
    bool ExistePorCorreo(Correo correo);
}