

function buyStock() {
    const url = 'transaction/buystocks';
    const username = window.localStorage.getItem('username');
    const quantity = $("#quantity").val();

    var query = window.location.search.substring(1);
    var vars = query.split("=");
    var id = vars[1];

    

    $.post(url, username, id, quantity, function (ok) {
        if (ok) {
            window.location.href = "home.html";
        }

    });
};




