using System.Net.Http.Json;
using RecetArreWeb.DTOs;

namespace RecetArreWeb.Services
{
    public interface IIngredienteService
    {
        Task<List<IngredienteDto>> ObtenerTodos();
        Task<IngredienteDto?> ObtenerPorId(int id);
        Task<IngredienteDto?> Crear(IngredienteCreacionDto dto);
        Task<bool> Actualizar(int id, IngredienteModificacionDto dto);
        Task<bool> Eliminar(int id);
    }

    public class IngredienteService : IIngredienteService
    {
        private readonly HttpClient httpClient; // aqui se guarda el cliente para hacer peticiones
        private const string endpoint = "api/Ingredientes"; // la ruta de la api para ingredientes

        public IngredienteService(HttpClient httpClient) => this.httpClient = httpClient;

        // esta funcion trae todos los ingredientes de la base de datos
        public async Task<List<IngredienteDto>> ObtenerTodos()
        {
            // se conecta a la api y si no hay nada pues manda una lista vacia
            return await httpClient.GetFromJsonAsync<List<IngredienteDto>>(endpoint) ?? new();
        }

        // aqui buscamos un solo ingrediente usando su id
        public async Task<IngredienteDto?> ObtenerPorId(int id) =>
            await httpClient.GetFromJsonAsync<IngredienteDto>($"{endpoint}/{id}");

        // aqui se mandan los datos para guardar un ingrediente nuevo
        public async Task<IngredienteDto?> Crear(IngredienteCreacionDto dto)
        {
            var res = await httpClient.PostAsJsonAsync(endpoint, dto);
            // regresa el ingrediente creado
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<IngredienteDto>() : null;
        }

        // esta sirve para cambiarle los datos a un ingrediente que ya existe
        public async Task<bool> Actualizar(int id, IngredienteModificacionDto dto)
        {
            var res = await httpClient.PutAsJsonAsync($"{endpoint}/{id}", dto);
            return res.IsSuccessStatusCode; // nos dice si se pudo o no
        }

        // esta es para borrar el ingrediente de la lista
        public async Task<bool> Eliminar(int id) => (await httpClient.DeleteAsync($"{endpoint}/{id}")).IsSuccessStatusCode;
    }
}