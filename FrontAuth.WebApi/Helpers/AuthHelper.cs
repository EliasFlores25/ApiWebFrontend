using System.Security.Claims;

namespace FrontAuth.WebApi.Helpers
{
    // Clase de utilidad para asistir en la gestión de la autenticación.
    public class AuthHelper
    {
        // Extrae el token de autenticación de los claims del usuario.
        public static string ObtenerToken(ClaimsPrincipal user)
        {
            return user.FindFirstValue("Token")?? string.Empty;
        }
    }
}