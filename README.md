# 🎯 PerfilSena - Sistema de Gestión de Perfiles y Comentarios

Sistema web moderno para crear perfiles de usuarios y gestionar comentarios entre ellos, desarrollado con **ASP.NET Core 8.0** y **Blazor WebAssembly**. Diseño inspirado en Instagram.

![Versión](https://img.shields.io/badge/version-1.0.0-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Tabla de Contenidos

- [Características](#-características)
- [Tecnologías](#-tecnologías)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Uso](#-uso)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [API Endpoints](#-api-endpoints)
- [Capturas de Pantalla](#-capturas-de-pantalla)
- [Solución de Problemas](#-solución-de-problemas)
- [Contribuir](#-contribuir)
- [Licencia](#-licencia)
- [Autor](#-autor)

---

## ✨ Características

### 👥 Gestión de Perfiles
- ✅ Crear perfiles con información personal
- ✅ Subir foto de perfil (hasta 5MB)
- ✅ Campos: Nombre, Teléfono, Dirección (máximo 50 caracteres cada uno)
- ✅ Visualización en cuadrícula estilo Instagram
- ✅ Editar y eliminar perfiles

### 💬 Sistema de Comentarios
- ✅ Chat entre perfiles
- ✅ Mensajes en tiempo real
- ✅ Historial de conversaciones
- ✅ Interfaz tipo mensajería instantánea
- ✅ Límite de 500 caracteres por mensaje

### 🎨 Diseño
- ✅ Interfaz moderna inspirada en Instagram
- ✅ Diseño responsive (móvil y escritorio)
- ✅ Imágenes circulares de perfil
- ✅ Animaciones y transiciones suaves
- ✅ Tema claro con colores característicos de Instagram

---

## 🛠️ Tecnologías

### Backend
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM para base de datos
- **SQL Server** - Base de datos relacional
- **Swagger/OpenAPI** - Documentación de API

### Frontend
- **Blazor WebAssembly** - Framework SPA
- **C#** - Lenguaje de programación
- **HTML5 & CSS3** - Maquetación y estilos
- **HTTP Client** - Comunicación con API

### Arquitectura
- **Clean Architecture** - Separación de capas
- **Repository Pattern** - Acceso a datos
- **Dependency Injection** - Inversión de dependencias
- **RESTful API** - Comunicación cliente-servidor

---

## 📦 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express, LocalDB o cualquier edición)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/) (opcional)

### Verificar instalación de .NET
```bash
dotnet --version
# Debe mostrar: 8.0.x
```

---

## 🚀 Instalación

### 1️⃣ Clonar el repositorio
```bash
git clone https://github.com/tuusuario/PerfilSena.git
cd PerfilSena
```

### 2️⃣ Restaurar dependencias

#### Backend
```bash
cd PerfilSena.API
dotnet restore
```

#### Frontend
```bash
cd ../PerfilSena.WEB
dotnet restore
```

### 3️⃣ Configurar la base de datos

#### Editar cadena de conexión

Abre `PerfilSena.API/appsettings.json` y configura tu conexión a SQL Server:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PerfilDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

**Opciones comunes:**

- **SQL Server LocalDB:**
```
  Server=(localdb)\\mssqllocaldb;Database=PerfilDB;Trusted_Connection=True;
```

- **SQL Server Express:**
```
  Server=localhost\\SQLEXPRESS;Database=PerfilDB;Trusted_Connection=True;TrustServerCertificate=True
```

- **SQL Server con autenticación:**
```
  Server=localhost;Database=PerfilDB;User Id=sa;Password=TuPassword;TrustServerCertificate=True
```

#### Aplicar migraciones
```bash
cd PerfilSena.API

# Instalar herramienta EF Core (si no está instalada)
dotnet tool install --global dotnet-ef

# Crear migración inicial
dotnet ef migrations add InitialCreate

# Aplicar migración a la base de datos
dotnet ef database update
```

### 4️⃣ Crear carpeta para imágenes
```bash
# En PerfilSena.API
mkdir -p wwwroot/img
```

---

## ⚙️ Configuración

### Puertos por defecto

- **Backend API**: `http://localhost:5134`
- **Swagger**: `http://localhost:5134/swagger`
- **Frontend**: `http://localhost:5168` (puede variar)

### Configurar CORS (si es necesario)

En `PerfilSena.API/Program.cs`:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

## 🎮 Uso

### Ejecutar el proyecto

#### Opción 1: Terminal separadas

**Terminal 1 - Backend:**
```bash
cd PerfilSena.API
dotnet run --urls "http://localhost:5134"
```

**Terminal 2 - Frontend:**
```bash
cd PerfilSena.WEB
dotnet run
```

#### Opción 2: Visual Studio

1. Clic derecho en la solución → **Configurar proyectos de inicio**
2. Seleccionar **Proyectos de inicio múltiples**
3. Establecer `PerfilSena.API` y `PerfilSena.WEB` en **Iniciar**
4. Presionar **F5**

### Acceder a la aplicación

1. **Backend API**: Abre `http://localhost:5134/swagger` para ver la documentación
2. **Frontend**: Abre `http://localhost:5168` (o el puerto que indique la consola)

### Flujo de uso

1. **Crear un perfil:**
   - Click en "Perfiles" en el menú
   - Click en "+ Nuevo Perfil"
   - Completar información (nombre, teléfono, dirección)
   - Cargar imagen de perfil (opcional)
   - Click en "Crear Perfil"

2. **Crear otro perfil:**
   - Repetir el proceso anterior
   - Necesitas al menos 2 perfiles para usar el chat

3. **Enviar comentarios:**
   - Selecciona un perfil de la lista
   - Click en "💬 Ver Comentarios"
   - Selecciona un destinatario en el panel lateral
   - Escribe tu mensaje y presiona Enter o click en ➤

---

## 📁 Estructura del Proyecto
```
PerfilSena/
│
├── PerfilSena.API/                     # Backend - ASP.NET Core Web API
│   ├── Controllers/                    # Endpoints de la API
│   │   ├── PabloReyesController.cs     # CRUD de perfiles
│   │   └── ComentarioController.cs     # Gestión de comentarios
│   │
│   ├── Data/                           # Capa de datos
│   │   ├── AppDbContext.cs             # Contexto de Entity Framework
│   │   └── Migrations/                 # Migraciones de BD
│   │
│   ├── Models/                         # Modelos de dominio
│   │   ├── PabloReyes.cs               # Entidad Perfil
│   │   └── Comentario.cs               # Entidad Comentario
│   │
│   ├── Services/                       # Lógica de negocio
│   │   ├── IPabloReyesService.cs       # Interfaz
│   │   ├── PabloReyesService.cs        # Implementación
│   │   ├── IComentarioService.cs       # Interfaz
│   │   └── ComentarioService.cs        # Implementación
│   │
│   ├── wwwroot/                        # Archivos estáticos
│   │   └── img/                        # Imágenes de perfil
│   │
│   ├── appsettings.json                # Configuración
│   ├── Program.cs                      # Punto de entrada
│   └── PerfilSena.API.csproj
│
├── PerfilSena.WEB/                     # Frontend - Blazor WebAssembly
│   ├── Models/                         # DTOs
│   │   ├── Pabloreyes.cs               # Modelo de perfil
│   │   └── Comentario.cs               # Modelo de comentario
│   │
│   ├── Services/                       # Cliente HTTP
│   │   ├── IPabloReyesService.cs       # Interfaz
│   │   ├── PabloReyesService.cs        # Implementación
│   │   ├── IComentarioService.cs       # Interfaz
│   │   └── ComentarioService.cs        # Implementación
│   │
│   ├── Pages/                          # Componentes de página
│   │   ├── Index.razor                 # Página de inicio
│   │   ├── Perfiles.razor              # Gestión de perfiles
│   │   └── Chat.razor                  # Sistema de mensajería
│   │
│   ├── Shared/                         # Componentes compartidos
│   │   ├── MainLayout.razor            # Layout principal
│   │   ├── NavMenu.razor               # Menú de navegación
│   │   └── PerfilModal.razor           # Modal de creación
│   │
│   ├── wwwroot/                        # Recursos estáticos
│   │   ├── css/
│   │   │   └── app.css                 # Estilos principales
│   │   └── index.html                  # HTML base
│   │
│   ├── _Imports.razor                  # Imports globales
│   ├── App.razor                       # Componente raíz
│   ├── Program.cs                      # Punto de entrada
│   └── PerfilSena.WEB.csproj
│
└── README.md                           # Este archivo
```

---

## 🌐 API Endpoints

### Perfiles (PabloReyes)

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/pabloreyes` | Obtener todos los perfiles |
| GET | `/api/pabloreyes/{id}` | Obtener perfil por ID |
| POST | `/api/pabloreyes` | Crear nuevo perfil |
| PUT | `/api/pabloreyes/{id}` | Actualizar perfil |
| DELETE | `/api/pabloreyes/{id}` | Eliminar perfil |

#### Ejemplo: Crear perfil

**Request:**
```http
POST /api/pabloreyes
Content-Type: multipart/form-data

nombre: "Juan Pérez"
telefono: "3001234567"
direccion: "Calle 123 #45-67"
imagen: [archivo]
```

**Response:**
```json
{
  "id": 1,
  "nombre": "Juan Pérez",
  "telefono": "3001234567",
  "direccion": "Calle 123 #45-67",
  "imagen": "img/pabloreyes_xxxxx.jpg",
  "fechaCreacion": "2024-01-15T10:30:00Z"
}
```

### Comentarios

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/comentario/conversacion/{id1}/{id2}` | Obtener chat entre dos perfiles |
| GET | `/api/comentario/pabloreyes/{id}` | Obtener comentarios de un perfil |
| POST | `/api/comentario` | Enviar comentario |

#### Ejemplo: Enviar comentario

**Request:**
```http
POST /api/comentario
Content-Type: application/json

{
  "contenido": "Hola, ¿cómo estás?",
  "pabloReyesEmisorId": 1,
  "pabloReyesReceptorId": 2
}
```

**Response:**
```json
{
  "id": 1,
  "contenido": "Hola, ¿cómo estás?",
  "fecha": "2024-01-15T10:35:00Z",
  "pabloReyesEmisorId": 1,
  "pabloReyesReceptorId": 2
}
```

---

## 📸 Capturas de Pantalla

### Página de Inicio
> Interfaz de bienvenida con hero section y características principales

### Gestión de Perfiles
> Grid de perfiles con imágenes circulares estilo Instagram

### Modal de Creación
> Formulario para crear nuevos perfiles con vista previa de imagen

### Sistema de Chat
> Interfaz de mensajería con lista de contactos y conversaciones

---

## 🐛 Solución de Problemas

### Error: "No se puede conectar a la base de datos"

**Solución:**
1. Verifica que SQL Server esté corriendo
2. Confirma la cadena de conexión en `appsettings.json`
3. Ejecuta: `dotnet ef database update`
```bash
# Verificar si la BD existe
dotnet ef database list

# Recrear BD desde cero
dotnet ef database drop -f
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Error: "CORS policy: No 'Access-Control-Allow-Origin'"

**Solución:**
1. Verifica que `app.UseCors("AllowAll");` esté ANTES de `app.UseRouting()`
2. Confirma que el backend esté corriendo en `http://localhost:5134`
3. Reinicia ambos proyectos

### Error: "Las imágenes no se cargan"

**Solución:**
1. Verifica que la carpeta `wwwroot/img` exista en el backend
2. Confirma que `app.UseStaticFiles();` esté en `Program.cs`
3. Prueba acceder directamente: `http://localhost:5134/img/nombrearchivo.jpg`
```bash
# Verificar permisos de la carpeta
cd PerfilSena.API
ls -la wwwroot/img
```

### Error: "Los comentarios no se envían"

**Solución:**
1. Abre la consola del navegador (F12) y busca errores
2. Verifica que ambos perfiles existan
3. Confirma que el backend devuelva 200 OK en Swagger
```bash
# Probar endpoint manualmente
curl -X POST http://localhost:5134/api/comentario \
  -H "Content-Type: application/json" \
  -d '{"contenido":"Test","pabloReyesEmisorId":1,"pabloReyesReceptorId":2}'
```

### Error: "dotnet ef not found"

**Solución:**
```bash
dotnet tool install --global dotnet-ef --version 8.0.0
```

---

## 🤝 Contribuir

Las contribuciones son bienvenidas. Para cambios importantes:

1. Fork el proyecto
2. Crea una rama para tu característica (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add: nueva característica'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

### Guía de estilo

- Usa nombres descriptivos en español
- Comenta código complejo
- Sigue los principios SOLID
- Añade pruebas unitarias

---

## 📝 Tareas Pendientes

- [ ] Implementar autenticación JWT
- [ ] Agregar paginación en lista de perfiles
- [ ] Notificaciones en tiempo real con SignalR
- [ ] Búsqueda y filtrado de perfiles
- [ ] Exportar conversaciones a PDF
- [ ] Modo oscuro
- [ ] Pruebas unitarias e integración
- [ ] Dockerización del proyecto
- [ ] CI/CD con GitHub Actions

---

## 📄 Licencia

Este proyecto está bajo la Licencia MIT. Ver el archivo [LICENSE](LICENSE) para más detalles.
```
MIT License

Copyright (c) 2024 [Tu Nombre]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction...
```

---

## 👨‍💻 Autor

**Pablo Reyes**

- GitHub: [@pablo2240](https://github.com/pablo2240)
- Email: reyestorrespablo22@gmail.com

---

## 🙏 Agradecimientos

- Desarrollado como proyecto educativo para SENA
- Basado en las mejores prácticas de .NET 8.0

---

## 📚 Recursos Adicionales

- [Documentación oficial de .NET](https://docs.microsoft.com/dotnet/)
- [Guía de Blazor](https://docs.microsoft.com/aspnet/core/blazor/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [ASP.NET Core Web API](https://docs.microsoft.com/aspnet/core/web-api/)

---

## 🔗 Enlaces Útiles

- [Repositorio del proyecto](https://github.com/tuusuario/PerfilSena)
- [Reporte de bugs](https://github.com/tuusuario/PerfilSena/issues)
- [Solicitar características](https://github.com/tuusuario/PerfilSena/issues/new)

---

<div align="center">

**⭐ Si este proyecto te fue útil, considera darle una estrella ⭐**

Hecho con ❤️ por la comunidad SENA

</div>
```

---

## 📋 Archivo LICENSE (MIT)

Crea también un archivo `LICENSE` en la raíz del proyecto:
```
MIT License

Copyright (c) 2024 Pablo Reyes

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
