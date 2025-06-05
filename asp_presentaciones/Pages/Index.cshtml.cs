using lib_dominio.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lib_presentaciones.Interfaces;

namespace asp_presentacion.Pages
{
    public class IndexModel : PageModel
    {
        // Estado de sesión del usuario
        public bool EstaLogueado = false;

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public string? Contrasena { get; set; }

        [BindProperty]
        public string RespuestaHumana { get; set; }

        // Mensaje de error para mostrar en pantalla
        public string MensajeError { get; set; } = string.Empty;

        private readonly IUsuariosPresentacion? iUsuariosPresentacion;

        public IndexModel(IUsuariosPresentacion iUsuariosPresentacion)
        {
            try
            {
                this.iUsuariosPresentacion = iUsuariosPresentacion;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnGet()
        {
            var variable_session = HttpContext.Session.GetString("Usuario");
            if (!string.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;
                return;
            }
        }

        // Acción para limpiar los campos del formulario
        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty;
                Contrasena = string.Empty;
                MensajeError = string.Empty;
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtEnter()
        {
            try
            {
                
                if (RespuestaHumana?.Trim() != "13")
                {
                    MensajeError = "Verificación fallida. Responde correctamente la pregunta: ¿Cuánto es 3 + 2?";
                    return;
                }

                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Contrasena))
                {
                    MensajeError = "Debes ingresar el email y la contraseña.";
                    return;
                }

                var task = iUsuariosPresentacion!.PorEmail(new lib_dominio.Entidades.Usuarios()
                {
                    Email = Email
                });
                task.Wait();
                var usuario = task.Result.FirstOrDefault();

                if (usuario == null || usuario.Contraseña != Contrasena)
                {
                    MensajeError = "Usuario o contraseña incorrectos.  Vuelva a intentar :)";
                    return;
                }

                ViewData["Logged"] = true;
                HttpContext.Session.SetString("Usuario", usuario.Id.ToString());
                HttpContext.Session.SetString("Rol", usuario.Roles!.Nombre!);
                EstaLogueado = true;

                OnPostBtClean(); // limpio los campos y mensaje
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        // Acción que se ejecuta al presionar el botón "Cerrar sesión"
        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/");
                EstaLogueado = false;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
