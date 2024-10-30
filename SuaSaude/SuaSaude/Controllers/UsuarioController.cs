using Microsoft.AspNetCore.Mvc;
using SuaSaude.Request.Usuario;
using SuaSaude.UseCase.Usuarios.AlterSenha;
using SuaSaude.UseCase.Usuarios.Create;
using SuaSaude.UseCase.Usuarios.Delete;
using SuaSaude.UseCase.Usuarios.Get;
using SuaSaude.UseCase.Usuarios.ValidarDados;
using SuaSaude.Util.Cadastro;

namespace SuaSaude.Controllers
{
    public class UsuarioController : SuaSaudeBaseController
    {
        [HttpPost("CreateUsuario")]
        public IActionResult CreateUsuario([FromBody] UsuarioBodyJson json,
                                           [FromServices] ValidarDadosUseCase useCaseValidar,
                                           [FromServices] CreateUsuarioUseCase useCase)
        {
            var dadosInvalidos = useCaseValidar.Execute(json.Nome, json.Sexo, json.DataNasc, json.Altura, json.Peso, json.Email, json.Senha);


            if (dadosInvalidos.Count > 0)
            {
                return BadRequest(new
                {
                    Erros = dadosInvalidos
                });
            }

            var novoUsuario = useCase.Execute(json.Nome, json.Sexo, json.DataNasc, json.Altura, json.Peso, json.Email, json.Senha);

            if (novoUsuario is null)
            {
                return BadRequest(new { Mensagem = "Falha ao criar o usuário." });
            }

            return Ok(novoUsuario);
        }

        [HttpPost("GetUsuario")]
        public IActionResult GetUsuario([FromBody] UserIDBodyJson json,
                                         [FromServices] GetUsuarioUseCase useCase)
        {
            var user = useCase.Execute(json.Usuario_Id);

            if (user is null) return BadRequest();

            return Ok(user);
        }

        [HttpPost("UpdateSenha")]
        public IActionResult UpdateSenha([FromBody] AlterarSenhaBodyJson json,
                                         [FromServices] AlterarSenhaUsuarioUseCase useCase)
        {
            var result = useCase.Execute(json.UsuarioID, json.SenhaAtual, json.NovaSenha, json.ConfirmaSenha);

            if (result == Enum.AlteracaoSenhaStatus.UsuarioNaoEncontrado)
                return BadRequest(new
                {
                    status = "Usuario Não Encontrado"
                });

            if (result == Enum.AlteracaoSenhaStatus.SenhaAtualIncorreta)
                return BadRequest(new
                {
                    status = "Senha Atual Incorreta"
                });

            if (result == Enum.AlteracaoSenhaStatus.SenhasNaoConferem)
                return BadRequest(new
                {
                    status = "Senhas não coincidem"
                });

                return Ok(new
                {
                    status = "Senha Alterada"
                });
        }
        [HttpDelete("DeleteUsuario")]
        public IActionResult Delete([FromBody] UserIDBodyJson json,
                                    [FromServices] DeleteUsuarioUseCase useCase)
        {
            if (useCase.Execute(json.Usuario_Id))
            {
                return Ok(new
                {
                    status = "Usuario Deletado com sucesso"
                });
            }
            return BadRequest(new
            {
                status = "Erro ao deletar"
            });
        }
    }

}
