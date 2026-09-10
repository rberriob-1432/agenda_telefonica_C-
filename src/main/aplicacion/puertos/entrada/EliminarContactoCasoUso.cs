using Aplicacion.Servicios.Dto.Comando;

namespace Aplicacion.Puertos.Entrada;

public interface IEliminarContactoCasoUso
{
    void Execute(EliminarContactoComando comando);
}