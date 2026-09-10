using Dominio.Modelo;

namespace Aplicacion.Puertos.Salida;

public interface IGuardarContactoPuerto
{
    Contacto Save(Contacto contacto);
}