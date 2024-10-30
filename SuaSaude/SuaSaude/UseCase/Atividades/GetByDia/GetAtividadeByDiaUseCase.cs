using SuaSaude.Contracts;
using SuaSaude.entities;

namespace SuaSaude.UseCase.Atividades.GetByDia
{
    public class GetAtividadeByDiaUseCase
    {
        private readonly IAtividadeRepository _repository;
        public GetAtividadeByDiaUseCase(IAtividadeRepository repository) => _repository = repository;

        public List<Atividade> Execute(DateTime data, int userID) => _repository.GetAtividadesByDia(data, userID);
    }
}
