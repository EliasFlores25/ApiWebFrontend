using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FrontAuth.WebApi.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;// Cliente HTTP para realizar llamadas a la API.
        private readonly JsonSerializerOptions _jsonOptions; // Opciones para la serialización y deserialización de JSON.

        // Constructor que inyecta la dependencia del HttpClient.
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, WriteIndented = true };
        }
        // Método genérico para obtener una lista de objetos de tipo T desde un endpoint específico.
        public async Task<List<T>> GetAllAsync<T>(string endpoint, string token = null)
        {
            AddAuthorizationHeader(token);
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions);

        }
        // Método genérico para obtener un objeto de tipo T por su ID desde un endpoint específico.
        public async Task<T> GetByIdAsync<T>(string endpoint, int id, string token = null)
        {
            AddAuthorizationHeader(token);
            var response = await _httpClient.GetAsync($"{endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, _jsonOptions);

        }
        //POST genérico
        // Método genérico para enviar datos a un endpoint específico y recibir una respuesta de tipo TResponse.
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, string token = null)
        {
            AddAuthorizationHeader(token);
            var content = new StringContent(JsonSerializer.Serialize(data, _jsonOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, _jsonOptions);
        }
        //PUT genérico
        // Método genérico para actualizar datos en un endpoint específico y recibir una respuesta de tipo TResponse.
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, int id, TRequest data, string token = null)
        {
            AddAuthorizationHeader(token);
            var content = new StringContent(JsonSerializer.Serialize(data, _jsonOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{endpoint}/{id}", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(json, _jsonOptions);
        }
        //DELETE genérico
        // Método genérico para eliminar un recurso por su ID desde un endpoint específico.
        public async Task<bool> DeleteAsync(string endpoint, int id, string token = null)
        {
            AddAuthorizationHeader(token);
            var response = await _httpClient.DeleteAsync($"{endpoint}/{id}");
            return response.IsSuccessStatusCode;
        }
        //Agrega el encabezado de autorización con el token proporcionado.
        private void AddAuthorizationHeader(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}