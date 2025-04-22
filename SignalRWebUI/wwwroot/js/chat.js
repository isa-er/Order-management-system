var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7113/SignalRHub").build();
document.getElementById("sendbutton").disabled = true; // en başta butonu devre dışı bırak
connection.on("ReceiveMessage", function (user, message) {
    var currentTime = new Date();
    var currentHour = currentTime.getHours(); // saat bilgisini al
    var currentMinute = currentTime.getMinutes(); // dakika bilgisini al

    var li = document.createElement("li"); // createElement ile li etiketi oluştur . SATIR GİBİ DÜŞÜN
    var span = document.createElement("span"); // createElement ile span etiketi oluştur

    span.style.fontWeight = "bold"; // span etiketinin font ağırlığını kalın yap
    span.textContent = user; // span etiketinin içeriğini kullanıcı adı yap
    li.appendChild(span); // span etiketini li etiketinin içine ekle 
    li.innerHTML += `: ${message}-${currentHour}:${currentMinute}`;
    document.getElementById("messagelist").appendChild(li); // li etiketini messagesList etiketinin içine ekle")
});


connection.start().then(function () {
    document.getElementById("sendbutton").disabled = false; // bağlantı sağlandığında butonu etkinleştir
}).catch(function (err) {
    return console.error(err.toString());
});



document.getElementById("sendbutton").addEventListener("click", function (event) { //addEventListener ile butona tıklandığında
    var user = document.getElementById("userinput").value; // userinputun değerini .value ile aldık
    var message = document.getElementById("messageinput").value;

    connection.invoke("SendMessage", user, message).catch(function (error) {
        return console.error(error.toString());
    }); // connection.invoke ile SendMessage metodunu çağırdık. Hemen yukarıdaki user ve message değerlerini gönderdik

    event.preventDefault(); // formun submit olmasını engelle

});



