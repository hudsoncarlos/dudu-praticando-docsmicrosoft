using renomeador_arquivos.Domain;
using renomeador_arquivos.Domain.Enums;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace renomeador_arquivos
{
    internal class Program
    {
        public static string comando = string.Empty;
        public static string textoAreaTransferencia = string.Empty;

        static void Main(string[] args)
        {
            var manualResetEvent = new ManualResetEvent(false);
            manualResetEvent.Reset();

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            IniciarCmd();

            manualResetEvent.Set();
        }

        private static void IniciarCmd()
        {
            do
            {
                try
                {
                    Log.Logger.Information("Renomeador de arquivos, selecione um item do menu abaixo.");
                    Log.Logger.Information("");
                    Log.Logger.Information("    1 - Renomear arquivos");
                    Log.Logger.Information("");
                    Console.Write("Lara Console> ");
                    comando = Console.ReadLine();

                    var processo = (EnumProcessos)Conversor.ParseInt(comando, 99);

                    switch (processo)
                    {
                        case EnumProcessos.RenomearArquivos:
                            ProcessarRenomearArquivos();
                            break;
                        default:
                            break;
                    }

                    comando = string.IsNullOrEmpty(comando) ? string.Empty : comando;
                    ValidarSeHouveComandoNessaEntrada(comando);
                }
                catch { }
            } while (comando.ToLower() != "exit");
        }

        private static void ProcessarRenomearArquivos()
        {
            var caminho = SolicitarCaminhoPastaArquivos();

            if (!ValidarSeHouveComandoNessaEntrada(caminho))
                RenomearArquivosUmPorUm(caminho);

            comando = string.Empty;
        }

        private static string SolicitarCaminhoPastaArquivos()
        {
            var caminho = string.Empty;

            do
            {
                Log.Logger.Information("Digite o caminho do arquivo:");
                Console.Write("Lara Console> ");
                comando = Console.ReadLine();

                caminho = comando;
                if (!ValidarSeHouveComandoNessaEntrada(comando))
                {
                    if (Directory.Exists(caminho))
                    {
                        comando = EnumComandos.Sair.GetDisplayName();
                        Log.Logger.Information($"Caminho inserido existe: {caminho}");
                    }
                    else
                        Log.Logger.Warning("Caminho inválido:");
                }

            } while (comando.ToLower() != "exit");

            return caminho;
        }

        private static void RenomearArquivosUmPorUm(string caminho)
        {
            List<string> arquivos = Directory.GetFiles(caminho).ToList();

            foreach (var item in arquivos)
            {
                var resulte = SolicitarNovoNomeDoArquivo(item);
                if (!ValidarSeHouveComandoNessaEntrada(resulte))
                    SalvarNovoNomeDoArquivo(item, resulte);
                else
                {
                    Log.Logger.Information($"Quer voltar ao menu inicial, s/n");
                    Console.Write("Lara Console> ");
                    comando = Console.ReadLine();

                    if (comando.ToLower() == "s")
                        break;
                }
            }
        }

        private static string SolicitarNovoNomeDoArquivo(string nomeArquivo)
        {
            var novoNomeArquivo = string.Empty;

            do
            {
                var caminhoArquivo = nomeArquivo.Split(Path.DirectorySeparatorChar);
                var nomeAtual = caminhoArquivo[caminhoArquivo.Length - 1];

                textoAreaTransferencia = nomeAtual;
                AbrirDialogoInsereDadosNaAreaDeTransferencia();

                Log.Logger.Information($"Digite o novo nome para o arquivo: {nomeAtual}");
                Console.Write("Lara Console> ");
                comando = Console.ReadLine();
                novoNomeArquivo = comando;

                if (comando == nomeAtual)
                {
                    comando = EnumComandos.Sair.GetDisplayName();
                    novoNomeArquivo = comando;
                }                    

                if (!ValidarSeHouveComandoNessaEntrada(comando))
                {
                    if (!File.Exists(nomeArquivo.Replace(nomeAtual, novoNomeArquivo)) && !string.IsNullOrEmpty(novoNomeArquivo))
                    {
                        comando = EnumComandos.Sair.GetDisplayName();
                        novoNomeArquivo = nomeArquivo.Replace(nomeAtual, novoNomeArquivo);
                    }
                    else
                        Log.Logger.Warning("Nome inválido, o arquivo já existe:");
                }

            } while (comando.ToLower() != "exit");

            return novoNomeArquivo;
        }

        private static void SalvarNovoNomeDoArquivo(string nomeArquivoAntigo, string novoNomeArquivo)
        {
            File.Move(nomeArquivoAntigo, novoNomeArquivo);
            if (File.Exists(novoNomeArquivo))
                Console.WriteLine("Arquivo Salvo com sucesso!");
            else
                Console.WriteLine("Algo deu errado, o arquivo não foi salvo.");
        }

        private static bool ValidarSeHouveComandoNessaEntrada(string comando)
        {
            var retorno = false;

            switch (comando.ToLower())
            {
                case "exit":
                case "":
                    retorno = true;
                    Log.Logger.Warning($"Houve um comando nessa entrada: {comando}");
                    break;
                case "cls":
                    retorno = true;
                    Log.Logger.Warning($"Houve um comando nessa entrada: {comando}");
                    Console.Clear();
                    break;
                default:
                    break;
            }

            return retorno;
        }

        public static void AbrirDialogoInsereDadosNaAreaDeTransferencia()
        {
            Thread td = new Thread(new ThreadStart(InsereDadosNaAreaDeTransferencia));
            td.SetApartmentState(ApartmentState.STA);
            td.IsBackground = true;
            td.Start();
        }
        
        [STAThread]
        public static void InsereDadosNaAreaDeTransferencia()
            => Clipboard.SetText(textoAreaTransferencia);
    }
}
