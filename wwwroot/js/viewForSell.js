
   $(function () {
       //const id = window.location.search.substring(1);
       const username = window.localStorage.getItem('username');
       const urlParams = new URLSearchParams(window.location.search);
       const myId = urlParams.get('id');

       const params = {
           id: myId,
           username: username
       };

       const param = new URLSearchParams(params);
       const queryString = param.toString();

       const url = "transaction/getuserstocktobesold?" + queryString;





        $.get(url, function (stock) {
        $("#id").val(stock.id);
        $("#companyname").text(stock.companyName);
        $("#price").text("Price: "+stock.price+ " NOK");
        $("#quantity").val(stock.quantity);




        });

        });



  function sellStock() {
        const url = 'transaction/sellstocks';

        const stock = {
        stockId: $("#id").val(),
        userName: window.localStorage.getItem('username'),
        quantity: $("#quantity").val()
        };






        $.post(url, stock, function (ok) {
        if (ok) {
        window.location.href = "BoughtStocks.html";
        }

        });
        };