
function succussMessage(message) {
    Swal.fire(
        'Success!',
        message,
        'success'
    )
}

function errorMessage(message) {
    Swal.fire(
        'Error!',
        message,
        'error'
    )
}

function showDetails(details) {

    var message = '<div style="text-align: left; line-height: 30px;">'

    for (const [key, value] of Object.entries(details)) {
        message += "<b>" + key + "</b>" + ": " + value + "<br/>";
    }

    message += "</div>"

    Swal.fire({
        title: 'Details',
        html: message,
        icon: 'success'
    })
}

function signOut() {
    window.location.replace("/logout")
}

function confirmDelete(dotnetHelper) {
    var spinner = '<div style="vertical-align: middle; " class="spinner-border text-primary ml-2" role="status"></div><br/><br/>'
    Swal.fire({
        title: 'Do you want to delete the record?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, Delete it',
        cancelButtonText: 'No'
    }).then((result) => {
        dotnetHelper.invokeMethodAsync("CheckConfirmAsync", result.isConfirmed)
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Deleting!',
                icon: 'warning',
                html: "<span> Please wait, This will be done in a moment </span> " + spinner,
                showCancelButton: false,
                showConfirmButton: false,
                allowEscapeKey: false,
                allowOutsideClick: false,
            })
        }
    })
}

function confirmUpdate(dotnetHelper) {
    var spinner = '<div style="vertical-align: middle; " class="spinner-border text-primary ml-2" role="status"></div><br/><br/>'
    Swal.fire({
        title: 'Do you want to Update the record?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, Update it',
        cancelButtonText: 'No'
    }).then((result) => {
        dotnetHelper.invokeMethodAsync("CheckConfirmAsync", result.isConfirmed)
        if (result.isConfirmed) {
            Swal.fire({
                title: 'Updating!',
                icon: 'warning',
                html: "<span> Please wait, This will be done in a moment </span> " + spinner,
                showCancelButton: false,
                showConfirmButton: false,
                allowEscapeKey: false,
                allowOutsideClick: false,
            })
        }
    })
}