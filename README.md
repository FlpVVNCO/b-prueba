# Proyecto Aplicación de consola para Defontana

Proyecto desarrollado con C# + Entity Framework.
El objetivo era desarrollar una aplicación de consola con C# + Entity Framework(opcional) que realizara una consulta a una base de datos RDS
la cual devolvería las ventas de los ultimos 30 días y con esa información responder las preguntas requeridas.
También realizar las consultas SQL con las mismos requerimientos.

## Comenzando a desplegar 🚀

Estas instrucciones te ayudarán a clonar y configurar el proyecto en tu máquina local para desarrollo y pruebas.

### Prerrequisitos

Antes de comenzar, asegúrate de tener instalado [.NET SDK](https://dotnet.microsoft.com/download) en tu sistema. 
Necesitarás una cuenta de [GitHub](https://github.com/) para clonar el repositorio. 
También necesitaras [Visual Studio Code](https://code.visualstudio.com/).

### Instalación 🔧

1. Clona este repositorio a tu máquina local:

   ```
    git clone https://github.com/FlpVVNCO/b-prueba/
   ```
   
2. Ve al directorio del proyecto:

   ```
    cd b-prueba
   ```
3. Instala las dependencias utilizando dotnet:

   ```
    dotnet restore
   ```

4. Inicia la aplicación de consola utilizando:

   ```
    dotnet run
   ```

_El proyecto se ejecutará y mostrará las respuestas de las preguntas._


## Historias de usuario

- Historia de Usuario 1: Consultar Ventas de los Últimos 30 Días
Como usuario interesado en analizar las ventas, quiero poder obtener la información de todas las ventas de los últimos 30 días desde la base de datos utilizando Entity Framework.

- Historia de Usuario 2: Imprimir Datos por Consola
Como usuario interesado en obtener información clave sobre las ventas, quiero poder ver ciertos datos importantes por consola.

- Historia de Usuario 3: Generar Consultas SQL
Como usuario interesado en obtener información directamente desde la base de datos, quiero tener consultas SQL listas para ejecutar en SQL Management o programas similares para responder preguntas específicas sin depender de la aplicación .NET.

## Comentarios

- Alguna duda/error que se presente pueden contactarme a mi correo: felipevivanco05@gmail.com
