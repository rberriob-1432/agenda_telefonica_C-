using Dominio.Modelo;

namespace Aplicacion.Puertos.Salida;

public interface IExportarContactosPuerto
{
    void Exportar(Contacto contacto);
}