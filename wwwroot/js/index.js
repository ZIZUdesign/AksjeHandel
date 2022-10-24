$(function () {
    
    allStocks();
    //displayStocks();

    
});


function displayStocks() {
    const item = $("#search").val();
    if (item == null) {
        allStocks();
        $("#stocks").show();
    }
    else {
        handleSearch();
        $("#searchedStocks").show();
    }


}


function allStocks() {
    if (localStorage.getItem('username') == null) {
        window.location.href = "login.html"
    }
    $.get("stock/getstocks", function (stocks) {
        formaterKunder(stocks);  
    });

    $("#logout").click(function () {
        window.localStorage.removeItem('username');
        window.location.href = "login.html";
    });
}

function formaterKunder(stocks) {
   
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Stock Name</th><th>Description</th><th>Company Name</th><th>Price</th><th>Quantity</th><th></th>" +
        "</tr>";
    for (let key in stocks) {
        ut += "<tr>" +
           
            "<td>" + stocks[key].stockName + "</td>" +
            "<td>" + stocks[key].description + "</td>" +
            "<td>" + stocks[key].companyName + "</td>" +
            "<td>" + stocks[key].price + "</td>" +
            "<td>" + stocks[key].quantity + "</td>" +

            "<td> <a class='btn btn-info' href='view.html?id=" + stocks[key].id + "'>View</a></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#stocks").html(ut);

   
}



function handleSearch() {
    const searchParam = $("#search").val();
    const url = "stock/getseacrhresult?searchTerm=" + searchParam;
    console.log(searchParam);


    $.get(url, function (stocks) {

        formaterStock(stocks);
    });
};

function formaterStock(stocks) {

    let ut = "<table class='table table-striped'>" +
        "<tr>" +
        "<th>Stock Name</th><th>Description</th><th>Company Name</th><th>Price</th><th>Quantity</th><th></th>" +
        "</tr>";
    for (let key in stocks) {
        ut += "<tr>" +

            "<td>" + stocks[key].stockName + "</td>" +
            "<td>" + stocks[key].description + "</td>" +
            "<td>" + stocks[key].companyName + "</td>" +
            "<td>" + stocks[key].price + "</td>" +
            "<td>" + stocks[key].quantity + "</td>" +

            "<td> <a class='btn btn-info' href='view.html?id=" + stocks[key].id + "'>View</a></td>" +
            "</tr>";
    }
    ut += "</table>";
    $("#searchedStocks").html(ut);
}

