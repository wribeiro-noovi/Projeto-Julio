using SuaSaude.Contracts;

namespace SuaSaude.UseCase.Autenticacao.RecuperarSenha
{
    public class RecuperarSenhaUseCase
    {
        private readonly IRecuperaSenha _repository;
        public RecuperarSenhaUseCase(IRecuperaSenha repository) => _repository = repository;

        public bool Execute(string email, string senha, string confirmaSenha) => _repository.RecuperarSenha(email, senha, confirmaSenha);
    }
}
