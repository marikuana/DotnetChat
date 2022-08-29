const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

hubConnection.on("OnMessage", function (message) {
    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(message.author.userName + ": "));

    let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message.text));

    var firstElem = document.getElementById("chat").lastChild;
    document.getElementById("chat").insertBefore(elem, firstElem);
});

let selectChatId = 1; // 0

async function SendMessage(message) {
    SendMessageWithChatId(selectChatId, message);
}

async function SendMessageWithChatId(chatId, message) {
    const responce = await fetch("/ChatApi/SendMessage", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            text: message,
            chatid: chatId
        })
    });
}

async function GetMessages(chatId, count, lastMessageId) {
    const responce = await fetch("/ChatApi/GetMessages", {
        method: "GET",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            ChatId: chatId,
            Count: count,
            LastMessageId: lastMessageId
        })
    });
}

async function EditMessage(messageId, newText) {
    const responce = await fetch("/ChatApi/EditMessage", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            MessageId: messageId,
            NewText: newText,
        })
    });
}

async function DeleteMessage(messageId, deleteForMe) {
    const responce = await fetch("/ChatApi/DeleteMessage", {
        method: "DELETE",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            MessageId: messageId,
            DeleteForMe: deleteForMe,
        })
    });
}


hubConnection.start();