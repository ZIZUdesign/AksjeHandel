$(function () {
    const id = window.location.search.substring(1);
    const url = "stock/getstock?" + id;

    $.get(url, function (stock) {
        $("#id").val(stock.id);
        $("#companyname").text(stock.companyName);
        $("#price").text("Price: "+stock.price+ " NOK");
        $("#quantity").val(stock.quantity);

      


    });

});



function buyStock() {
    const url = 'transaction/buystocks';

    const buyStock = {
         stockId: $("#id").val(),
         userName: window.localStorage.getItem('username'),
         quantity: $("#quantity").val()
    };
    

    
        


    $.post(url, buyStock, function (ok) {
        if (ok) {
            window.location.href = "BoughtStocks.html";
        }

    });
};



    
