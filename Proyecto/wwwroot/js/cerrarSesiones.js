function CerrarSesiones(){
  Swal.fire({
      position: 'center',
      html:
      '<header style="background:#34B5AA;"><br></header>'+
      '<body>'+
          '<div class="container">'+
              '<div class="text-center" >'+
                  'style="width: 200px;" alt="logo">'+
                  '<div style="padding-top:30px">'+
                      '<button  disabled style="box-sizing: border-box;  border-color: #b44593;border: 1px solid rgba(0, 0, 0, 0.2);'+
                          'border-radius: 25px;font-style: normal;font-weight: 400;font-size: 20px;line-height: 24px;text-align: center;color: #525252;padding: 15px;">Configuración de tu cuenta'+
                      '</button>'+
                  '</div>'+
              '</div>'+
              '<div>'+
  
              '<div class="d-flex flex-direction: row justify-content-center" style="margin:25px">'+
                  '<div style="border-top: grey 1px solid;border-bottom: grey 1px solid; padding:15px">'+
                      '<button onClick="usu()" style="border:none;margin:15px; padding:5px;">'+
                          '<i class="bi bi-person-fill" style="padding-left:40px;padding-right:20px"></i>'+
                          '<span style="font-style: normal;font-weight: 600;font-size: 19px;line-height: 23px;border:none;padding-right:40px;color: #272727;">Cambiar Nombre</span>'+
                      '</button>'+
  
                      '<button onClick="pawd()"style="border:none;margin:15px; padding:5px">'+
                          '<i class="bi bi-lock-fill" style="padding-left:40px;padding-right:20px"></i>'+
                          '<span style="font-style: normal;font-weight: 600;font-size: 19px;line-height: 23px;border:none;padding-right:40px;color: #272727;">Cambiar Contraseña</span>'+
                      '</button>'+
                  '</div>'+
                  '<button onClick="confirm()" style=" border:none;margin:15px; padding:10px;background: linear-gradient(0deg, rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.2)), #7DBC54;border-radius: 5px;">'+
                      '<span style="font-style:normal;font-weight: 700;font-size: 20px;line-height: 24px;text-align: center;color: #FFFBFB;">Cerrar Sesión</span>'+
                  '</button>'+
              '</div>'+
          '</div>'+
      '</body>'+
      '<footer style="background:#34B5AA"><br></footer>',
      allowOutsideClick:true,
      allowEscapeKey:false,
      alloeWnterKey:false,
      showConfirmButton: false,
      showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }
  
    })
}
  async function confirm(){
      Swal.fire(window.location = "/")
  }
async function pawd (){
  const { value: password } = await Swal.fire({
    title: 'Nueva Contraseña',
    input: 'password',
    inputLabel: 'Password',
    inputPlaceholder: 'Escribe tu nueva contraseña',
    inputAttributes: {
      maxlength: 10,
      autocapitalize: 'off',
      autocorrect: 'off'
    }
  })
  if (password) {
    Swal.fire(`Contraseña Actualizada`)
  }
}

async function usu (){
  const { value: text } = await Swal.fire({
    input: 'text',
    inputLabel: 'Nombre de Usuario',
    inputPlaceholder: 'ingresa tu nuevo nombres de usuario',
    inputAttributes: {
      'aria-label': 'Type your message here'
    },
    showCancelButton: true
  })
  
  if (text) {
    Swal.fire(`Usuario Actualizado`)
  }
}