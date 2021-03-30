function GetPageLogin() {

    itemForm = document.createElement('form');
    itemForm.name = 'PageLogin';
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

    itemButton = document.createElement('button');
    itemButton.id = 'submit';
    itemButton.innerHTML = 'Войти';
    itemButton.type = 'submit';

    itemForm.appendChild(itemButton);

    document.getElementById("myForm").innerHTML = "";
    document.getElementById("myForm").appendChild(itemForm);

    document.forms["PageLogin"].addEventListener("submit", e => {
        e.preventDefault();

        document.getElementById("errors").innerHTML = "";

        const form = document.forms["PageLogin"];
        const mail = form.elements["mail"].value;
        const password = form.elements["password"].value;

        Login(mail, password);
    });
}

async function Login(userMail, userPassword) {

    const response = await fetch("api/accounts/login", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            mail: userMail,
            password: userPassword
        })
    });
    if (!response.ok) {
        //alert(await response.status)
        const errorData = await response.json();
        if (errorData) {

            if (errorData["Login"]) {
                addError(errorData["Login"]);
            }

            if (errorData.errors) {
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