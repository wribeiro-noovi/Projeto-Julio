using SuaSaude.Contracts;

namespace SuaSaude.UseCase.Autenticacao.VerificaEmail
{
    public class VerificaEmailUseCase
    {
        private readonly IVerificaEmailRepository _repository;
        public VerificaEmailUseCase(IVerificaEmailRepository repository) => _repository = repository;

        public bool Execute(string email) => _repository.VerificarUsuarioComEmail(email);
    }
}
