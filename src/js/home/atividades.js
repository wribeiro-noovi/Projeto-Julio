import { getDataAtual } from '../home/home.js'; 
let listaDeAtividades = []; // Variável global para armazenar as atividades do dia

export async function buscarAtividades(dataAtual) { 
    try {
        const userID = localStorage.getItem('userId'); 
        const requestBody = { data: dataAtual, userID: userID };
        const listaAtividades = document.getElementById('atividades-lista');

        // Limpa a lista antes de cada busca
        listaAtividades.innerHTML = '';

        const response = await fetch('https://localhost:7272/Atividade/GetAtvPorDia', {
            method: 'POST', 
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(requestBody) 
        });

        if (response.ok) {
            const atividades = await response.json();
            listaDeAtividades = atividades; 

            // Se houver atividades, renderiza
            if (atividades.length > 0) {
                atividades.forEach(atividade => {
                    const li = renderizarAtividade(atividade);
                    listaAtividades.appendChild(li);
                });
            } else {
                // Apenas adicionar a mensagem se a lista está vazia
                mostrarMensagem('Não há atividades cadastradas para esse dia', false);
            }
            return atividades;
        } else {
            // Se a requisição falhar, exibe a mensagem
            mostrarMensagem('Erro ao buscar atividades', true);
            return listaDeAtividades; 
        }
    } catch (error) {
        console.error('Erro ao buscar atividades:', error);
        mostrarMensagem('Erro ao buscar atividades', true);
        return listaDeAtividades;  
    }
}

function renderizarAtividade(atividade) {
    const li = document.createElement('li');
    const checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.checked = atividade.status;

    checkbox.addEventListener('change', async () => {
        await atualizarStatusAtividade(atividade.id); 
    });

    const label = document.createElement('label');
    label.textContent = `${atividade.descricao} - ${new Date(atividade.data).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })}`;

    li.appendChild(checkbox);
    li.appendChild(label);
    return li;
}

async function atualizarStatusAtividade(atividadeId) {
    try {
        const requestBody = { atividadeId: atividadeId }; 
        const response = await fetch('https://localhost:7272/Atividade/AlterCheck', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(requestBody)
        });

        if (response.ok) {
            console.log(`Status da atividade ${atividadeId} atualizado com sucesso.`);
            await buscarAtividadesDoDia(getDataAtual());
        } else {
            console.error('Erro ao atualizar o status da atividade:', response.statusText);
        }
    } catch (error) {
        console.error('Erro ao atualizar o status da atividade:', error);
    }
}

function mostrarMensagem(mensagem, erro) {
    const listaAtividades = document.getElementById('atividades-lista');
    const li = document.createElement('li');
    li.textContent = mensagem;
    li.classList.add(erro ? 'mensagem-erro' : 'mensagem-informativa');
    listaAtividades.appendChild(li);
}



//-----------------------------------------------------------------------------------------------------------------
//FUNCAO PARA VALIDAR OS BOTÕES DA MODAL ADD
const botaoAdicionar = document.getElementById('adicionar-atividade');
const modalAdicionar = document.getElementById('modal-adicionar');
const fecharModalAdd = modalAdicionar.querySelector('.fechar');

botaoAdicionar.addEventListener('click', () => {
    modalAdicionar.style.display = 'block'; // Mostra o modal
});

fecharModalAdd.addEventListener('click', () => {
    modalAdicionar.style.display = 'none'; // Fecha o modal
});

//FUNCAO PARA ADICIONAR UMA NOVA ATV:
const formAdicionarAtividade = document.getElementById('form-adicionar-atividade');

