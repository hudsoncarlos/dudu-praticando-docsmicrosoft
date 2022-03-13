using AutoMapper;
using poc_automapper.Domain;
using poc_automapper.Domain.Enum;
using poc_automapper.Domain.ViewModel;
using Serilog;
using System;
using System.Threading;

namespace poc_automapper
{
    internal class Program
    {
        public static IMapper MapperConfig;
        static void Main(string[] args)
        {
            var manualResetEvent = new ManualResetEvent(false);
            manualResetEvent.Reset();

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            var configuration = new MapperConfiguration(x => { x.CreateMap<Pessoa, PessoaViewModel>(); });
            MapperConfig = configuration.CreateMapper();

            var pessoaViewModel = RetornaPessoaViewModelMapper();

            Log.Information($"{nameof(pessoaViewModel.Id)} - {pessoaViewModel.Id}");
            Log.Information($"{nameof(pessoaViewModel.Nome)} - {pessoaViewModel.Nome}");
            Log.Information($"{nameof(pessoaViewModel.SobreNome)} - {pessoaViewModel.SobreNome}");
            Log.Information($"{nameof(pessoaViewModel.Idade)} - {pessoaViewModel.Idade}");
            Log.Information($"{nameof(pessoaViewModel._Sexo)} - {pessoaViewModel._Sexo}");
            Log.Information($"{nameof(pessoaViewModel._DataNascimento)} - {pessoaViewModel._DataNascimento}");

            manualResetEvent.Set();
        }

        private static PessoaViewModel RetornaPessoaViewModelMapper()
        {
            var pessoa = new Pessoa(new DateTime(1988, 07, 27), EnumSexo.Masculino) 
            {
                Id = 1, Nome = "Hudson Pessoa", SobreNome = "Carlos", Renda = 6.700 
            };

            var pessoaViewModel = new PessoaViewModel(new DateTime(1988, 07, 27), EnumSexo.Masculino)
            {
                Id = 2,
                Nome = "Hudson PessoaViewModel",
                SobreNome = "Carlos"
            };

            return MapperConfig.Map(pessoa, pessoaViewModel);
        }
    }
}
