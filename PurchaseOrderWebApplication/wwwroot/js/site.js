// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
(
    function confirmupdateOrderAmount(event) {
        event.preventDefault();
        Swal.fire({
            title: "Do you want to update the changes?",
            showDenyButton: true,
            showCancelButton: true,
            confirmButtonText: "Update",
            denyButtonText: `Don't update`
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                const deleteForm = document.getElementById('updateOrderAmount');
                deleteForm.submit();
                Swal.fire("Updated!", "", "success");
            } else if (result.isDenied) {
                Swal.fire("Changes are not update", "", "info");
            }
        });
    },
    function confirmupdateOrderAddress(event) {
    event.preventDefault();
    Swal.fire({
        title: "Do you want to update the changes?",
        showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Update",
        denyButtonText: `Don't update`
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            const deleteForm = document.getElementById('updateOrderAddress');
            deleteForm.submit();
            Swal.fire("Updated!", "", "success");
        } else if (result.isDenied) {
            Swal.fire("Changes are not update", "", "info");
        }
    });
}
);