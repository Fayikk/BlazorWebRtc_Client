
toastr.options = {
    "closeButton": true,
    "progressBar": true,
    "positionClass": "toast-bottom-right",
    "timeOut": "3000"
};
window.toastrNotify = (type, message, title) => {
    console.log("trigger toastrNotify")
    if (type === "success") {
        toastr.success(message, title);
    } else if (type === "error") {
        toastr.error(message, title);
    } else if (type === "info") {
        toastr.info(message, title);
    } else if (type === "warning") {
        toastr.warning(message, title);
    }
};
