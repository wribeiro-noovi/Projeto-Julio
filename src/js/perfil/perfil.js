document.addEventListener("DOMContentLoaded", () => {
    const userId = localStorage.getItem("userId");
    
    if (userId) {
        const url = "https://localhost:7272/Usuario/GetUsuario";
        
        const body = JSON.stringify({ usuario_Id: userId });

        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: body
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro ao obter dados do usuário');
            }
            return response.json();
        })
        .then(data => {
            document.getElementById("nome").value = data.nome;
            document.getElementById("sexo").value = data.sexo; 
            document.getElementById("data-nascimento").value = data.dataNasc.split('T')[0];
            document.getElementById("altura").value = data.altura;
            document.getElementById("peso").value = data.peso;
            document.getElementById("email").value = data.email;

            localStorage.setItem("senha", data.senha);
        })
        .catch(error => {
            console.error('Erro:', error);
        });
    } else {
        console.error("User ID não encontrado no localStorage");
    }
});

//-----------------------------------------------------------------------------------------------------------------
// ALTERAR SENHA

// Função para abrir o modal
function abrirModal() {
    document.getElementById("modal-alterar-senha").style.display = "block";
}

// Função para fechar o modal
function fecharModal() {
    document.getElementById("modal-alterar-senha").style.display = "none";
}

// Adiciona o evento de click para o botão "Alterar Senha"
document.getElementById("alterar-senha").addEventListener("click", abrirModal);

// Adiciona o evento de click para o botão de fechar
document.querySelector(".fechar").addEventListener("click", fecharModal);

// Adiciona o evento para fechar o modal quando clicar fora do conteúdo
window.addEventListener("click", (event) => {
    const modal = document.getElementById("modal-alterar-senha");
    if (event.target === modal) {
        fecharModal();
    }
});

// Função para atualizar a senha
async function atualizarSenha(event) {
    event.preventDefault(); // Evita o envio padrão do formulário

    const usuarioID = localStorage.getItem("userId");
    const senhaAtual = document.getElementById("senha-atual").value;
    const novaSenha = document.getElementById("nova-senha").value;
    const confirmaSenha = document.getElementById("confirmar-senha").value;

    // Limpa a mensagem de erro anterior
    document.getElementById("mensagem-erro").textContent = "";

    // Monta o corpo da requisição
    const body = JSON.stringify({
        usuarioID: parseInt(usuarioID), // Converte para número
        senhaAtual: senhaAtual,
        novaSenha: novaSenha,
        confirmaSenha: confirmaSenha
    });

    try {
        const response = await fetch("https://localhost:7272/Usuario/UpdateSenha", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: body
        });

        const data = await response.json();

        if (response.status === 200) {
            document.getElementById("mensagem-sucesso").textContent = data.status;
        } else {
            // Exibe a mensagem de erro no parágrafo
            document.getElementById("mensagem-erro").textContent = data.status;
        }
    } catch (error) {
        console.error("Erro ao atualizar a senha:", error);
        document.getElementById("mensagem-erro").textContent = "Ocorreu um erro ao tentar atualizar a senha.";
    }
}


// Adiciona o evento de submit ao formulário de alteração de senha
document.getElementById("form-alterar-senha").addEventListener("submit", atualizarSenha);
//-----------------------------------------------------------------------------------------------------------------
//FUNCAO DELETE
document.addEventListener('DOMContentLoaded', () => {
    const modalDeletar = document.getElementById('modal-deletar');
    const btnDeletar = document.getElementById('excluir-perfil'); // Botão que abre o modal
    const btnFecharModal = document.querySelector('#modal-deletar .fechar'); // Botão para fechar o modal
    const btnConfirmarDeletar = document.querySelector('.botoes-modal #confirmar-deletar'); // Botão de confirmação no modal
    const btnCancelarDeletar = document.querySelector('.botoes-modal #cancelar-deletar'); // Botão de cancelar no modal
    const mensagemErro = document.getElementById('mensagem-erro');
    const mensagemSucesso = document.getElementById('mensagem-sucesso');

    // Função para abrir o modal
    btnDeletar.addEventListener('click', () => {
        modalDeletar.style.display = 'block'; // Exibir o modal
    });

    // Função para fechar o modal ao clicar no botão fechar
    btnFecharModal.addEventListener('click', () => {
        modalDeletar.style.display = 'none'; // Ocultar o modal
    });

    // Função para fechar o modal ao clicar no botão cancelar
    btnCancelarDeletar.addEventListener('click', () => {
        modalDeletar.style.display = 'none'; // Ocultar o modal
    });

    // Função para confirmar a exclusão
    btnConfirmarDeletar.addEventListener('click', async () => {
        const usuarioId = localStorage.getItem("userId"); // Obter o ID do usuário do localStorage

        try {
            const response = await fetch('https://localhost:7272/Usuario/DeleteUsuario', {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    usuario_Id: usuarioId // Passar o ID do usuário no corpo da requisição
                })
            });

            if (!response.ok) {
                // Se a resposta não for OK, exiba uma mensagem de erro
                const errorData = await response.json();
                mensagemErro.textContent = errorData.status; // Exibir a mensagem de erro recebida
                mensagemSucesso.textContent = ''; // Limpar mensagem de sucesso
            } else {
                // Se a exclusão for bem-sucedida, redirecionar para a página de login
                modalDeletar.style.display = 'none'; // Ocultar o modal
                window.location.href = '../../../index.html'; // Redirecionar para a página de login
            }
        } catch (error) {
            // Captura de erros de rede
            mensagemErro.textContent = 'Ocorreu um erro ao tentar deletar a conta.';
            mensagemSucesso.textContent = ''; // Limpar mensagem de sucesso
        }
    });

    // Fecha o modal se o usuário clicar fora dele
    window.addEventListener('click', (event) => {
        if (event.target === modalDeletar) {
            modalDeletar.style.display = 'none'; // Ocultar o modal
        }
    });
});
