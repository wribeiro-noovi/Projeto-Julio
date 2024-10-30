using Microsoft.AspNetCore.Mvc;
using SuaSaude.Request.Autenticacao;
using SuaSaude.UseCase.Autenticacao.EnviarToken;
using SuaSaude.UseCase.Autenticacao.ReceberToken;
using SuaSaude.UseCase.Autenticacao.RecuperarSenha;
using SuaSaude.UseCase.Autenticacao.Validar;
using SuaSaude.UseCase.Autenticacao.VerificaEmail;

namespace SuaSaude.Controllers
{
    public class AutenticacaoController : SuaSaudeBaseController
    {
        [HttpPost("ValidarLogin")]
        public IActionResult ValidarLogin([FromBody] AutenticacaoBodyJson json,
                                          [FromServices] ValidarLoginUseCase useCase)
        {
            var usuarioLogado = useCase.Execute(json.email, json.senha);

            if (usuarioLogado != null)
            {
                return Ok(usuarioLogado);
            }
            return BadRequest(new
            {
                UsuarioConectado = false
            });
        }

        [HttpPost("EnviarToken")]
        public IActionResult EnviaToken([FromBody] EnviaTokenBodyJson json,
                                        [FromServices] VerificaEmailUseCase useCaseEmail,
                                        [FromServices] EnviarTokenUseCase useCase)
        {
            if (!useCaseEmail.Execute(json.Email))
            {
                return BadRequest(new
                {
                    Usuario = "E-mail fornecido não localizado na base de dados"
                });
            }
            var tokenEnviado = useCase.Execute(json.Email);
            if (tokenEnviado is null) return BadRequest(new
            {
                Enviado = false
            });
            return Ok(new
            {
                Enviado = true,
            });
        }
        [HttpPost("ValidarToken")]
        public IActionResult ValidarToken([FromBody] RecebeTokenBodyJson json,
                                          [FromServices] ReceberTokenUseCase useCase)
        {
            var tokenValido = useCase.Execute(json.Token, json.Email);

            if (tokenValido is null) return BadRequest(new
            {
                Token = "Token Inválido"
            });

            if (tokenValido.DataEnvio.AddMinutes(5) < DateTime.Now) return BadRequest(new
            {
                Token = "Token Expirado"
            });

            return Ok(new
            {
                Token = "Token Válido"
            });
        }

        [HttpPost("RecuperaSenha")]
        public IActionResult AlteraSenha([FromBody] RecuperaSenhaBodyJson json,
                                         [FromServices] RecuperarSenhaUseCase useCase)
        {
            if (!useCase.Execute(json.Email, json.Senha, json.SenhaConfirmacao))
                return BadRequest(new
                {
                    SenhaAtualizada = "Falha ao atualizar a senha"
                });

            return Ok(new
            {
                SenhaAtualizada = true
            });
        }
    }
}
