﻿namespace SuaSaude.Request.Usuario
{
    public class AlterarSenhaBodyJson
    {
        public int UsuarioID { get; set; }
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
        public string ConfirmaSenha { get; set; } = string.Empty;
    }
}
