using Aplicacion.Servicios.Dto.Comando;
using Dominio.Modelo;

namespace Aplicacion.Puertos.Entrada;

public interface IActualizarContactoCasoUso
{
    Contacto Execute(ActualizarContactoComando comando);
}