using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Autenticacao.Validar
{
    public class ValidarLoginUseCase
    {
        private readonly IAutenticacaoRepository _repository;
        public ValidarLoginUseCase(IAutenticacaoRepository repository) => _repository = repository;

        public Usuario Execute(string email, string senha) => _repository.Login(email, senha);
    }
}
