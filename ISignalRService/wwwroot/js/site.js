// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        /////.withUrl("http://signalr.internal.com:30503/matt")
        .withUrl("http://localhost:5000/matt")
        .build();

   

    connection.on("connected", () => {
        const statusDiv = document.getElementById("status");
        statusDiv.innerHTML = "connected and posted";
    }
    );

    connection.on("status_fail", function (result) {
        var statusDiv = document.getElementById("status");
        statusDiv.innerHTML = "Failed, Reason is " + result.cause;
    }
    );

    connection.on("disconnected", function () {
        connection.stop();
        statusDiv.innerHTML = "disconnected";
    }
    );

    connection.start()
        .catch(err => console.error(err.toString()));

   // connection.invoke("Initialize", 1);
};

setupConnection();

document.getElementById("submit").addEventListener("click", e => {
    e.preventDefault();
    var id = document.getElementById("id").value;
    var name = document.getElementById("name").value;
    var category = document.getElementById("category").value;
    var price = document.getElementById("price").value;

    fetch("http://localhost:5002/api/product",
    //fetch("http://product.internal.com:30503/api/product",
        {
            method: "POST",
            body: JSON.stringify({ id, name, category, price }),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(connection.invoke("Initialize", id));
});