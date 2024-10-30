using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Usuarios.Create
{
    public class CreateUsuarioUseCase
    {
        private readonly IUsuarioRepository _repository;
        public CreateUsuarioUseCase(IUsuarioRepository repository) => _repository = repository;

        public Usuario Execute(string nome, string sexo, DateTime dataNasc, decimal altura, decimal peso, string email, string senha) => _repository.Create(nome, sexo, dataNasc, altura, peso, email, senha);
    }
}
