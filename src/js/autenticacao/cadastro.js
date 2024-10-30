document.querySelector('#cadastro-form').addEventListener('submit', async(event) =>{
    event.preventDefault();

    const msgSucesso = document.querySelector('#cadastro-sucesso');
    const msgErro = document.querySelector('#cadastro-falha');

    const nome = document.querySelector('#nome-input').value;
    const sexo = document.querySelector('#sexo-input').value;
    const dataNasc = document.querySelector('#dataNasc-input').value;
    const altura = document.querySelector('#altura-input').value || 0; // Define 0 se estiver vazio
    const peso = document.querySelector('#peso-input').value || 0; // Define 0 se estiver vazio
    const email = document.querySelector('#email-input').value;
    const senha = document.querySelector('#senha-input').value;
    const confirmaSenha = document.querySelector('#confirma-senha-input').value;

        try{
            msgErro.style.display = "none"
            msgSucesso.style.display = "none"

            if(senha != confirmaSenha){
                msgErro.textContent = "As senhas não coincidem.";
                msgErro.style.display = "block"
                return;
                
            }
            else if (!dataNasc) {
                msgErro.textContent = "Preencha a data de nascimento";
                msgErro.style.display = "block";
                return;
            }
            else{
                const response = await fetch('https://localhost:7272/Usuario/CreateUsuario',{
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(
                    { 
                        nome: nome,
                        sexo: sexo,
                        dataNasc: new Date(dataNasc).toISOString(),
                        altura: altura,
                        peso: peso,
                        email: email,
                        senha: senha
                    })
                })

                if(response.ok){
                    msgSucesso.style.display = "block"
                } else{
                    const responseErros = await response.json();
                    const erros = Object.values(responseErros.erros).join("\n");
                    msgErro.textContent = erros
                    msgErro.style.display = "block"
                    document.querySelector('#cadastro-form').reset();
                }
            }
            
        }catch (error) {
            console.error('Erro na conexão:', error);
        }    
});