using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Usuarios.Get
{
    public class GetUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        public GetUsuarioUseCase(IUsuarioRepository repository) => _repository = repository;

        public Usuario? Execute(int userID) => _repository.Get(userID);
    }
}
