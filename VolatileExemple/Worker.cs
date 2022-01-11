namespace VolatileExemple
{
    public class Worker
    {
        // Este método é chamado quando o thread é iniciado.
        public void DoWork()
        {
            bool work = false;
            while (!_shouldStopVolatile)
            {
                Thread.Sleep(5000);
                work = !work; // simular algum trabalho
            }
            Console.WriteLine("3 - Thread de trabalho: terminando normalmente.");
        }
        public void RequestStop()
        {
            Console.WriteLine("2 - Propriedade _shouldStopVolatile sendo acessada por outra thread.");
            _shouldStopVolatile = true;
        }
        // A palavra-chave volátil é usada como uma dica para o compilador de que esses dados
        // membro é acessado por vários threads.
        private volatile bool _shouldStopVolatile;
    }
}
