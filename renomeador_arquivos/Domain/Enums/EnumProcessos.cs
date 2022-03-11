using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace renomeador_arquivos.Domain.Enums
{
    public enum EnumProcessos
    {
        [Display(Name = "hsc rename arquivos")]
        RenomearArquivos = 1,
        [Display(Name = "exit")]
        Default = 99
    }

    public enum EnumComandos
    {
        [Display(Name = "exit")]
        Sair = 0
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.GetCustomAttribute<DisplayAttribute>(false)
                      ?.Name
                      ?? val.ToString();
        }
    }
}
