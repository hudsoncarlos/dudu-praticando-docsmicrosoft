using System;
using System.Threading;

namespace praticando_manual_reset_event
{
    public class Program
    {
        // mre é usado para bloquear e liberar threads manualmente. Isto é
        // criado no estado não sinalizado.
        private static ManualResetEvent mre = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("\nInicie 3 threads nomeados que bloqueiam em um ManualResetEvent:\n");

            for (int i = 0; i <= 2; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }

            Thread.Sleep(500);
            Console.WriteLine("\nQuando todos os três threads forem iniciados, pressione Enter para chamar Set()" +
                              "\npara liberar todos os tópicos.\n");
            Console.ReadLine();

            mre.Set();

            Thread.Sleep(500);
            Console.WriteLine("\nQuando um ManualResetEvent é sinalizado, os threads que chamam WaitOne()" +
                              "\nnão bloqueie. Pressione Enter para mostrar isso.\n");
            Console.ReadLine();

            for (int i = 3; i <= 4; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }

            Thread.Sleep(500);
            Console.WriteLine("\nPressione Enter para chamar Reset(), para que os threads bloqueiem novamente" +
                              "\nquando eles chamarem WaitOne().\n");
            Console.ReadLine();

            mre.Reset();

            // Start a thread that waits on the ManualResetEvent.
            Thread t5 = new Thread(ThreadProc);
            t5.Name = "Thread_5";
            t5.Start();

            Thread.Sleep(500);
            Console.WriteLine("\nPressione Enter para chamar Set() e concluir a demonstração.");
            Console.ReadLine();

            mre.Set();

            // If you run this example in Visual Studio, uncomment the following line:
            //Console.ReadLine();
        }
        private static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine(name + " inicia e chama mre.WaitOne()");

            mre.WaitOne();

            Console.WriteLine(name + " termina.");
        }
    }

    /* This example produces output similar to the following:

    Start 3 named threads that block on a ManualResetEvent:

    Thread_0 starts and calls mre.WaitOne()
    Thread_1 starts and calls mre.WaitOne()
    Thread_2 starts and calls mre.WaitOne()

    When all three threads have started, press Enter to call Set()
    to release all the threads.


    Thread_2 ends.
    Thread_0 ends.
    Thread_1 ends.

    When a ManualResetEvent is signaled, threads that call WaitOne()
    do not block. Press Enter to show this.


    Thread_3 starts and calls mre.WaitOne()
    Thread_3 ends.
    Thread_4 starts and calls mre.WaitOne()
    Thread_4 ends.

    Press Enter to call Reset(), so that threads once again block
    when they call WaitOne().


    Thread_5 starts and calls mre.WaitOne()

    Press Enter to call Set() and conclude the demo.

    Thread_5 ends.
     */
}