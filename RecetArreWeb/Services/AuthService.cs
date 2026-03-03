using RecetArreWeb.DTOs;
using System.Net.Http.Json;

namespace RecetArreWeb.Services
{

    public interface IAuthService
    {
        Task<RespuestaAutenticacion?> Login(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion?> Registrar(CredencialesUsuario credencialesUsuario);
        Task<RespuestaAutenticacion?> RenovarToken();
        Task Logout();
    }
    public class AuthService : IAuthService
    {
       private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;
        private const string endpoint = "api/Cuentas";

        public AuthService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<RespuestaAutenticacion?> Login(CredencialesUsuario credencialesUsuario)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{endpoint}/Login", credencialesUsuario);
                if (response.IsSuccessStatusCode)
                {
                    var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();
                    if (respuesta != null)
                    {
                        await tokenService.GuardarToken(respuesta.Token, respuesta.Expiracion);
                        return respuesta;
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error en login: {error}");
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al hacer login: {ex.Message}" );
                return null;
            }
        }
        
        public async Task<RespuestaAutenticacion?> Registrar(CredencialesUsuario credencialesUsuario)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"{endpoint}/Registrar", credencialesUsuario);
                if (response.IsSuccessStatusCode)
                {
                   var respuesta = await response.Content.ReadFromJsonAsync<RespuestaAutenticacion>();
                    if (respuesta != null)
                    {
                        await tokenService.GuardarToken(respuesta.Token, respuesta.Expiracion);
                        return respuesta;
                    }
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error en registro: {error}");

                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar: {ex.Message}");
                return null;
            }

        }

        Task IAuthService.Logout()
        {
            throw new NotImplementedException();
        }

        Task<RespuestaAutenticacion?> IAuthService.Registrar(CredencialesUsuario credencialesUsuario)
        {
            throw new NotImplementedException();
        }

        Task<RespuestaAutenticacion?> IAuthService.RenovarToken()
        {
            throw new NotImplementedException();
        }
    }
}
