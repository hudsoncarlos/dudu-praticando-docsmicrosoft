using poc_automapper.Domain.Enum;
using System;

namespace poc_automapper.Domain
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public int Idade { get; set; }
        private DateTime _DataNascimento { get; set; }
        private EnumSexo _Sexo { get; set; }
        public double Renda { get; set; }

        public Pessoa(DateTime dataNascimento, EnumSexo enumSexo)
        {                        
            this._DataNascimento = dataNascimento;
            this._Sexo = enumSexo;
            this.Idade = CalcularIdadePelaDataDeNascimento(dataNascimento);
        }

        private int CalcularIdadePelaDataDeNascimento(DateTime dataNascimento)
        {
            var idade = DateTime.Now.Year - _DataNascimento.Year;
            if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                idade -= 1;

            return idade;
        }
    }
}
