using SuaSaude.Contracts;

namespace SuaSaude.UseCase.Atividades.Update
{
    public class UpdateAtividadeUseCase
    {
        private readonly IAtividadeRepository _repository;
        public UpdateAtividadeUseCase(IAtividadeRepository repository) => _repository = repository;

        public bool Execute(int atvID, string descricao, DateTime data) => _repository.Update(atvID, descricao, data);
    }
}
