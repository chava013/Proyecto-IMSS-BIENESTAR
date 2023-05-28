using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Clases;
using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Proyecto.Controllers
{
    public class UsuarioController : Controller
    {


        private readonly IHttpContextAccessor context;
        public UsuarioController(IHttpContextAccessor httpContextAccessor)
        {
            context = httpContextAccessor;
        }
        public IActionResult IndexUsuario()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult ValidarUsuario(UsuarioCLS oUsuarioCLS)
        {
            string u;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(oUsuarioCLS);
                }
                else
                {
                    using (Models.CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                    {
                        oUsuarioCLS = (from user in db.Usuarios
                                    where user.Nomusuario == oUsuarioCLS.nomusuario && user.Contraseña == oUsuarioCLS.contraseña
      
                                    select new UsuarioCLS
                                    {
                                        clvdoctor = user.Clvdoctor,
                                        nomusuario = user.Nomusuario,
                                        contraseña = user.Contraseña
                                    }).First();
                        u = oUsuarioCLS.nomusuario;
                        DoctorCLS oDoctorCLS = (from doc in db.Doctors
                                                where doc.Clvdoctor == oUsuarioCLS.clvdoctor
                                                select new DoctorCLS
                                                {
                                                    clvdoctor = doc.Clvdoctor,
                                                    clvespecialidad = doc.Clvespecialidad.ToString(),
                                                    clvturno = doc.Clvturno.ToString(),
                                                    nomdoctor = doc.Nomdoctor

                                                }
                            ).First();
                        context.HttpContext.Session.SetString("doctor", oDoctorCLS.nomdoctor);
                        context.HttpContext.Session.SetString("clvdoctor", oDoctorCLS.clvdoctor.ToString());
                    }
                }
            }
            catch (Exception)
            {
                context.HttpContext.Session.SetString("error", "1");
                return RedirectToAction("IndexUsuario", "Usuario");
            }
            if(u.Equals("CHAVA13"))
            {
                context.HttpContext.Session.SetString("sesion", "1");
            }
            else
            {
                context.HttpContext.Session.SetString("sesion", "2");
            }
          

            return RedirectToAction("Index", "Home");
        }

        public IActionResult EditarUsuario(int id)
        {
            UsuarioCLS oUsuarioCLS = new UsuarioCLS();
            using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
            {
                oUsuarioCLS = (from user in db.Usuarios
                             where user.Clvdoctor == id

                             select new UsuarioCLS
                             {
                                 clvdoctor = user.Clvdoctor,
                                 nomusuario = user.Nomusuario,
                                 contraseña = user.Contraseña
                             }).First();
            }


            return View(oUsuarioCLS);
        }
        [HttpPost]
        public IActionResult ActualizarUsuario(UsuarioCLS oUsuarioCLS)
        {

            try
            {

                using (CENTRODESALUDSRLContext db = new CENTRODESALUDSRLContext())
                {
                    if (!ModelState.IsValid)
                    {
                        return View("EditarUsuario", oUsuarioCLS);
                    }
                    else
                    {

                        Usuario user = db.Usuarios.Where(p => p.Clvdoctor == oUsuarioCLS.clvdoctor).First();
                        user.Nomusuario = oUsuarioCLS.nomusuario;
                        user.Contraseña = oUsuarioCLS.contraseña;
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                return View("EditarUsuario", oUsuarioCLS);

            }

            return RedirectToAction("Index","Home");
        }
    }
}
