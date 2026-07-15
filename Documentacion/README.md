# Sistema de Matrícula Universitaria

## Descripción

El proyecto consiste en un sistema web de matrícula universitaria desarrollado con ASP.NET Core MVC. Su propósito es facilitar la gestión de estudiantes, carreras, cursos, docentes y matrículas mediante una interfaz web dinámica, autenticación de usuarios y almacenamiento de información en una base de datos SQLite.

## Tecnologías utilizadas

- ASP.NET Core MVC
- C#
- Entity Framework Core
- ASP.NET Core Identity
- SQLite
- Bootstrap
- Razor Views
- Visual Studio Code
- .NET 10

## Funcionalidades implementadas

### Autenticación y seguridad

- Registro de usuarios.
- Inicio y cierre de sesión.
- Confirmación de cuenta en el entorno local.
- Roles de Administrador y Estudiante.
- Protección de controladores mediante autorización.
- Restricción de módulos administrativos según el rol.

### Gestión de carreras

- Listar carreras.
- Registrar carreras.
- Consultar detalles.
- Editar carreras.
- Eliminar carreras.
- Mostrar el estado activo o inactivo.

### Gestión de cursos

- Listar cursos.
- Registrar cursos.
- Asociar cada curso con una carrera.
- Consultar detalles.
- Editar cursos.
- Eliminar cursos.
- Administrar créditos, cupo máximo y estado.

### Gestión de docentes

- Listar docentes.
- Registrar docentes.
- Consultar detalles.
- Editar docentes.
- Eliminar docentes.
- Administrar especialidad, correo, teléfono y estado.

### Gestión de estudiantes

- Listar estudiantes.
- Registrar estudiantes.
- Asociar cada estudiante con una carrera.
- Consultar detalles.
- Editar estudiantes.
- Eliminar estudiantes.
- Administrar información personal y estado.

### Gestión de matrículas

- Listar matrículas.
- Registrar matrículas.
- Relacionar estudiantes con cursos.
- Consultar detalles.
- Editar matrículas.
- Eliminar matrículas.
- Evitar matrículas duplicadas en el mismo curso y período académico.
- Administrar fecha, período, estado y nota final.

## Roles del sistema

### Administrador

Puede acceder a los módulos:

- Carreras
- Cursos
- Docentes
- Estudiantes
- Matrículas

También puede crear, consultar, editar y eliminar registros.

### Estudiante

Puede:

- Registrarse e iniciar sesión.
- Consultar las carreras disponibles.
- Ver los detalles de una carrera.

No puede acceder a las funciones administrativas.

## Usuarios de prueba

### Administrador

Correo:

admin@universidad.com

Contraseña:

Admin123*

### Estudiante

Correo:

estudiante@universidad.com

Contraseña:

Estudiante123*

## Base de datos

El sistema utiliza SQLite. En la carpeta BaseDatos se incluyen:

- ProyectoFinal.db
- ProyectoFinal.sql

La base de datos contiene las tablas principales:

- Carreras
- Cursos
- Docentes
- Estudiantes
- Matriculas

También contiene las tablas utilizadas por ASP.NET Core Identity para administrar usuarios y roles.

## Instrucciones para ejecutar el proyecto

1. Abrir una terminal.
2. Entrar en la carpeta del proyecto.
3. Restaurar las dependencias.
4. Aplicar las migraciones.
5. Ejecutar la aplicación.

Comandos:

dotnet restore

dotnet ef database update

dotnet run

6. Abrir en el navegador la dirección indicada después del mensaje:

Now listening on:

## Pruebas realizadas

Se verificaron las siguientes operaciones:

- Registro e inicio de sesión.
- Asignación de roles.
- Restricción de acceso por roles.
- Registro, consulta, modificación y eliminación de carreras.
- Registro, consulta, modificación y eliminación de cursos.
- Registro, consulta, modificación y eliminación de docentes.
- Registro, consulta, modificación y eliminación de estudiantes.
- Registro, consulta, modificación y eliminación de matrículas.
- Validación de formularios.
- Prevención de matrículas duplicadas.
- Persistencia de datos en SQLite.

## Observación

El proyecto fue desarrollado como implementación inicial de un sistema de matrícula universitaria. La solución incluye las entidades y operaciones principales requeridas para continuar ampliando sus funcionalidades en etapas posteriores.
