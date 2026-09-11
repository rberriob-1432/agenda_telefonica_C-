
using Infraestructura.Configuraciones;

namespace Main;

public static class Program
{
    public static void Main(string[] args)
    {
        ContainerDependencia container =
            new ContainerDependencia();

        container.AgendaTelefonicaCli().Start();
    }
}

