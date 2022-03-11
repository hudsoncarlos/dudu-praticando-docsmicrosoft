using renomeador_arquivos.Domain.Enums;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace renomeador_arquivos
{
    internal class Program
    {
        public static string Command = string.Empty;
        public static string TextClipboard = string.Empty;
        public static IHttpClientFactory HttpClientFactory { get; private set; }

        static void Main(string[] args)
        {
            var manualResetEvent = new ManualResetEvent(false);
            manualResetEvent.Reset();

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            BeginCommand();

            manualResetEvent.Set();
        }

        private static void BeginCommand()
        {
            do
            {
                try
                {
                    //Log.Logger.Information("File renamer, select an item from the menu below.");
                    Log.Logger.Information("");
                    Console.Write("Lara Command> ");
                    Command = Console.ReadLine();

                    //var process = (EnumProcess)LaraConverter.ParseInt(Command, 99);

                    switch (Command)
                    {
                        case "lara file-rename":
                            ProcessRenameFiles();
                            break;
                        case "lara cmd":
                        default:
                            break;
                    }

                    Command = string.IsNullOrEmpty(Command) ? string.Empty : Command;
                    ValidadeIfThereIsCommandOnThisRoad(Command);
                }
                catch { }
            } while (Command.ToLower() != "exit");
        }

        private static void ProcessRenameFiles()
        {
            var path = RequestFileFolderPath();

            if (!ValidadeIfThereIsCommandOnThisRoad(path))
                RenameFileOneByOne(path);

            Command = string.Empty;
        }

        private static string RequestFileFolderPath()
        {
            var path = string.Empty;

            do
            {
                Log.Logger.Information("Enter the file path:");
                Console.Write("Lara command> ");
                Command = Console.ReadLine();

                path = Command;
                if (!ValidadeIfThereIsCommandOnThisRoad(Command))
                {
                    if (Directory.Exists(path))
                    {
                        Command = EnumCommands.Exit.GetDisplayName();
                        Log.Logger.Information($"The Entered path exists: {path}");
                    }
                    else
                        Log.Logger.Warning("Invalid path:");
                }

            } while (Command.ToLower() != "exit");

            return path;
        }

        private static void RenameFileOneByOne(string path)
        {
            List<string> files = Directory.GetFiles(path).ToList();

            foreach (var item in files)
            {
                var resulte = RequestNewFileName(item);
                if (!ValidadeIfThereIsCommandOnThisRoad(resulte))
                    SaveNewFileName(item, resulte);
                else
                {
                    Log.Logger.Information($"Deseja voltar ao menu inicial, s/n");
                    Console.Write("Lara Console> ");
                    Command = Console.ReadLine();

                    if (Command.ToLower() == "s")
                        break;
                }
            }
        }

        private static string RequestNewFileName(string fileName)
        {
            var newFileName = string.Empty;

            do
            {
                var filePath = fileName.Split(Path.DirectorySeparatorChar);
                var currentName = filePath[filePath.Length - 1];

                TextClipboard = currentName;
                OpenDialogInsertDataInClipboard();                

                Log.Logger.Information($"Enter the new name for the file: {currentName}");
                Console.Write("Lara command> ");
                Command = Console.ReadLine();
                newFileName = Command;

                if (Command == currentName)
                {
                    Command = EnumCommands.Exit.GetDisplayName();
                    newFileName = Command;
                }                    

                if (!ValidadeIfThereIsCommandOnThisRoad(Command))
                {
                    if (!File.Exists(fileName.Replace(currentName, newFileName)) && !string.IsNullOrEmpty(newFileName))
                    {
                        Command = EnumCommands.Exit.GetDisplayName();
                        newFileName = fileName.Replace(currentName, newFileName);
                    }
                    else
                        Log.Logger.Warning("Invalid name, the file already exists:");
                }

            } while (Command.ToLower() != "exit");

            return newFileName;
        }

        private static void SaveNewFileName(string oldFileName, string newFileName)
        {
            File.Move(oldFileName, newFileName);
            if (File.Exists(newFileName))
                Console.WriteLine("File save successFully!");
            else
                Console.WriteLine("Something went wrong, the file was not saved.");
        }

        private static bool ValidadeIfThereIsCommandOnThisRoad(string comando)
        {
            var retorno = false;

            switch (comando.ToLower())
            {
                case "exit":
                case "":
                    retorno = true;
                    Log.Logger.Warning($"There was a command in that entry: {comando}");
                    break;
                case "cls":
                    retorno = true;
                    Log.Logger.Warning($"There was a command in that entry: {comando}");
                    Console.Clear();
                    break;
                default:
                    break;
            }

            return retorno;
        }

        public static void OpenDialogInsertDataInClipboard()
        {
            Thread td = new Thread(new ThreadStart(InsereDadosNaAreaDeTransferencia));
            td.SetApartmentState(ApartmentState.STA);
            td.IsBackground = true;
            td.Start();
        }
        
        [STAThread]
        public static void InsereDadosNaAreaDeTransferencia()
            => Clipboard.SetText(TextClipboard);
    }
}
