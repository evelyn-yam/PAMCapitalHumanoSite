# Template

![.NET Version](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-WASM-512BD4?logo=blazor)
![Metronic](https://img.shields.io/badge/Metronic-9-406F8D)
![Tailwind CSS](https://img.shields.io/badge/Tailwind-CSS-38BDF8?logo=tailwindcss)
![Architecture](https://img.shields.io/badge/Architecture-Layered-2EA44F)
![Status](https://img.shields.io/badge/Template-Ready-2EA44F)

Plantilla base para construir aplicaciones **Blazor WebAssembly** con **Metronic 9**. El proyecto ya incluye layouts, tema visual, assets, paginas dummy, consumo de API por capas y una estructura preparada para crecer sin mezclar responsabilidades.

La intencion de este repositorio es servir como punto de partida para nuevos productos: clonar, renombrar, conectar servicios reales y reemplazar las pantallas dummy por modulos propios.

## Stack

| Tecnologia | Uso |
| --- | --- |
| .NET 10 | Runtime y SDK principal |
| Blazor WebAssembly | Aplicacion cliente |
| Metronic 9 | UI kit, componentes y estilos base |
| Tailwind CSS | Utilidades visuales incluidas por Metronic |
| HttpClientFactory | Clientes HTTP nombrados |
| Newtonsoft.Json | Serializacion y contratos JSON |
| Blazored.LocalStorage | Almacenamiento local para auth opcional |

## Caracteristicas

- Layout publico para autenticacion.
- Layout privado para dashboard con navbar y sidebar.
- Tema claro/oscuro con color primario `#406f8d`.
- Assets de Metronic cargados desde `wwwroot/assets`.
- Paginas dummy para auth, perfil, usuarios y 404.
- Formularios con `EditForm`, Data Annotations y validacion en tiempo real.
- Date pickers, selects, multiselect, upload, switch y password toggle de Metronic.
- Cliente API centralizado mediante `HttpClientFactory`.
- Separacion entre DTOs, entidades, mappers, servicios y repositorios.
- Plantilla JWT incluida como referencia, desactivada por defecto.

## Inicio rapido

```bash
    dotnet restore
    dotnet run --project Template.csproj
```

Para validar compilacion:

```bash
    dotnet build Template.csproj
```

Si el SDK preview genera errores de assets comprimidos durante desarrollo local, se puede validar con:

```bash
dotnet build Template.csproj --p:BlazorEnableCompression=false
```

## Configuracion

La URL base de la API se define en `wwwroot/appsettings.json`:

```json
{
  "ExternalProviders": {
    "ApiService": {
      "UrlBase": "http://localhost:3000/api/"
    }
  }
}
```

Los repositorios deben usar rutas relativas, por ejemplo:

```text
./v1/users
```

Con la configuracion anterior, la peticion final apunta a:

```text
http://localhost:3000/api/v1/users
```

## Estructura

```text
Components/     # Componentes compartidos de soporte
Dtos/           # Contratos JSON de entrada/salida con API
Entities/       # Modelos internos y constantes compartidas
Layout/         # Layouts principales, navbar y sidebar
Mappers/        # Conversiones entre DTOs y entidades
Modules/        # Inyeccion de dependencias, HttpClient y opciones
Pages/          # Paginas Razor organizadas por area
Persistence/    # Repositorios y cliente HTTP comun
Services/       # Casos de uso consumidos por la UI
ViewModels/     # Modelos de formularios y validaciones de vista
wwwroot/        # Assets, estilos, scripts y configuracion cliente
docs/           # Documentacion tecnica complementaria
```

## Capas

La aplicacion mantiene una separacion simple y explicita:

| Capa | Responsabilidad |
| --- | --- |
| `Pages` | Renderizar UI y manejar interaccion del usuario |
| `ViewModels` | Estado y validaciones de formularios |
| `Services` | Casos de uso consumidos por las paginas |
| `Persistence` | Acceso a endpoints y detalles HTTP |
| `Dtos` | Contratos exactos de la API |
| `Entities` | Modelos internos de la aplicacion |
| `Mappers` | Adaptacion entre contratos externos y modelos internos |

Ejemplo actual de usuarios:

```text
GET /api/v1/users
  -> UserDto
  -> UserMapper
  -> User
  -> Usuarios.razor
```

## Rutas

| Ruta | Layout | Descripcion |
| --- | --- | --- |
| `/` | Auth | Pantalla inicial de autenticacion |
| `/sign-in` | Auth | Login dummy |
| `/sign-up` | Auth | Registro dummy |
| `/2fa` | Auth | Validacion dummy de doble factor |
| `/reset-password/enter-email` | Auth | Recuperacion dummy de contrasena |
| `/dashboard/perfil` | Main | Formulario de perfil con validaciones y controles Metronic |
| `/dashboard/usuarios` | Main | Tabla de usuarios consumiendo `v1/users` |
| Cualquier ruta no registrada | Empty | Pagina 404 |

## Paginas dummy

| Pagina | Contenido |
| --- | --- |
| Auth | Pantallas visuales listas para conectar a un backend real |
| Perfil | Formulario con controles Metronic y validaciones de Blazor |
| Usuarios | Tabla conectada a `v1/users`; si no hay datos muestra estado vacio |
| 404 | Vista completa para rutas no encontradas |

## API de usuarios

La pagina de usuarios espera que `GET /api/v1/users` devuelva una lista con esta forma:

```json
[
  {
    "id": "1",
    "name": "Esther",
    "lastname": "Howard",
    "email": "esther.howard@acme.com",
    "role": "Disenadora de producto",
    "ubication": "Mexico",
    "status": "Activo",
    "phone": "9981000001"
  }
]
```

El DTO vive en `Dtos/Users/UserDto.cs`; la entidad interna vive en `Entities/User.cs`; la conversion vive en `Mappers/Users/UserMapper.cs`.

## Autenticacion

La autenticacion JWT esta incluida como plantilla, pero permanece desactivada para que el proyecto pueda ejecutarse sin backend de auth.

Los archivos de referencia estan excluidos desde `Template.csproj` hasta que se decida activar autenticacion real. La guia completa esta en:

```text
docs/PLANTILLA_AUTH.md
```

Cuando se active, la intencion es usar `AccessTokenHandler` para agregar automaticamente:

```http
Authorization: Bearer {accessToken}
```

a todas las peticiones realizadas con el cliente nombrado `ApiService`.

## Metronic

Los estilos y scripts principales se cargan desde `wwwroot/index.html`.

La integracion con Blazor esta en:

```text
wwwroot/js/metronic-blazor.js
```

Ese archivo reinicializa componentes de Metronic despues de navegar entre paginas, algo necesario porque Blazor cambia vistas sin recargar el documento completo.

## Convenciones

- Las paginas nuevas deben vivir dentro de `Pages/Auth`, `Pages/Dashboard` o `Pages/Shared`, segun corresponda.
- Las pantallas del dashboard deben usar `MainLayout`.
- Las pantallas publicas deben usar `AuthLayout` o el layout definido por el router.
- Los contratos de API deben declararse en `Dtos`.
- Los modelos internos deben declararse en `Entities`.
- Las conversiones entre DTOs y entidades deben declararse en `Mappers`.
- Los formularios con varias reglas de validacion deben mover su estado a `ViewModels`.
- Los componentes Razor no deben construir URLs ni consumir `HttpClient` directamente.
- Los repositorios deben usar clientes nombrados y rutas relativas.

## Renombrar el template

Al crear un proyecto nuevo a partir de esta plantilla, revisa:

- Nombre del `.csproj`.
- Namespace raiz `Template`.
- Titulo en `wwwroot/index.html`.
- Nombre visual de la aplicacion.
- Configuracion de API en `wwwroot/appsettings.json`.

## Estado actual

Este template esta listo para usarse como base. Las paginas incluidas son dummy y estan pensadas para demostrar integracion visual, estructura por capas, consumo de API y validaciones de formulario.
