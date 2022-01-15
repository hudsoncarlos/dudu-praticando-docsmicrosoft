using System;

namespace EventHandlerExemple
{
    public class Program
    {
        static void Main()
        {
            var limite = new Random().Next(10);
            Console.WriteLine($"Numero limite de adições = {limite}");

            var contador = new Counter(limite);
            contador.LimitAlcance += ContadorLimitAlcance;

            Console.WriteLine("Digite a tecla 'a' incrementar o total.");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("Adicionando um.");
                contador.Add(1);
            }
        }

        static void ContadorLimitAlcance(object? sender, EventArgs e)
        {
            Console.WriteLine("O limite foi alcançado.");
            Environment.Exit(0);
        }

        class Counter        
        {
            private int _limite;
            private int _total;

            public Counter(int passarLimite)
               => _limite = passarLimite;

            public void Add(int passarTotal)
            {
                _total += passarTotal;
                if (_total >= _limite)
                    LimitAlcance?.Invoke(this, EventArgs.Empty);
            }

            public event EventHandler LimitAlcance;
        }
    }
}