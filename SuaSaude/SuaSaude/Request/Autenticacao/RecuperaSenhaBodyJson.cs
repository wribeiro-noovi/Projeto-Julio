namespace SuaSaude.Request.Autenticacao
{
    public class RecuperaSenhaBodyJson
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string SenhaConfirmacao { get; set; } = string.Empty;
    }
}
