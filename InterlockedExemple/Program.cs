namespace InterlockedExemple
{
    class InterlockedClass
    {
        //0 para falso, 1 para verdadeiro.
        private static int _usandoRecurso = 0;
        private const int _numeroInteracoesThread = 5;
        private const int _numeroThreads = 10;

        static void Main()
        {
            Thread minhaThread;
            var randomico = new Random();

            for (int i = 0; i < _numeroThreads; i++) 
            {
                minhaThread = new Thread(new ThreadStart(ProcessandoMinhaThread))
                {
                    Name = $"Thread{i + 1}"
                };

                // Espera um tempo aleatório antes de iniciar próxima thread.
                Thread.Sleep(randomico.Next(0, 1000));
                minhaThread.Start();
            }

            Console.ReadKey();
        }

        static void ProcessandoMinhaThread() 
        {
            for (int i = 0; i < _numeroInteracoesThread; i++)
            {
                UsoRecurso();

                // Espera 1 segundo antes da procima tentativa.
                Thread.Sleep(5000);
            }
        }

        // Um simples método que nega reentrada.
        static bool UsoRecurso()
        {
            // 0 Indica que o método não é utilizado.
            if (0 == Interlocked.Exchange(ref _usandoRecurso, 1))
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} - Bloqueado.");

                // Código para acessar recurso que não é Thread safe iria aqui.

                // Simula algum trabalho
                Thread.Sleep(500);

                Console.WriteLine($"{Thread.CurrentThread.Name} - Ainda existe um bloqueio.");

                // Solta o bloqueio.
                Interlocked.Exchange(ref _usandoRecurso, 0);
                Console.WriteLine($"{Thread.CurrentThread.Name} - Desbloqueado.");
                return true;
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.Name} foi negado o bloqueio.");
                return false;
            }
        }
    }
}