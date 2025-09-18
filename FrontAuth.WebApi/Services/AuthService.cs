using FrontAuth.WebApi.DTOs.UsuarioDTOs;

namespace FrontAuth.WebApi.Services
{
    public class AuthService
    {
        // Declara una dependencia para realizar llamadas a la API.
        private readonly ApiService _apiService;

        // Inyecta el servicio de API a través del constructor.
        public AuthService(ApiService apiService) { _apiService = apiService; }

        // Método para manejar el inicio de sesión de un usuario.
        public async Task<LoginResponseDTO> LoginAsync(UsuarioLoginDTO dto)
        {
            return await _apiService.PostAsync<UsuarioLoginDTO, LoginResponseDTO>("auth/login", dto);
        }

        // Método para manejar el registro de un nuevo usuario.
        public async Task<LoginResponseDTO> RegistrarAsync(UsuarioRegistroDTO dto)
        {
            return await _apiService.PostAsync<UsuarioRegistroDTO, LoginResponseDTO>("auth/registrar", dto);
        }
    }
}