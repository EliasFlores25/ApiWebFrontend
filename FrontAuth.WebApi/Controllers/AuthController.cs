using FrontAuth.WebApi.DTOs.UsuarioDTOs;
using FrontAuth.WebApi.Helpers;
using FrontAuth.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FrontAuth.WebApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService) { _authService = authService; }

        //Get: mostrar login
        // Muestra el formulario de inicio de sesión.
        [HttpPost]
        public IActionResult Login() { return View(); }

        //Post: Login
        // Procesa las credenciales de un usuario para iniciar sesión y lo redirige si es exitoso.
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View();
            }
            //Crear y firmar los claims usando el helper
            var principal = ClaimsHelper.CrearClaimsPrincipal(result);
            await HttpContext.SignInAsync("AuthCookie", principal);
            return RedirectToAction("Index", "Home");
        }
        //Post: registro
        // Registra un nuevo usuario y, si es exitoso, inicia sesión y lo redirige.
        [HttpPost]
        public async Task<IActionResult> Registrar (UsuarioRegistroDTO dto)
        {
            var result = await _authService.RegistrarAsync(dto);
            if (result == null || result.Id<=0)
            {
                ViewBag.Error = "Erro al registrar";
                return View();
            }
            //Crear y firmar los claims usando el helper
            var principal = ClaimsHelper.CrearClaimsPrincipal(result);
            await HttpContext.SignInAsync("AuthCookie", principal);
            return RedirectToAction("Index", "Home");
        }
        //Post: logout
        // Cierra la sesión del usuario actual y lo redirige a la página de login.
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AuthCookie");
            return RedirectToAction("Login");
        }
        //Post: mostrar registro
        // Muestra el formulario de registro de un nuevo usuario.
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }
    }
}
