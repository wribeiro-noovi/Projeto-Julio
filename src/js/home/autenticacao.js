document.addEventListener("DOMContentLoaded", () => {
    const isLoggedIn = localStorage.getItem("isLoggedIn");

    if (isLoggedIn !== "true") {
        window.location.href = "../../../index.html";
    } else {
        const username = localStorage.getItem("username");
        const userId = localStorage.getItem("userId"); 
        const email = localStorage.getItem("email"); 
        const peso = localStorage.getItem("peso"); 
        const altura = localStorage.getItem("altura"); 
        const sexo = localStorage.getItem("sexo"); 

        const headerContent = document.getElementById("header-content");
        const welcomeMessage = document.createElement("p");
        welcomeMessage.textContent = `Bem-vindo, ${username}!`;
        welcomeMessage.classList.add("welcome-message");
        headerContent.appendChild(welcomeMessage);
    }

    const logoutButton = document.getElementById('logout');
    logoutButton.addEventListener('click', () => {
        localStorage.removeItem('isLoggedIn');
        localStorage.removeItem('username');
        localStorage.removeItem('userId'); 
        localStorage.removeItem('email'); 
        localStorage.removeItem('peso'); 
        localStorage.removeItem('altura'); 
        localStorage.removeItem('sexo'); 
        window.location.pathname = '../../index.html';
    });
});
