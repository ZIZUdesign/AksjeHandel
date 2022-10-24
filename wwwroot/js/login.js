
function LoginUser() {

    const user = {
        username: $("#username").val(),
        password: $("#password").val()
    };

    const url = "account/login";
    $.post(url, user, function (ok) {
        if (ok) {
            window.location.href = 'index.html';
            window.localStorage.setItem("username", user.username);
            console.log(ok);
        } else {
            $("#feil").html("User has not been registered... try agian!");

        }

    });
};

