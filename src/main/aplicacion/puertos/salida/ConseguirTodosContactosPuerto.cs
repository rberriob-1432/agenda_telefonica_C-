using Dominio.Modelo;

namespace Aplicacion.Puertos.Salida;

public interface IConseguirTodosContactosPuerto
{
    Contacto GetAll();
}