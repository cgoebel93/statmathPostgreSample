using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using statmathPostgreSample.Controller;
using statmathPostgreSample.Database;
using System;
using System.Linq;

namespace statmathPostgreSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MachineController controller = new MachineController();
                controller.ShowOptions();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{Properties.Resource1.UnhandledException} {e}");
            }
        }
    }
}
