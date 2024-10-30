document.addEventListener("DOMContentLoaded", function() {
    const emailSection = document.getElementById('emailSection');
    const tokenSection = document.getElementById('tokenSection');
    const novaSenhaSection = document.getElementById('novaSenha');

    const enviarTokenBtn = document.getElementById('enviarTokenBtn');
    const validaTokenBtn = document.getElementById('verificaTokenBtn');
    const form = document.getElementById('recuperarSenhaForm');

    const msgErroEmail = document.querySelector('#email-falha')
    const msgErroToken = document.querySelector('#token-falha')
    const msgErroSenha = document.querySelector('#nova-senha-falha')
    const msgSucessoSenha = document.querySelector('#nova-senha-sucesso')

    // Função para enviar o token de recuperação
    async function enviarToken() {
        msgErroEmail.style.display = "none"

        const email = document.getElementById('email').value;
        const confirmaEmail = document.getElementById('confirma-email').value;

        if (email !== confirmaEmail) {
            msgErroEmail.textContent = "Os E-mails não coincidem."
            msgErroEmail.style.display = "block"
            return;
        }

        const response = await fetch('https://localhost:7272/Autenticacao/EnviarToken', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email: email }),
        });

        if (response.ok) {
            tokenSection.classList.remove('hiden'); // Exibe o campo para inserir o token
            emailSection.classList.add('hiden') // esconde campo do e-mail
        } else {
            const responseErros = await response.json();
            const erro = Object.values(responseErros.usuario).join("");
            msgErroEmail.textContent = erro
            msgErroEmail.style.display = "block"
            document.querySelector('#recuperarSenhaForm').reset();
            return;
        }
    }

    // Função para validar o token
    async function validaToken() {
        msgErroToken.style.display = "none"; 
        msgErroToken.textContent = "";

        const email = document.getElementById('email').value;
        const token = document.getElementById('token').value;

        const response = await fetch('https://localhost:7272/Autenticacao/ValidarToken', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email: email, token: token}),
        });

        if (response.ok) {
            novaSenhaSection.classList.remove('hiden'); // Exibe o campo para alterar a senha
            tokenSection.classList.add('hiden');
        } else {
            const responseErros = await response.json();
            const erro = responseErros.token;
            msgErroToken.textContent = erro
            msgErroToken.style.display = "block"

            return;
        }
    }

    // Função para atualizar a senha
    async function atualizarSenha(event) {
        event.preventDefault();

        msgErroSenha.style.display = "none"
        msgSucessoSenha.style.display = "none"

        const email = document.getElementById('email').value;
        const novaSenha = document.getElementById('nova-senha').value;
        const confirmaSenha = document.getElementById('confirma-nova-senha').value;

        if (novaSenha !== confirmaSenha) {
            msgErroSenha.textContent = "As senhas não coincidem."
            msgErroSenha.style.display = "block"
            return;
        }

        const response = await fetch('https://localhost:7272/Autenticacao/RecuperaSenha', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email: email, senha: novaSenha, senhaConfirmacao: confirmaSenha }),
        });

        if (response.ok) {
            msgSucessoSenha.textContent = "Senha atualizada com sucesso!"
            msgSucessoSenha.style.display = "block"

            const senha = document.getElementById('nova-senha')
            const nSenha = document.getElementById('confirma-nova-senha')
            senha.disabled = true;
            nSenha.disabled = true;
        } else {
            const responseErros = await response.json();
            const erro = responseErros.senhaAtualizada;
            msgErroSenha.textContent = erro
            msgErroSenha.style.display = "block"

            return;
        }
    }

    // Vinculando os eventos ao JS
    enviarTokenBtn.addEventListener('click', enviarToken);
    validaTokenBtn.addEventListener('click', validaToken);
    form.addEventListener('submit', atualizarSenha);
});