formAdicionarAtividade.addEventListener('submit', async (e) => {
    e.preventDefault();

    const descricao = document.getElementById('descricao').value;
    const data = document.getElementById('data').value;
    const hora = document.getElementById('hora').value;

    const userId = localStorage.getItem('userId'); 
    const dataHora = new Date(`${data}T${hora}`) 
    dataHora.setHours(dataHora.getHours() - 3);

    const requestBody = {
        descricao: descricao,
        data: dataHora,
        status: false, 
        usuario_id: userId 
    };

    try {
        const response = await fetch('https://localhost:7272/Atividade/CreateAtividades', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(requestBody)
        });

        if (response.ok) {
            modalAdicionar.style.display = 'none';
            formAdicionarAtividade.reset();
            await buscarAtividades(getDataAtual());
            await buscarAtividadesDoDia(getDataAtual());
        } else {
            console.error('Erro ao adicionar atividade:', response.statusText);
        }
    } catch (error) {
        console.error('Erro ao adicionar atividade:', error);
    }
});
//-----------------------------------------------------------------------------------------------------------------
//FUNCAO EXCLUIR
const botaoAlterarAtividade = document.getElementById('alterar-atividade');
const modalSelecionar = document.getElementById('modal-selecionar');
const fecharModalAlter = modalSelecionar.querySelector('.fechar');
const modalAlterar = document.getElementById('modal-alterar');
const tituloAtividade = document.getElementById('titulo-atividade');
const formAlterarAtividade = document.getElementById('form-alterar-atividade');
const botaoConfirmarSelecao = document.getElementById('confirmar-selecao');

let atividadeSelecionadaId = null; 
let atividadeSelecionadaDados = {}; 

botaoAlterarAtividade.addEventListener('click', async () => {
    await listarAtividades();
    modalSelecionar.style.display = 'block';
});

async function listarAtividades() {
    const atividades = await buscarAtividades(getDataAtual());
    const listaAtividadesModal = modalSelecionar.querySelector('#lista-atividades-modal');
    listaAtividadesModal.innerHTML = '';

    atividades.forEach(atividade => {
        const option = document.createElement('option');
        option.textContent = atividade.descricao;
        option.value = atividade.id; 

        // Define cada propriedade do dataset individualmente
        option.dataset.descricao = atividade.descricao;
        option.dataset.data = atividade.data;
        option.dataset.hora = atividade.hora;
        option.dataset.status = atividade.status;

        listaAtividadesModal.appendChild(option);
    });
}


botaoConfirmarSelecao.addEventListener('click', () => {
    const listaAtividadesModal = modalSelecionar.querySelector('#lista-atividades-modal');

    if (listaAtividadesModal.value) {
        atividadeSelecionadaId = listaAtividadesModal.value; 

        const optionSelecionada = listaAtividadesModal.options[listaAtividadesModal.selectedIndex];
        atividadeSelecionadaDados = {
            descricao: optionSelecionada.dataset.descricao,
            data: optionSelecionada.dataset.data,
            hora: optionSelecionada.dataset.hora,
            status: optionSelecionada.dataset.status,
        };

        preencherModalAlterar(atividadeSelecionadaDados);
        modalSelecionar.style.display = 'none';
        modalAlterar.style.display = 'block';
    } else {
        alert('Por favor, selecione uma atividade.');
    }
});

function preencherModalAlterar(dados) {
    tituloAtividade.textContent = `Alterar a atividade: ${dados.descricao}`; 
    document.getElementById('descricao-atividade').value = dados.descricao;
    document.getElementById('data-atividade').value = dados.data;
    document.getElementById('hora-atividade').value = dados.hora;
}

// Função para atualizar a atividade
async function atualizarAtividade(id, dados) {
    const response = await fetch(`https://localhost:7272/Atividade/UpdateAtv`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(dados),
    });

    if (!response.ok) {
        const errorDetails = await response.json(); // Captura detalhes do erro
        throw new Error(`Erro ao atualizar a atividade: ${errorDetails.message || 'Erro desconhecido'}`);
    }

    return await response.json();
}

