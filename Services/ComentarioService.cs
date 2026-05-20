using System.Net.Http.Json;
using RecetArreWeb.DTOs;

namespace RecetArreWeb.Services
{
    public interface IComentarioService
    {
        Task<List<ComentarioDto>> ObtenerPorReceta(int recetaId);
        Task<bool> Crear(ComentarioCreacionDto dto);
        Task<bool> Eliminar(int id);
    }

    public class ComentarioService : IComentarioService
    {
        private readonly HttpClient httpClient;
        //readonly es para garantizar que la variable se puede adignar solo una vez
        private const string endpoint = "api/Comentarios";//aqui manda al endpoint del back

        public ComentarioService(HttpClient httpClient) => this.httpClient = httpClient;

        // aqui traemos solo los comentarios que pertenecen a una receta especifica
        public async Task<List<ComentarioDto>> ObtenerPorReceta(int recetaId)
        {
            try
            {
                // se conecta a la ruta especial de la api para esa receta
                return await httpClient.GetFromJsonAsync<List<ComentarioDto>>($"{endpoint}/receta/{recetaId}") ?? new();
            }
            catch { return new(); } // si falla regresa lista vacia para que no truene
        }

        // aqui guardamos un comentario nuevo
        public async Task<bool> Crear(ComentarioCreacionDto dto)
        {
            var res = await httpClient.PostAsJsonAsync(endpoint, dto);
            return res.IsSuccessStatusCode;
        }

        // aqui borramos el comentario por su id
        public async Task<bool> Eliminar(int id) => (await httpClient.DeleteAsync($"{endpoint}/{id}")).IsSuccessStatusCode;
    }
}