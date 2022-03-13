using poc_automapper.Domain.Enum;
using System;

namespace poc_automapper.Domain.ViewModel
{
    public class PessoaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string NomeSobrenome { get; set; }
        public int Idade { get; set; }
        public DateTime _DataNascimento { get; private set; }
        public EnumSexo _Sexo { get; private set; }

        public PessoaViewModel(DateTime dataNascimento, EnumSexo enumSexo)
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
