using Dominio.Modelo;

namespace Aplicacion.Puertos.Salida;

public interface IActualizarContactoPuerto
{
    Contacto Update(Contacto contacto);
}