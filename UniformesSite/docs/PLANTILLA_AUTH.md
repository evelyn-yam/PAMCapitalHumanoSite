# Plantilla de autenticacion JWT

Este documento describe la parte de autenticacion que viene incluida como referencia. La aplicacion funciona sin backend de auth: las pantallas de login son visuales y el dashboard permanece accesible.

Los archivos bajo `Services/Auth`, `Dtos/Auth`, `Persistence/AuthRepository.cs`, `Modules/HttpClient/Handlers/AccessTokenHandler.cs` y `Components/RedirectToLogin.razor` existen para acelerar la integracion futura, pero estan excluidos desde `Template.csproj`.

## Estado actual

| Elemento | Estado |
| --- | --- |
| Pantallas auth | Activas como UI demo |
| Login contra API | Desactivado |
| Registro contra API | Desactivado |
| JWT en localStorage | Desactivado |
| Rutas `[Authorize]` | Desactivadas |
| Redireccion automatica a login | Desactivada |

## Como activar auth real

### 1. Paquetes NuGet

En `Template.csproj`, descomenta:

```xml
<PackageReference Include="Microsoft.AspNetCore.Components.Authorization" Version="10.0.3" />
<PackageReference Include="Microsoft.AspNetCore.WebUtilities" Version="10.0.3" />
```

Despues elimina el `ItemGroup` que contiene los `Compile Remove` de auth.

### 2. Configuracion

Agrega la seccion `Auth` en `wwwroot/appsettings.json`:

```json
"Auth": {
  "SignInPath": "./v1/auth/sign-in",
  "SignUpPath": "./v1/auth/sign-up"
}
```

Ajusta las rutas al contrato real del backend.

### 3. Registro de servicios

En `Program.cs`, descomenta los usings y registros marcados como `PLANTILLA AUTH`:

```csharp
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<JwtAuthStateProvider>());
```

En `_Imports.razor`, descomenta los usings de authorization, componentes y servicios de auth.

En `Modules/Injection/InjectionExtensions.cs`, descomenta:

```csharp
services.AddScoped<IAuthRepository, AuthRepository>();
services.AddScoped<IAccessTokenService, AccessTokenService>();
services.AddScoped<IAuthService, AuthService>();
```

### 4. HttpClient con token

En `Modules/HttpClient/HttpClientExtensions.cs`, descomenta el `AccessTokenHandler`:

```csharp
services.AddScoped<AccessTokenHandler>();

services.AddHttpClient("ApiService", client =>
{
    client.BaseAddress = new Uri(normalizedBaseUrl);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("BLAZOR-SITE");
    client.Timeout = TimeSpan.FromMinutes(2);
})
.AddHttpMessageHandler<AccessTokenHandler>();
```

### 5. Opciones de auth

En `Modules/Features/FeatureExtensions.cs`, descomenta:

```csharp
services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.SectionName));
```

### 6. UnitOfWork

En `Persistence/IUnitOfWork.cs` y `Persistence/UnitOfWork.cs`, descomenta la propiedad `AuthRepository` y agregala al constructor.

### 7. Router protegido

En `App.razor`:

- Comenta el router simple activo.
- Descomenta el bloque con `CascadingAuthenticationState`.
- Usa `AuthorizeRouteView` y `RedirectToLogin`.

En las paginas del dashboard, descomenta:

```csharp
@attribute [Authorize]
```

### 8. Paginas auth

En `Pages/Auth/SignIn.razor`, reemplaza el submit demo por una llamada a `IAuthService.SignInAsync`.

En `Pages/Auth/SignUp.razor`, reemplaza el submit demo por una llamada a `IAuthService.SignUpAsync`.

En `Layout/Shared/Sidebar.razor`, descomenta el bloque de logout si el proyecto va a cerrar sesion desde el menu.

## Contrato esperado de API

### Sign in

Request:

```json
{
  "email": "usuario@dominio.com",
  "password": "password",
  "rememberMe": true
}
```

Response:

```json
{
  "accessToken": "eyJ..."
}
```

Tambien se acepta la propiedad `token`.

### Sign up

Request:

```json
{
  "email": "usuario@dominio.com",
  "password": "password"
}
```

Response:

```json
{
  "accessToken": "eyJ..."
}
```

## Archivos incluidos

| Archivo | Responsabilidad |
| --- | --- |
| `Services/Auth/AuthService.cs` | Coordina login, registro, logout y notificacion del estado de sesion |
| `Services/Auth/AccessTokenService.cs` | Lee, guarda y elimina el token en localStorage |
| `Services/Auth/JwtAuthStateProvider.cs` | Expone el usuario autenticado a Blazor usando claims del JWT |
| `Services/Auth/JwtTokenParser.cs` | Lee claims y expiracion del token desde cliente |
| `Modules/HttpClient/Handlers/AccessTokenHandler.cs` | Agrega `Authorization: Bearer` a las peticiones HTTP |
| `Persistence/AuthRepository.cs` | Ejecuta las llamadas HTTP de sign in y sign up |
| `Dtos/Auth/*` | Define los contratos JSON de autenticacion |
| `Entities/AuthConstants.cs` | Centraliza las claves de localStorage |
| `Components/RedirectToLogin.razor` | Redirige a `/sign-in` conservando `returnUrl` |

## Comportamiento esperado al activarlo

```text
Sign in correcto
  -> se guarda accessToken
  -> JwtAuthStateProvider notifica usuario autenticado
  -> dashboard permite entrada

Request a API
  -> AccessTokenHandler agrega Bearer token
  -> repositorios consumen el cliente ApiService

Logout
  -> se elimina accessToken
  -> Blazor actualiza AuthenticationState
  -> usuario vuelve a sign-in
```
