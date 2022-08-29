const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

hubConnection.on("OnMessage", function (message) {
    AddMessage(message);
    AddMessageHtml(message)
});

let selectChatId = 0;

let chats = [
    {
        chatId: 0,
        messages: [
            {
                Id: 0,
                Text: "",
                ChatId: 0,
            }
        ]
    }
];

function AddMessage(message) {
    let chat = chats.find(f => f.chatId == message.chatId);
    if (chat == null) {
        chats.push({ chatId: message.chatId, messages: [] });
        chat = chats.find(f => f.chatId == message.chatId);
    }
    
    chat.messages.push(message);
}

function AddMessageHtml(message) {
    if (Array.isArray(message)) {
        for (var i = 0; i < message.length; i++) {
            AddMessageHtml(message[i]);
        }
        return;
    }

    if (message.chatId != selectChatId)
        return;

    let author = message.author.userName;
    let text = message.text;

    let userNameElem = document.createElement("b");
    userNameElem.appendChild(document.createTextNode(author + ": "));

    let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(text));

    document.getElementById("chat").insertAdjacentElement("beforeend", elem);
}

async function LoadChat(chatId) {
    ClearMessageHtml();

    selectChatId = chatId;

    let chat = chats.find(f => f.chatId == chatId);
    if (chat == null) {
        chats.push({ chatId: chatId, messages: [] });
        chat = chats.find(f => f.chatId == chatId);
        let messages = await GetMessages(chatId, 10, 2147483647);
        chat.messages.push(...messages)
    }

    AddMessageHtml(chat.messages);
}

function ClearMessageHtml() {
    var container = document.getElementById("chat");

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }
}

function SelectChat(chatId) {
    if (selectChatId == chatId)
        return;

    LoadChat(chatId);
}

async function SendMessage(message) {
    SendMessageWithChatId(selectChatId, message);
}

async function SendMessageWithChatId(chatId, message) {
    const response = await fetch("/ChatApi/SendMessage", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            text: message,
            chatid: chatId
        })
    });
}

async function GetMessages(chatId, count, lastMessageId) {
    const response = await fetch(`/ChatApi/GetMessages?ChatId=${chatId}&Count=${count}&LastMessageId=${lastMessageId}`, {
        method: "GET",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    const data = await response.json();

    if (response.ok === true) {
        return data;
    }
    else {
        console.log("Error: ", response.status, data.errorText);
        return [];
    }
}

async function EditMessage(messageId, newText) {
    const response = await fetch("/ChatApi/EditMessage", {
        method: "PUT",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            MessageId: messageId,
            NewText: newText,
        })
    });
}

async function DeleteMessage(messageId, deleteForMe) {
    const response = await fetch("/ChatApi/DeleteMessage", {
        method: "DELETE",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            MessageId: messageId,
            DeleteForMe: deleteForMe,
        })
    });
}


hubConnection.start();