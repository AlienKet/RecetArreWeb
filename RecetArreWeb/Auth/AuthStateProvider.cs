using Microsoft.AspNetCore.Components.Authorization;
using RecetArreWeb.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace RecetArreWeb.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider
    {

        private readonly ITokenService tokenService;

        //Usuarios anonimos (sin autenticar)
        private readonly AuthenticationState anonimo = 
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        public AuthStateProvider(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //1. Obtener el token de localStorage
            var token = await tokenService.ObtenerToken();

            //2. Si no hay token, usuario no autenticado

            if (string.IsNullOrEmpty(token))
                return anonimo;

            //3. si hay token, construir el estado de autorizacion
            return ConstruirAuthenticactionState(token);

        }

        public AuthenticationState ConstruirAuthenticactionState (string token)
        {
            try
            {
                var handler=new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var claims = jwtToken.Claims;
                var identity=new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);

            }
            catch
            {
                return anonimo;
            }
        }

        public void NotificarLogin(string token)
        {
            var authState = ConstruirAuthenticactionState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));

        }

        public void NotificarLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(anonimo));
        }


    }//fin clase authstateprovider
}//fin namespace
