using SuaSaude.Contracts;
using SuaSaude.Enum;

namespace SuaSaude.UseCase.Usuarios.AlterSenha
{
    public class AlterarSenhaUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        public AlterarSenhaUsuarioUseCase(IUsuarioRepository repository) => _repository = repository;

        public AlteracaoSenhaStatus Execute(int userID, string sAtual, string senha, string confirmaSenha) => _repository.AlterSenha(userID, sAtual, senha, confirmaSenha);
    }
}
