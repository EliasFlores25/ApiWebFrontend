using FrontAuth.WebApi.DTOs.UsuarioDTOs;
using FrontAuth.WebApi.Helpers;
using FrontAuth.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FrontAuth.WebApi.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApiService _apiService;
        public UsuarioController(ApiService apiService) { _apiService = apiService; }

        //Get: Index
        // Muestra la vista de índice con una lista de usuarios.
        public async Task<IActionResult> Index()
        {
            // Obtiene el token de autenticación del usuario actual utilizando la clase de ayuda.
            var token = AuthHelper.ObtenerToken(User); // Obtener el token JWT del usuario autenticado desde los claims

            var usuarios = await _apiService.GetAllAsync<UsuarioDTO>("User/usuarios", token);
            return View(usuarios);
        }
    }
}