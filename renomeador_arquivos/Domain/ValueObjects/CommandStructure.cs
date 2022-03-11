namespace renomeador_arquivos.Domain.ValueObjects
{
    public class CommandStructure
    {
        public string Source { get; set; }
        public string Option { get; set; }
        public string Command { get; set; }
        public string CommandOptions { get; set; }
        public string Arguments { get; set; }
    }
}
