namespace SuaSaude.Request.Autenticacao
{
    public class RecebeTokenBodyJson
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
