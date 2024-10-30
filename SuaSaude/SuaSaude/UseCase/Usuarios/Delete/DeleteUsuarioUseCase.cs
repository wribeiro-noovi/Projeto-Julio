using SuaSaude.Contracts;

namespace SuaSaude.UseCase.Usuarios.Delete
{
    public class DeleteUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        public DeleteUsuarioUseCase(IUsuarioRepository repository) => _repository = repository;

        public bool Execute(int userID) => _repository.Delete(userID);
    }
}
