using Microsoft.JSInterop;

namespace RecetArreWeb.Services
{
    public interface ITokenService
    {
        Task GuardarToken(string token, DateTime expiracion);
        Task<string?> ObtenerToken();
        Task<DateTime?> ObtenerExpiracion();
        Task<bool> EstaAutenticado();
        Task EliminarToken();
    }


    public class TokenService : ITokenService
    {

        private readonly IJSRuntime jsRuntime; //
        private const string TOKEN_KEY = "autToken";
        private const string EXPIRACION_KEY = "tokenExpiracion";

        public TokenService(IJSRuntime jsRuntime)
        {
            this.jsRuntime= jsRuntime;
        }

        public async Task EliminarToken()
        {
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem",TOKEN_KEY);
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", EXPIRACION_KEY);

        }
      

        public async Task<bool> EstaAutenticado()
        {
            var token = await ObtenerToken();
            return !string.IsNullOrEmpty(token);
        }

       public async Task GuardarToken(string token, DateTime expiracion)
        {
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, token);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", EXPIRACION_KEY, expiracion.ToString("o"));
            //Formatp ISO 8601
        }

        public async Task<DateTime?> ObtenerExpiracion()
        {
            try
            {
                var expiracionStr = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", EXPIRACION_KEY);
                if (string.IsNullOrEmpty(expiracionStr))
                    return null;

                if (DateTime.TryParse(expiracionStr, out var expiracion))
                    return expiracion;
            }
            catch
            {
                return null;
            }
            return null;
        }

        
        public async Task<string?> ObtenerToken()
        {
            try
            {
                //1. leer el token de local storge
                var token = await jsRuntime.InvokeAsync<string?>("localStorage.getItem", TOKEN_KEY);

                //2. Si no hay token, devolver null
                if (string.IsNullOrEmpty(token))
                    return null;

                //3. Verificar si el token expiro
                var expiracion = await ObtenerExpiracion();
                if(expiracion.HasValue && expiracion.Value < DateTime.UtcNow)
                {
                    //Token expirado, eliminar

                    await EliminarToken();
                    return null;
                }
                return token;

            }
            catch
            {
                return null;
            }
        }

    }
}
