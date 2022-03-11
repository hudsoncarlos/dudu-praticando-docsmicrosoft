using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace renomeador_arquivos.Domain.Enums
{
    public enum EnumProcess
    {
        [Display(Name = "file-rename")]
        FileRename = 1,
        Default = 99
    }

    public enum EnumCommands
    {
        [Display(Name = "exit")]
        Exit = 0
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
