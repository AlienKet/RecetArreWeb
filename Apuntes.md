http://pruebabd2026.somee.com/scalar/v1

http://pruebabd2026.somee.com/web/

**Prueba:**
 "email": "alien1234@gmail.com",
"password": "Alien123!"

Ejecutar: Se ejecuta en somee en website al dar click en start website.

Publicar a somee:
1. Stop Website
2. En visual ir la pestaña RecetArreWeb y darle click derecho, luego en publicar
3. Abrir la carpeta que se creo y entrar a la carpeta wwwroot
4. Seleccionar todos los archivos de esa carpeta y comprimirlos a .zip
5. Ir a somee despues a FileManager>web
6. Eliminar lo que este ahi excepto el web.config
7. Click en Subir archivo, poner el .zip y descomprimirlo

--Host
Servidor de Hosting: Vercel
--Framework y Lenguaje
Framework principal: Blazor WebAssembly (Wasm). 
Una tecnología que permite compilar código de C# para que se ejecute directamente en el navegador del usuario a velocidad casi nativa.

Lenguaje de Programación: C# (para la lógica del lado del cliente) 
combinado con HTML5 en archivos de extensión .razor

--Enrutamiento (Routing)
Se maneja del lado del cliente (Client-side routing)
por medio del motor de Blazor.

Cada vista define su ruta de acceso en la primera 
línea mediante la directiva @page (por ejemplo: 
@page "/recetas", @page "/login" o rutas dinámicas con 
parámetros como @page "/recetas/{Id:int}").

Para la navegación lógica en código C# se inyecta y
utiliza el servicio nativo NavigationManager (ej: navigation.NavigateTo("login"))

--Paleta de Colores Utilizada
Mantiene una identidad visual corporativa, limpia y moderna
(visible en los contenedores, botones y barras laterales):

Color de Identidad / Marca (Brand): Verde Esmeralda (#198754 o similar), 
usado en encabezados de modales, botones principales de éxito 
("Guardar Receta", "+ Nueva Receta") y logos.

Color de Contraste / Menú: Azul Marino Oscuro, usado en la barra de navegación 
lateral (NavMenu) para dar profundidad.

Color de Selección / Enfoque: Morado/Azul Lavanda, utilizado para 
resaltar de forma visual la pestaña activa en la que se encuentra el usuario
(ej. la pestaña "Recetas").

Badges informativos: Cian/Celeste para las categorías y Gris Claro
para destacar los ingredientes de forma individual.

--Subir a Vercel:
1. crear crear vercel.json: 
{
  "rewrites": [{ "source": "/(.*)", "destination": "/index.html" }]
}
2. Subir cambios al repositorio Commit y despues sincronizar
3.Entrar a vercel e iniciar sesión con github
https://vercel.com/new
4. Configurar el Build: En Build and Output Settings, 
cambiar los valores por estos:
Application Preset: Other
Activar Build Command y pegar: bash build.sh
Activar Output Directory: dist/wwwroot
5. Desplegar: Dar clic en Deploy y copiar el enlace que genere