formAlterarAtividade.addEventListener('submit', async (event) => {
    event.preventDefault();

    const novaDescricao = document.getElementById('descricao-atividade').value;
    const novaData = document.getElementById('data-atividade').value;
    const novaHora = document.getElementById('hora-atividade').value;

    // Concatenar data e hora
    const dataHoraAtualizada = new Date(`${novaData}T${novaHora}:00`); 
    dataHoraAtualizada.setHours(dataHoraAtualizada.getHours() - 3); 
    const dataHoraISO = dataHoraAtualizada.toISOString();

    // Verificar se o ID da atividade selecionada é válido
    if (!atividadeSelecionadaId) {
        alert('Atividade não selecionada. Tente novamente.');
        return;
    }

    try {
        await atualizarAtividade(atividadeSelecionadaId, {
            id: atividadeSelecionadaId, 
            descricao: novaDescricao,
            data: dataHoraISO, // Certifique-se de que esta data está no formato correto
        });

        const atividades = await buscarAtividades(getDataAtual());
        renderizarAtividade(atividades);
        // Fechar a modal
        modalAlterar.style.display = 'none';
    } catch (error) {
        console.log(atividadeSelecionadaId)
        console.error('Erro ao atualizar a atividade:', error);
        alert('Erro ao atualizar a atividade. Tente novamente.');
    }
});

// Fechar a modal de alterar atividade
modalAlterar.querySelector('.fechar').addEventListener('click', () => {
    modalAlterar.style.display = 'none';
});

// Fechar a modal de selecionar
fecharModalAlter.addEventListener('click', () => {
    modalSelecionar.style.display = 'none';
});

//-----------------------------------------------------------------------------------------------------------------


const botaoRemover = document.getElementById('remover-atividade');
const modalSelecionarExclusao = document.getElementById('modal-selecionar-exclusao');
const botaoConfirmarExclusao = document.getElementById('confirmar-selecao-excluir');
let atividadeParaExcluirId = null;

// Abrir modal para selecionar a atividade para exclusão
botaoRemover.addEventListener('click', async () => {
    const atividades = await buscarAtividades(getDataAtual());
    const listaAtividadesModal = modalSelecionarExclusao.querySelector('#lista-atividades-excluir');
    listaAtividadesModal.innerHTML = '';

    atividades.forEach(atividade => {
        const option = document.createElement('option');
        option.textContent = atividade.descricao;
        option.value = atividade.id; // Armazena o ID da atividade para exclusão
        option.dataset.descricao = atividade.descricao; // Armazena a descrição da atividade
        listaAtividadesModal.appendChild(option);
    });

    modalSelecionarExclusao.style.display = 'block';
});

// Fechar a modal de seleção
modalSelecionarExclusao.querySelector('.fechar').addEventListener('click', () => {
    modalSelecionarExclusao.style.display = 'none';
});

// Confirmar seleção e abrir a modal de exclusão
botaoConfirmarExclusao.addEventListener('click', () => {
    const listaAtividadesModal = modalSelecionarExclusao.querySelector('#lista-atividades-excluir');

    if (listaAtividadesModal.value) {
        atividadeParaExcluirId = listaAtividadesModal.value;

        // Obter a descrição da atividade selecionada
        const optionSelecionada = listaAtividadesModal.options[listaAtividadesModal.selectedIndex];
        const descricaoAtividade = optionSelecionada.dataset.descricao;

        // Atualizar o título do modal de exclusão
        const tituloAtividade = document.querySelector('#modal-excluir h2'); 
        tituloAtividade.textContent = `Excluir a atividade: ${descricaoAtividade}`; 

        
        document.getElementById('modal-excluir').style.display = 'block';
        modalSelecionarExclusao.style.display = 'none';
    } else {
        alert('Por favor, selecione uma atividade para excluir.');
    }
});

// Fechar a modal de exclusão
document.getElementById('modal-excluir').querySelector('.fechar').addEventListener('click', () => {
    document.getElementById('modal-excluir').style.display = 'none';
});

// Função para excluir a atividade
async function excluirAtividade(id) {
    const response = await fetch(`https://localhost:7272/Atividade/DeleteAtv`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json', // Define o tipo de conteúdo
        },
        body: JSON.stringify({ atividadeId: id }), // Envia o ID da atividade no corpo da requisição
    });

    if (!response.ok) {
        const errorDetails = await response.json(); // Captura detalhes do erro
        throw new Error(`Erro ao excluir a atividade: ${errorDetails.message || 'Erro desconhecido'}`);
    }

    return await response.json(); // Retorna a resposta em JSON, se necessário
}

