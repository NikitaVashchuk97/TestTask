async function GetLanguages() {
    const response = await fetch("/api/languages", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok) {
        const languages = await response.json();
        var list = document.getElementById('select'),
            item = document.createElement('option');
        languages.forEach(language => {
            item.innerHTML = language;
            item.value = language;
            list.appendChild(item.cloneNode(true));
        });
    }
}
