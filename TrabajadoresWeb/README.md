# Proyecto TrabajadoresWeb

Este proyecto es una aplicación ASP.NET Core MVC para registrar, editar, listar y eliminar trabajadores desde una base de datos SQL Server.


## Instrucciones

1. Restaurar los paquetes y compilar el proyecto.
2. Antes de ejecutar, **modificar la cadena de conexión** con tus datos de acceso en el archivo `appsettings.json`. con tu clave y usuario de SQL Server.

## Comentario

Utilice SQL Server Authentication

### Ejemplo de cadena de conexión:

"ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-KVVF9C0\\SUICHI;Database=TrabajadoresPrueba;User Id=sa;Password=mierda123;TrustServerCertificate=True;Encrypt=False"
  },