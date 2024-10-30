import { buscarAtividades } from '../home/atividades.js';
import { buscarAtividadesDoDia} from '../home/atividades.js';

const mesAtualSpan = document.getElementById("mes-atual");
const diaAtualSpan = document.getElementById("dia-atual");
const mesAnteriorBtn = document.getElementById("mes-anterior");
const mesProximoBtn = document.getElementById("mes-proximo");
const diaAnteriorBtn = document.getElementById("dia-anterior");
const diaProximoBtn = document.getElementById("dia-proximo");

let dataAtual = new Date(); // Declare aqui para que esteja disponível em outros módulos

const meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];

function atualizarData() {
    mesAtualSpan.textContent = meses[dataAtual.getMonth()];
    diaAtualSpan.textContent = dataAtual.getDate();
    buscarAtividades(dataAtual); 
    buscarAtividadesDoDia(dataAtual)
}

mesAnteriorBtn.addEventListener("click", () => {
    dataAtual.setMonth(dataAtual.getMonth() - 1);
    atualizarData();
});

mesProximoBtn.addEventListener("click", () => {
    dataAtual.setMonth(dataAtual.getMonth() + 1);
    atualizarData();
});

diaAnteriorBtn.addEventListener("click", () => {
    dataAtual.setDate(dataAtual.getDate() - 1);
    atualizarData();
});

diaProximoBtn.addEventListener("click", () => {
    dataAtual.setDate(dataAtual.getDate() + 1);
    atualizarData();
});

atualizarData();

export function getDataAtual() {
    return dataAtual;
}
