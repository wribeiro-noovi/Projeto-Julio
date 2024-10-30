using SuaSaude.Contracts;

namespace SuaSaude.UseCase.Atividades.Delete
{
    public class DeleteAtividadeUseCase
    {
        private readonly IAtividadeRepository _repository;
        public DeleteAtividadeUseCase(IAtividadeRepository repository) => _repository = repository;

        public bool Execute(int atvID) => _repository.Delete(atvID);
    }
}
