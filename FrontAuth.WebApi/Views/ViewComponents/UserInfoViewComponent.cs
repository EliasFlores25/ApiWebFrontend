using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FrontAuth.WebApi.Views.ViewComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        // Este método es invocado automáticamente para construir la vista del componente.
        public IViewComponentResult Invoke()
        {
            var user = HttpContext.User;
            if (user.Identity != null && user.Identity.IsAuthenticated)
            {

                var nombre = user.FindFirstValue(ClaimTypes.Name);
                var rol = user.FindFirstValue(ClaimTypes.Role);
                ViewBag.Nombre = nombre;
                ViewBag.Rol = rol;
            }
            else
            {
                ViewBag.Nombre = "Invitado";
                ViewBag.Rol = "";
            }
            return View();
        }
    }
}