// Confirmar exclusão da atividade
document.getElementById('confirmar-exclusao').addEventListener('click', async () => {
    try {
        const resultado = await excluirAtividade(atividadeParaExcluirId);

        const atividades = await buscarAtividades(getDataAtual());
        renderizarAtividade(atividades); 
        await buscarAtividadesDoDia(getDataAtual());
        
    } catch (error) {
        console.error('Erro ao excluir a atividade:', error);
        alert('Erro ao excluir a atividade. Tente novamente.');
    }

    document.getElementById('modal-excluir').style.display = 'none';
});


// Cancelar a exclusão
document.getElementById('cancelar-exclusao').addEventListener('click', () => {
    document.getElementById('modal-excluir').style.display = 'none';
});

//-----------------------------------------------------------------------------------------------------------------
//FUNCAO PRA RETORNAR O GRÁFICO:

// ID do usuário armazenado no localStorage
const userId = localStorage.getItem("userId");

// Função para buscar as atividades do dia na API
export async function buscarAtividadesDoDia(dataAtual) {
    const graficoContainer = document.getElementById("grafico-atividade");
    const mensagemErro = document.getElementById("mensagem-erro");

    try {
        const response = await fetch("https://localhost:7272/Atividade/GetAtvPorDia", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                data: dataAtual,
                userID: userId
            })
        });
        
        if (!response.ok) {
            throw new Error("Erro ao buscar atividades do dia.");
        }
        
        const atividades = await response.json();

        // Filtra as atividades concluídas e pendentes
        const atividadesConcluidas = atividades.filter(atividade => atividade.status === true).length;
        const atividadesPendentes = atividades.length - atividadesConcluidas;

        // Verifica se há atividades
        if (atividades.length > 0) {
            // Oculta a mensagem de erro e exibe o gráfico
            mensagemErro.style.display = "none";
            graficoContainer.style.display = "block";

            // Configura o gráfico com os dados obtidos
            renderizarGraficoAtividades(atividades.length, atividadesConcluidas, atividadesPendentes);
        } else {
            // Oculta o gráfico e exibe a mensagem de erro
            graficoContainer.style.display = "none";
            mensagemErro.style.display = "block";
        }
        
    } catch (error) {
        console.error("Erro:", error);
        // Oculta o gráfico e exibe a mensagem de erro em caso de erro
        graficoContainer.style.display = "none";
        mensagemErro.style.display = "block";
    }
}


// Função para renderizar o gráfico de pizza
function renderizarGraficoAtividades(totalAtividades, concluidas, pendentes) {
    const ctx = document.getElementById('grafico').getContext('2d');
    const tituloGrafico = `Atividades do dia: ${totalAtividades}`; // Título dinâmico com o total de atividades

    const config = {
        type: 'pie',
        data: {
            labels: ['Concluídas', 'Pendentes'],
            datasets: [{
                label: tituloGrafico,
                data: [concluidas, pendentes],
                backgroundColor: ['#4CAF50', '#FF5252'], // Verde para concluídas, vermelho para pendentes
                hoverOffset: 4
            }]
        },
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: tituloGrafico,
                    font: {
                        size: 18 // Aumenta o tamanho do título
                    }
                },
                legend: {
                    display: true,
                    position: 'bottom'
                },
                tooltip: {
                    callbacks: {
                        label: function(context) {
                            const label = context.label || '';
                            const value = context.raw || 0;
                            return `${label}: ${value}`;
                        }
                    }
                }
            }
        }
    };
    
    

    // Inicializa ou atualiza o gráfico de atividades
    if (window.graficoAtividades) {
        window.graficoAtividades.destroy(); 
    }
    window.graficoAtividades = new Chart(ctx, config); // Cria o novo gráfico
}

// Chama a função para buscar as atividades do dia ao carregar a página
buscarAtividadesDoDia();

