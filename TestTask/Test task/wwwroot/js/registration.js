function GetPageRegister() {

    itemForm = document.createElement('form');
    itemForm.name = 'PageRegister';
    itemForm.setAttribute('method', "post");

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

    itemInputConfirmPassword = document.createElement('input'),
        itemLabelConfirmPassword = document.createElement('label');

    itemInputConfirmPassword.name = 'confirmPassword';
    itemLabelConfirmPassword.htmlFor = 'confirmPassword';
    itemLabelConfirmPassword.innerHTML = ' Подтвердите пароль: ';

    itemForm.appendChild(itemLabelConfirmPassword);
    itemForm.appendChild(itemInputConfirmPassword);

    itemButton = document.createElement('button');
    itemButton.id = 'submit';
    itemButton.innerHTML = 'Зарегистрироваться';
    itemButton.type = 'submit';

    itemForm.appendChild(itemButton);

    document.getElementById("myForm").innerHTML = "";
    document.getElementById("myForm").appendChild(itemForm);

    document.forms["PageRegister"].addEventListener("submit", e => {
        e.preventDefault();

        document.getElementById("errors").innerHTML = "";

        const form = document.forms["PageRegister"];
        const name = form.elements["name"].value;
        const surname = form.elements["surname"].value;
        const mail = form.elements["mail"].value;
        const password = form.elements["password"].value;
        const confirmPassword = form.elements["confirmPassword"].value;

        Registration(name, surname, mail, password, confirmPassword);
    });
}

async function Registration(userName, userSurname, userMail, userPassword, userConfirmPassword) {

    const response = await fetch("api/accounts/register", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            name: userName,
            surname: userSurname,
            mail: userMail,
            password: userPassword,
            confirmPassword: userConfirmPassword,
        })
    });
    if (!response.ok) {
        const errorData = await response.json();
        if (errorData) {

            if (errorData["Registration"]) {
                addError(errorData["Registration"]);
            }

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
                if (errorData.errors["PasswordConfirm"]) {
                    addError(errorData.errors["PasswordConfirm"]);
                }
            }
        }
    }
}