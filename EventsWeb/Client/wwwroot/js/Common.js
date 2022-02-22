"use strict";

/*Toastr Js*/

window.toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "3000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

window.showToastr = (type, message) => {

    if (type == "success") {
       
        toastr.success(message, 'Operation Successful')
    }

    if (type == "error") {

        toastr.error(message, 'Operation Failed')
    }
}

/*sweetAlert*/
window.showSweetAlert = () => {
    return new Promise((resolve) =>
    {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                resolve(true);
            }
            else {
                resolve(false);
            }
        })
    });
}