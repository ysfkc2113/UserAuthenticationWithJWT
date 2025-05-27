
window.addEventListener('DOMContentLoaded', () => {
    const token = localStorage.getItem('accesstoken');
    const loginLink = document.querySelector('.nav-link[href="login.html"]');
    if (token && loginLink) {
        loginLink.innerText = "Çıkış";
        loginLink.href = "#";
        loginLink.addEventListener('click', () => {
            localStorage.removeItem('accesstoken');
            window.location.reload();
        });
    }
});
