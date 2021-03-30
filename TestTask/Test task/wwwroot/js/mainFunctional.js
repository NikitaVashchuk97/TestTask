function GetPageUsers() {

    itemForm = document.createElement('form');
    itemForm.name = 'pageUsers';
    itemForm.setAttribute('method', "post");

    itemInputId = document.createElement('input');

    itemInputId.name = 'id';
    itemInputId.type = 'hidden';
    itemInputId.value = '0';

    itemForm.appendChild(itemInputId);

    itemInputMail = document.createElement('input'),
        itemLabelMail = document.createElement('label');

    itemInputMail.name = 'mail';
    itemLabelMail.htmlFor = 'mail';
    itemLabelMail.innerHTML = 'Почта: ';

    itemForm.appendChild(itemLabelMail);
    itemForm.appendChild(itemInputMail);

    itemInputPassword = document.createElement('input');
    itemLabelPassword = document.createElement('label');

    itemInputPassword.name = 'password';
    itemLabelPassword.htmlFor = 'password';
    itemLabelPassword.innerHTML = ' Пароль: ';

    itemForm.appendChild(itemLabelPassword);
    itemForm.appendChild(itemInputPassword);


    itemInputName = document.createElement('input'),
        itemLabelName = document.createElement('label');

    itemInputName.name = 'name';
    itemLabelName.htmlFor = 'name';
    itemLabelName.innerHTML = ' Имя: ';

    itemForm.appendChild(itemLabelName);
    itemForm.appendChild(itemInputName);

    itemInputSurname = document.createElement('input'),
        itemLabelSurname = document.createElement('label');

    itemInputSurname.name = 'surname';
    itemLabelSurname.htmlFor = 'surname';
    itemLabelSurname.innerHTML = ' Фамилия: ';

    itemForm.appendChild(itemLabelSurname);
    itemForm.appendChild(itemInputSurname);

    itemButton = document.createElement('button');
    itemButton.id = 'submit';
    itemButton.innerHTML = 'Добавить';
    itemButton.type = 'submit';

    itemForm.appendChild(itemButton);

    document.getElementById("myForm").innerHTML = "";
    document.getElementById("myForm").appendChild(itemForm);

    document.forms["pageUsers"].addEventListener("submit", e => {
        e.preventDefault();

        document.getElementById("errors").innerHTML = "";


        const form = document.forms["pageUsers"];
        const id = form.elements["id"].value;
        const name = form.elements["name"].value;
        const surname = form.elements["surname"].value;
        const mail = form.elements["mail"].value;
        const password = form.elements["password"].value;

        const language = document.getElementById("select").value;

        if (id == 0)
            CreateUser(name, surname, mail, password, language);
        else
            EditUser(id, name, surname, mail, password, language);

        form.reset();
        form.elements["id"].value = 0;
    });

    GetUsers();
}

async function GetUsers() {
    const response = await fetch("/api/users/getAll", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok) {
        const users = await response.json();
        let rows = document.querySelector("tbody");
        rows.innerHTML = "";
        users.forEach(user => {
            rows.append(row(user));
        });
    }
    else {
        if (response.status == 404) {
            alert('Пройдите авторизацию');
        }
    }
}

async function CreateUser(userName, userSurname, userMail, userPassword, userLanguage) {

    const response = await fetch("api/users/create", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: userName,
            surname: userSurname,
            mail: userMail,
            password: userPassword,
            language: userLanguage
        })
    });
    if (response.ok === true) {
        const user = await response.json();
        document.querySelector("tbody").append(row(user));
    }
    else {
        const errorData = await response.json();
        if (errorData) {

            //if (errorData["Name"]) {
            //    addError(errorData["Name"]);
            //}

            if (errorData.errors) {
                if (errorData.errors["Name"]) {
                    addError(errorData.errors["Name"]);
                }
                if (errorData.errors["Surname"]) {
                    addError(errorData.errors["Surname"]);
                }
                if (errorData.errors["Mail"]) {
                    addError(errorData.errors["Mail"]);
                }
                if (errorData.errors["Password"]) {
                    addError(errorData.errors["Password"]);
                }
            }
        }
    }
}

//async function SetLanguages() {
//    const userLanguage = document.getElementById("select").value;

//    await fetch("api/users/setCulture", {
//        method: "POST",
//        headers: { "Accept": "application/json", "Content-Type": "application/json" },
//        body: JSON.stringify(userLanguage)
//    });
//}

function addError(errors) {
    errors.forEach(error => {
        const p = document.createElement("p");
        p.append(error);
        document.getElementById("errors").append(p);
    });
}

function row(user) {

    const tr = document.createElement("tr");

    const avatarTd = document.createElement("td");
    const avatarImg = document.createElement("img");
    avatarImg.src = "Files\\" + user.mail + ".png";
    avatarImg.width = 40;
    avatarImg.height = 40;
    avatarTd.append(avatarImg);
    tr.append(avatarTd);

    const idTd = document.createElement("td");
    idTd.append(user.id);
    tr.append(idTd);

    const nameTd = document.createElement("td");
    nameTd.append(user.name);
    tr.append(nameTd);

    const surnameTd = document.createElement("td");
    surnameTd.append(user.surname);
    tr.append(surnameTd);

    const mailTd = document.createElement("td");
    mailTd.append(user.mail);
    tr.append(mailTd);

    const passwordTd = document.createElement("td");
    passwordTd.append(user.password);
    tr.append(passwordTd);

    const languageTd = document.createElement("td");
    languageTd.append(user.language);
    tr.append(languageTd);

    const checkboxTd = document.createElement("td");
    const check = document.createElement("input");
    check.id = user.id;
    check.type = "checkbox";
    check.checked = user.isAdmin;
    check.addEventListener("click", e => {
        if (!SetAdminRole(check.id)) {
            e.preventDefault();
        }
    });
    checkboxTd.appendChild(check);
    tr.appendChild(checkboxTd);


    const linksTd = document.createElement("td");
    const editLink = document.createElement("a");
    editLink.id = user.id;
    editLink.setAttribute("style", "cursor:pointer;padding:15px;color:blue;");
    editLink.append("Изменить");
    editLink.addEventListener("click", e => {

        e.preventDefault();

        const id = editLink.id;
        const form = document.forms["pageUsers"];
        form.elements["name"].value = id;

        GetUser(id);

    });
    linksTd.append(editLink);
    tr.appendChild(linksTd);

    return tr;
}