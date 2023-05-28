
function mostrarModal(titulo = "¿Desea guardar la información?",
                    texto = "Verifique que la información es correcta antes de guardar") {
    return Swal.fire({
        title:titulo,
        text:texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Guardar'
     
   
    })

}