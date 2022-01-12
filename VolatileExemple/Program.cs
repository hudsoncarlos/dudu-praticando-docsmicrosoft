namespace VolatileExemple
{
    public class WorkerThreadExample
    {
        public static void Main()
        {
            // Cria o objeto de thread de trabalho. Isso não inicia o tópico.
            var workerObject = new Worker();
            var workerThread = new Thread(workerObject.DoWork);

            // Inicia a thread de trabalho.
            workerThread.Start();
            Console.WriteLine("1 - Tópico principal: iniciando o tópico do trabalhador...");

            // Faz um loop até que o thread de trabalho seja ativado.
            while (!workerThread.IsAlive)
                ;

            // Coloca o thread principal em repouso por 500 milissegundos para
            // permite que a thread de trabalho faça algum trabalho.
            Thread.Sleep(5000);

            // Solicita que o thread de trabalho pare.
            workerObject.RequestStop();

            // Use o método Thread.Join para bloquear o thread atual
            // até que a thread do objeto termine.
            workerThread.Join();
            Console.WriteLine("4 - Encadeamento principal: o encadeamento de trabalho foi encerrado.");
            Console.ReadKey();
        }
    }
}