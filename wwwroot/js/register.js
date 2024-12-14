//document.getElementById("register").addEventListener("click", async e => {
//    e.preventDefault();
//    const response = await fetch("/auth/register/Create", {
//        method: "POST",
//        headers: {"Content-Type": "application/json" },
//        body: JSON.stringify({
//            Name: document.getElementById("name"),
//            Email: document.getElementById("email"),
//            Password: document.getElementById("password")
//        })
//    });
//    if (response.redirected === true) {
//        console.log("user created");
//    }
//    else {
//        console.log("Ошибка: ", response.status);
//    }
//})


$("#register").click(function (e) {
    e.preventDefault();
    var form = $("#registerForm");
    var data = {
        Name: $("#name").val(),
        Email: $("#email").val(),
        Password: $("#password").val()
    };
    $.ajax({
        type: "POST",
        url: "/auth/register/Create",
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (response) {
            window.location.replace("/auth/login");
        },
        error: function (error) {
            console.log("Ошибка ", error)
        }
    });

});