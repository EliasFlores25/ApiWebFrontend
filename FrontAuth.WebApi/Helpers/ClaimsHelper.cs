using FrontAuth.WebApi.DTOs.UsuarioDTOs;
using System.Security.Claims;

namespace FrontAuth.WebApi.Helpers
{
    // Define una clase de ayuda estática para gestionar claims.
    public static class ClaimsHelper
    {
        // Método para construir una identidad de usuario a partir de sus datos de inicio de sesión.
        public static ClaimsPrincipal CrearClaimsPrincipal(LoginResponseDTO usuario)
        {
            // Se crea una lista de declaraciones (claims) del usuario.
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email,usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim("Token", usuario.Token)
            };
            // Crea una identidad de claims con las declaraciones y el esquema de autenticación.
            var identity = new ClaimsIdentity(claims, "AuthCookie");

            // Devuelve un objeto ClaimsPrincipal, que es el contenedor de la identidad.
            return new ClaimsPrincipal(identity);
        }
    }
}
