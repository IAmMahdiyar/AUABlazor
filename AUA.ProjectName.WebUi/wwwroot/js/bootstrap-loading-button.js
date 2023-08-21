let buttonClass = "submit"
let elements = "";

function startLoading() {
    init()

    for (element of elements) {
        let spans = element.getElementsByTagName("span")
        var spinner = spans[0]
        var loading = spans[1]
        var submit = spans[2]
        element.disabled = true;
        spinner.style.display = "inline-block";
        loading.style.display = "inline-block";
        submit.style.display = "none";
    }
}

function stopLoading() {
    init()

    for (element of elements) {
        let spans = element.getElementsByTagName("span")
        var spinner = spans[0]
        var loading = spans[1]
        var submit = spans[2]
        element.disabled = false;
        spinner.style.display = "none";
        loading.style.display = "none";
        submit.style.display = "block";
    }

}

function init() {
    elements = document.getElementsByClassName(buttonClass)
}
