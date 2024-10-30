document.querySelector('#login-form').addEventListener('submit', async (event) => {
    event.preventDefault();

    const email = document.querySelector('#email-input').value;
    const senha = document.querySelector('#senha-input').value;
    const erro = document.querySelector('#erro-login');

    try {
        const response = await fetch('https://localhost:7272/Autenticacao/ValidarLogin', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email: email, senha: senha })
        });

        if (response.ok) {
            const usuario = await response.json();

            // Salva as informações do usuário no localStorage
            localStorage.setItem("isLoggedIn", "true");
            localStorage.setItem("userId", usuario.id); 
            localStorage.setItem("username", usuario.nome);
            localStorage.setItem("email", usuario.email); // Mudado para "email"
            localStorage.setItem("sexo", usuario.sexo); // Mudado para "sexo"
            localStorage.setItem("altura", usuario.altura); // Mudado para "altura"
            localStorage.setItem("peso", usuario.peso); // Mudado para "peso"
            localStorage.setItem("dataNasc", usuario.dataNasc); // Mudado para "dataNasc"

            window.location.pathname = '../../src/html/home/home.html';
        } else {
            erro.style.display = "block";
        }
    } catch (error) {
        console.error('Erro na conexão:', error);
    }
});
