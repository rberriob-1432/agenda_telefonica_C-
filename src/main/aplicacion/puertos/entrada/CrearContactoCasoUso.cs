using Aplicacion.Servicios.Dto.Comando;
using Dominio.Modelo;

namespace Aplicacion.Puertos.Entrada;

public interface ICrearContactoCasoUso
{
    Contacto Execute(AgregarContactoComando comando);
}