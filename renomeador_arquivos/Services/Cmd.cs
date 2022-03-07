using System;
using System.Diagnostics;

namespace renomeador_arquivos.Services
{
    public static class Cmd
    {
        public static string ExecutarCMD(string comando)
        {
            using (Process processo = new Process())
            {
                processo.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");

                // Formata a string para passar como argumento para o cmd.exe
                processo.StartInfo.Arguments = string.Format("/c {0}", comando);

                processo.StartInfo.RedirectStandardOutput = true;
                processo.StartInfo.UseShellExecute = false;
                processo.StartInfo.CreateNoWindow = true;

                processo.Start();
                //processo.WaitForExit();

                return processo.StandardOutput.ReadToEnd();
            }
        }
    }
}
