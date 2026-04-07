/*Калькулятор*/
function calc(){
    let input1 = Number(document.getElementById("input1").value);
    let input2 = Number(document.getElementById("input2").value);
    let op = document.getElementById("op").value;
    let result;
    switch (op) {
        case "+":
            result = input1 + input2;
            break;
        case "-": 
        result = input1 - input2; 
            break;
        case "*": 
            result = input1 * input2;
            break;
        case "/": 
            result = input2 !== 0 ? input1 / input2 : "Ошибка";
            break;
        default:
            result = "Неизвестная операция";
    }
    document.getElementById("result").innerText = "Результат: " + result;
}

function greet() {
    let now = new Date();
    let hours = now.getHours();
    let message;
    if (hours < 12) {
        message = "Доброе утро!";
    }
    else if (hours < 18) {
        message = "Добрый день!";
    }
    else {
        message = "Добрый вечер!";
    }

    document.getElementById("message").innerText = message;
}
window.onload = greet;

let rates = {
    usd: 1,
    eur: 0.9,
    rub: 90,
    cny: 7,
    jpy: 150
};
function convert() {
    let amount = Number(document.getElementById("amount").value);
    let from = document.getElementById("from").value;
    let to = document.getElementById("to").value;
    conversionResult = amount / rates[from] * rates[to];
    document.getElementById("conversionResult").innerText =
        "Результат: " + conversionResult.toFixed(2);

    if (document.getElementById("from").value === document.getElementById("to").value) {
        document.getElementById("conversionResult").innerText = "Вы выбрали одинаковые валюты!";
    }
}

let visits = localStorage.getItem("visits");
if (visits === null) {
    visits = 0;
}
visits++;
localStorage.setItem("visits", visits);
document.getElementById("visitCount").innerText = "Посещений: " + visits;

let startTime = Date.now();
window.onbeforeunload = function() {
    let endTime = Date.now();
    let timespent = (endTime - startTime) / 1000;
    let totalTime = localStorage.getItem("totalTime");
    if (totalTime === null) {
        totalTime = 0;
    }
    totalTime = Number(totalTime) + timespent;
    localStorage.setItem("totalTime", totalTime);
}
let totalTime = localStorage.getItem("totalTime") || 0;
let avgTime = totalTime / visits;
document.getElementById("avgTime").innerText = "Среднее время: " + avgTime.toFixed(2) + " сек";

function submitForm() {
    let fio = document.getElementById("fio").value;
    let phone = document.getElementById("phone").value;
    let inputDate = document.getElementById("date").valueAsDate;
    let question = document.getElementById("question").value;
    let fioRegex = /^[А-Яа-яA-Za-z\s]+$/;
    if (!fioRegex.test(fio)) {
        alert("ФИО должно содержать только буквы!");
        return;
    }
    let phoneRegex = /^\+?\d{10,12}$/;
    if (!phoneRegex.test(phone)) {
        alert("Неверный телефон!");
        return;
    }
    let today = new Date();
    today.setHours(0, 0, 0, 0);
    let userTime = new Date(inputDate);
    userTime.setHours(0, 0, 0, 0);
    if (userTime < today) {
        alert("Дата не может быть в прошлом!");
        return;
    }
    else if (userTime > today) {
        alert("Дата не может быть в будущем!");
        return;
    }
    if (question === "") {
        alert("Введите вопрос!");
        return;
    }
    let parts = fio.split(" ");
    if (parts.length === 3) {
        let [surname, name, patronymic] = parts;

        document.getElementById("fioOutput").innerText =
            "Имя: " + name +
            ", Фамилия: " + surname +
            ", Отчество: " + patronymic;
    }
    openModal()
}
document.getElementById("photo").addEventListener("change", function(event) {
    let img = document.getElementById("preview");
    img.src = URL.createObjectURL(event.target.files[0]);
});
function openModal() {
    document.getElementById("modal").style.display = "block";
}

function closeModal() {
    document.getElementById("modal").style.display = "none";
}
