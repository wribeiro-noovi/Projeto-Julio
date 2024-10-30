using Mysqlx.Prepare;
using SuaSaude.Util.Cadastro;

namespace SuaSaude.UseCase.Usuarios.ValidarDados
{
    public class ValidarDadosUseCase
    {
        private readonly ValidarDadosCadastrais _repository;
        public ValidarDadosUseCase(ValidarDadosCadastrais repository) => _repository = repository;

        public Dictionary<string, string> Execute(string nome, string sexo, DateTime dataNasc, decimal altura, decimal peso, string email, string senha) =>
            _repository.ValidarDados(nome, sexo, dataNasc, altura, peso, email, senha);
    }
}
