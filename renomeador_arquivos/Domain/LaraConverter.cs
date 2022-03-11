using System;

namespace renomeador_arquivos.Domain
{
    public static class LaraConverter
    {
        public static int ParseInt(this string str, int def = 0)
        {
            if (int.TryParse(str, out int resultado))
                return resultado;
            else
                return def;
        }

        public static double ParseDouble(this string str, double def = 0)
        {
            if (double.TryParse(str, out double resultado))
                return resultado;
            else
                return def;
        }

        public static bool ParseBool(this string str, bool def = false)
        {
            if (bool.TryParse(str, out bool resultado))
                return resultado;
            else
                return def;
        }

        public static DateTime ParseDate(this string str)
            => ParseDate(str, DateTime.Now);

        public static DateTime ParseDate(this string str, DateTime def)
        {
            if (DateTime.TryParse(str, out DateTime resultado))
                return resultado;
            else
                return def;
        }

        //public static T ParseEnum<T>(this string? str, T def)
        //{
        //    if (Enum.TryParse<T>(str, out T resultado))
        //        return resultado;
        //    else
        //        return def;
        //}
    }
}
