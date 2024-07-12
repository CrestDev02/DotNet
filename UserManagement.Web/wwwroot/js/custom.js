$(document).ready(function () {
    
});

function deleteUser(id) {
    if (id > 0) {
        $.ajax({
            type: "DELETE",
            url: "/User/Delete?id=" + id,
            dataType: "json",
            encode: true,
            success: function (data) {
                console.log(data);
                if (Boolean(data.error) == true) {
                    var errorMessage = $('#errorMessage');
                    errorMessage.removeClass('d-none');
                    errorMessage.html(data.message);
                    setTimeout(function () {
                        errorMessage.addClass('d-none');
                    }, 2000);
                }
                else {
                    var successMessage = $('#successMessage');
                    successMessage.removeClass('d-none');
                    successMessage.html(data.message);
                    setTimeout(function () {
                        successMessage.addClass('d-none');
                        location.reload();
                    }, 2000);
                }
            },
            complete: function (data) {

            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.log(xhr.status);
                console.log(xhr.responseText);
            }

        }).done(function (data) {

        });
    }
}