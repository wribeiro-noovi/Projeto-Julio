using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Atividades.Create
{
    public class CreateAtividadeUseCase
    {
        private readonly IAtividadeRepository _repository;
        public CreateAtividadeUseCase(IAtividadeRepository repository) => _repository = repository;

        public Atividade Execute(string descricao, DateTime data, bool status, int userID) => _repository.Create(descricao, data, status, userID);
    }
}
