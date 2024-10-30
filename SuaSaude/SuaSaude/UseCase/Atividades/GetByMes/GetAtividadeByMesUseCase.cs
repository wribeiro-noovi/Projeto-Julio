using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Atividades.GetByMes
{
    public class GetAtividadeByMesUseCase
    {
        private readonly IAtividadeRepository _repository;
        public GetAtividadeByMesUseCase(IAtividadeRepository repository) => _repository = repository;

        public List<Atividade> Execute(DateTime data, int userID) => _repository.GetAtividadesByMes(data, userID); 
    }
}
