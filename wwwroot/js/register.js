function validateUsername(username) {
    const regexp = /^[a-zA-ZæøåÆØÅ\.\ \-]{2,20}$/;
    const ok = regexp.test(username);
    if (!ok) {
        $("#feilBrukernavn").html("Brukernavnet må bestå av 2 til 20 bokstaver");
        return false;
    } 
    else {
        $("#feilBrukernavn").html("");
        return true;
    }
}

function registerUser() {
    
    const user = {
        username: $("#username").val(),
        password: $("#password").val()
    };
    

    const url = "account/register";
    $.post(url, user, function (Ok) {
        if (Ok) {
            window.location.href = 'login.html';
        } else {
            $("#feil").html("User has not been registered... try agian!");
           
        }
        
    });
};