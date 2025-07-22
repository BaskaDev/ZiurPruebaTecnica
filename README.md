# Prueba Técnica - ZiurBlazorApp

**Autor:** Jhon Hernandez - Full Stack Developer

## Descripción

Esta es una aplicación desarrollada en Blazor WebAssembly como prueba técnica. La app consume una API REST protegida con Bearer Token y muestra los datos en una grilla con paginación y buscador.

## Características
- Consumo seguro de API REST con configuración externa.
- Visualización de datos en tabla con paginación y resaltado de búsqueda.
- Arquitectura siguiendo el patrón MVC adaptado a Blazor.

## Configuración

1. **Clona el repositorio:**
   ```sh
   git clone <url-del-repo>
   ```
2. **Ingresa a la carpeta del proyecto:**
   ```sh
   cd "Prueba tecnica/ZiurBlazorApp"
   ```
3. **Configura la API y el Bearer Token:**
   - Copia el archivo de ejemplo:
     ```sh
     cp wwwroot/appsettings.example.json wwwroot/appsettings.json
     ```
   - O crea `wwwroot/appsettings.json` con el siguiente contenido:
     ```json
     {
       "ApiUrl": "<URL_DE_LA_API>",
       "BearerToken": "<TU_BEARER_TOKEN>"
     }
     ```



## Ejecución

Desde la carpeta `ZiurBlazorApp`, ejecuta:

```sh
 dotnet run
```

Luego abre tu navegador en la URL que indique la terminal (por ejemplo, http://localhost:5000).

## Estructura del Proyecto

- `Models/` - Modelos de datos
- `Services/` - Servicios de acceso a datos (API)
- `Pages/` - Vistas y controladores (code-behind)
- `wwwroot/appsettings.json` - Configuración de API y credenciales

---

**Prueba técnica realizada por Jhon Hernandez - Full Stack Developer** 