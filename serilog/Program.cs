using Serilog;
using System;

namespace praticando_serilog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Usando Serilog...");
            Log.Debug("Usando Serilog...");
            Log.Error("Usando Serilog...");
            Log.Warning("Usando Serilog...");

            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();
            Log.Information("Usando mais um Serilog");

            int a = 10, b = 0;
            try
            {
                Log.Debug("Dividindo {A} por {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Alguma coisa deu errado...");
            }

            Log.CloseAndFlush();

            Console.ReadKey();
        }
    }
}
