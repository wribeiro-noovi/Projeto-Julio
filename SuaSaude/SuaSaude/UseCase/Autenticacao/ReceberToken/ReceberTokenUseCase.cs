using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Autenticacao.ReceberToken
{
    public class ReceberTokenUseCase
    {
        private readonly IEnviarEmail _repository;
        public ReceberTokenUseCase(IEnviarEmail repository) => _repository = repository;

        public TokenRecuperacao Execute(string token, string email) => _repository.validarToken(token, email);
    }
}
