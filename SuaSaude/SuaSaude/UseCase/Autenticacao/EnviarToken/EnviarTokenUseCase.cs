using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Autenticacao.EnviarToken
{
    public class EnviarTokenUseCase
    {
        private readonly IEnviarEmail _repository;
        public EnviarTokenUseCase(IEnviarEmail repository) => _repository = repository;

        public TokenRecuperacao Execute(string emailDestino) => _repository.EnviarToken(emailDestino);
    }
}
