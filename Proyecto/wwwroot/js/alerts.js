function submitForm(form) {
    Swal.fire({
        title: "Guardar",
        text: "¿Deseas guardar los cambios?",
        icon: "question",
        showDenyButton: true,
        denyButtonText: "Cancelar",
        confirmButtonText: 'Guardar',
        buttons: true,
        dangerMode: true,
    })
        .then(function (result) {
            if (result.isConfirmed) {
                form.submit();
              
            }
            else if (result.isDenied) {
                Swal.fire('Ups', 'Los cambios no fueron realizados', 'info')

            }
        });
    return false;

}
function mostrarModal(titulo = "¿Seguro que quiere borrar la información?",
    texto = "No podrá recuperar el registro una vez acepte") {
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar',
        denyButtonText: 'Cancelar'


    })

}

function Mensaje() {
    Swal.fire({
        title: "Guardar",
        text: "¿Deseas guardar los cambios?",
        icon: "question",
        showDenyButton: true,
        denyButtonText: "Cancelar",
        confirmButtonText: 'Guardar',
        buttons: true,
        dangerMode: true,
    })

}