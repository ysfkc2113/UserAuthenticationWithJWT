// navbar.js
// Set the API base URL - Backend'inizin çalıştığı URL'yi doğru şekilde ayarlayın
const API_BASE_URL = 'https://localhost:7222/api';

// Function to decode JWT payload (JWT'deki kullanıcı bilgilerini okumak için)
function decodeJwtPayload(token) {
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
        return JSON.parse(jsonPayload);
    } catch (e) {
        console.error("Error decoding JWT payload:", e);
        return null;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    // Inject the navbar HTML into the body (or a specific div if you prefer)
    const navbarHtml = `
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
            <div class="container">
                <a class="navbar-brand" href="index.html">Kulüp Yönetim Sistemi</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav me-auto">
                        <li class="nav-item">
                            <a class="nav-link" href="index.html">Ana Sayfa</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="events.html">Etkinlikler</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="clubs.html">Kulüpler</a>
                        </li>
                        <li class="nav-item"> <a class="nav-link" href="my-clubs.html">Kulüplerim</a>
                        </li>
                        <li class="nav-item dropdown" id="dashboardDropdown" style="display: none;">
                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownDashboard" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                Dashboard
                            </a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdownDashboard">
                                <li><a class="dropdown-item" href="admin-dashboard.html">Admin Dashboard</a></li>
                                <li><a class="dropdown-item" href="academician-dashboard.html">Academician Dashboard</a></li>
                                <li><a class="dropdown-item" href="clubManagerDashboard.html">Club Manager Dashboard</a></li>
                            </ul>
                        </li>
                    </ul>
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item" id="loginButton">
                            <a class="btn btn-outline-light" href="login.html">Giriş</a>
                        </li>
                        <li class="nav-item dropdown" id="userMenu" style="display: none;">
                            <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownUser" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="fas fa-user-circle me-1"></i> <span id="usernameDisplay">Kullanıcı Adı</span>
                            </a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdownUser">
                                <li><a class="dropdown-item" href="profile.html">Profil</a></li>
                                <li><hr class="dropdown-divider"></li>
                                <li><a class="dropdown-item" href="#" id="logoutButton">Çıkış</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    `;

    document.body.insertAdjacentHTML('afterbegin', navbarHtml); // Inserts the navbar at the beginning of the body

    // localStorage'dan token ve kullanıcı bilgilerini al
    const accessToken = localStorage.getItem('accessToken');
    let username = localStorage.getItem('username');
    let role = localStorage.getItem('role');

    console.log("Navbar script yüklendi.");
    console.log("localStorage'dan okunan accessToken:", accessToken ? accessToken.substring(0, 30) + '...' : 'Yok');
    console.log("localStorage'dan okunan username:", username);
    console.log("localStorage'dan okunan role:", role);

    // Eğer accessToken varsa ancak username veya role eksikse, token'dan çözmeye çalış
    if (accessToken && (!username || !role)) {
        const payload = decodeJwtPayload(accessToken);
        if (payload) {
            username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            const roles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            role = Array.isArray(roles) ? roles[0] : roles;
            localStorage.setItem('username', username);
            localStorage.setItem('role', role);
            console.log("Token'dan çözümlenen ve kaydedilen username:", username);
            console.log("Token'dan çözümlenen ve kaydedilen role:", role);
        }
    }

    // Navbar'ı kullanıcının oturum durumuna ve rolüne göre ayarla
    function setupNavbar() {
        if (accessToken) {
            document.getElementById('loginButton').style.display = 'none';
            document.getElementById('userMenu').style.display = 'block';
            document.getElementById('usernameDisplay').textContent = username || 'Kullanıcı';

            const dashboardDropdown = document.getElementById('dashboardDropdown');
            if (role === 'Admin' || role === 'Academician' || role === 'Club Manager') {
                dashboardDropdown.style.display = 'block';
            } else {
                dashboardDropdown.style.display = 'none';
            }

            // 'Kulüplerim' bağlantısı artık her zaman görünür olduğu için bu kontrol kaldırıldı.
            // 'Etkinliklerim' navigasyon öğesi kaldırıldığı için bu kontrol de kaldırıldı.
        } else {
            document.getElementById('loginButton').style.display = 'block';
            document.getElementById('userMenu').style.display = 'none';
            document.getElementById('dashboardDropdown').style.display = 'none';
            // 'Kulüplerim' bağlantısı artık her zaman görünür olduğu için bu kontrol kaldırıldı.
            // 'Etkinliklerim' navigasyon öğesi kaldırıldığı için bu kontrol de kaldırıldı.
        }
    }

    setupNavbar();

    // Çıkış (Logout) butonu dinleyicisi
    document.getElementById('logoutButton').addEventListener('click', (e) => {
        e.preventDefault();
        localStorage.removeItem('accessToken');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('username');
        localStorage.removeItem('role');
        window.location.href = 'login.html';
    });
});