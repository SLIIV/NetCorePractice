var tokenKey = "accessToken";
document.getElementById("login").addEventListener("click", async e => {
    e.preventDefault();
    const response = await fetch("/auth/login/Login", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Email: document.getElementById("email").value,
            Password: document.getElementById("password").value
        })
    });

    if (response.ok === true) {

    }
    else {
        console.log("Ошибка: ", response.status);
    }
})