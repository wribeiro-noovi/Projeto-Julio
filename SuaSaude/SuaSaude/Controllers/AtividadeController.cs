using Microsoft.AspNetCore.Mvc;
using SuaSaude.Request.Atividade;
using SuaSaude.UseCase.Atividades.AlterCheck;
using SuaSaude.UseCase.Atividades.Create;
using SuaSaude.UseCase.Atividades.Delete;
using SuaSaude.UseCase.Atividades.GetByDia;
using SuaSaude.UseCase.Atividades.GetByMes;
using SuaSaude.UseCase.Atividades.Update;

namespace SuaSaude.Controllers
{
    public class AtividadeController : SuaSaudeBaseController
    {
        [HttpPost("CreateAtividades")]
        public IActionResult Create([FromBody] CreateAtividadeBodyJson json,
                                    [FromServices] CreateAtividadeUseCase useCase)
        {
            var novaAtv = useCase.Execute(json.Descricao, json.Data, json.Status, json.Usuario_id);

            if (novaAtv == null) return BadRequest(new
            {
                Criacao = "Erro ao criar a atividade"
            });

            return Ok(new
            {
                Criacao = "Atividade criada"
            });
        }

        [HttpPost("GetAtvPorDia")]
        public IActionResult GetByDay([FromBody] GetDataBodyJson json,
                                      [FromServices] GetAtividadeByDiaUseCase useCase)
        {
            var atividades = useCase.Execute(json.Data, json.UserID);

            if (atividades.Count > 0) return Ok(atividades);

            return BadRequest(new
            {
                Atividades = "Não há atividades cadastrada para esse dia"
            });

        }
        [HttpPost("GetAtvPorMes")]
        public IActionResult GetByMouth([FromBody] GetDataBodyJson json,
                                        [FromServices] GetAtividadeByMesUseCase useCase)
        {
            var atividades = useCase.Execute(json.Data, json.UserID);

            if (atividades.Count > 0) return Ok(atividades);

            return BadRequest(new
            {
                Atividades = "Não há atividades cadastrada para esse mês"
            });
        }

        [HttpPost("UpdateAtv")]
        public IActionResult Update([FromBody] UpdateAtvBodyJson json,
                                    [FromServices] UpdateAtividadeUseCase useCase)
        {
            if(useCase.Execute(json.Id, json.Descricao, json.Data))
            {
                return Ok(new
                {
                    Atividade = "Atividade Atualizada",
                    Descricao = json.Descricao,
                    Data = json.Data,
                });
            }
            return BadRequest(new
            {
                Atividade = "Atividade não foi atualizada"
            });
        }
        [HttpDelete("DeleteAtv")]
        public IActionResult Delete([FromBody] IdAtvBodyJson json,
                                    [FromServices] DeleteAtividadeUseCase useCase)
        {
            if (useCase.Execute(json.AtividadeId))
            {
                return Ok(new
                {
                    Atividade = "Atividade Deletada"
                });
            }
            return BadRequest(new
            {
                Atividade = "Atividade não foi deletada"
            });
        }
        [HttpPost("AlterCheck")]
        public IActionResult Alter([FromBody] IdAtvBodyJson json,
                                   [FromServices] AlterCheckUseCase useCase)
        {
            if (useCase.Execute(json.AtividadeId))
            {
                return Ok();
            }
            return BadRequest(new
            {
                Atividade = "Status não alterado"
            });
        }
    }
}
