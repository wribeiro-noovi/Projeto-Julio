using SuaSaude.Contracts;

namespace SuaSaude.UseCase.Atividades.AlterCheck
{
    public class AlterCheckUseCase
    {
        private readonly IAtividadeRepository _repository;
        public AlterCheckUseCase(IAtividadeRepository repository) => _repository = repository;

        public bool Execute(int atvId) => _repository.AtividadeCheck(atvId);
    }
}
