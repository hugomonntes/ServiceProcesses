using DI_EX7;
using System;

namespace SERV_EX2
{
    public class App
    {
        static void Main(string[] args)
        {
            List<Astro> aa = DI_EX7.Program.coleccionDeAstros;

        }
    }
}

//Ampliar el programa gestor de Astros (Ejercicio 8 de los temas 1, 2
//y 3 genéricos) de forma que guarde la información de la base de
//datos que se está creando.
//La colección de Astros debe cargarse al iniciar el programa y
//guardarse en el disco duro al finalizar el mismo de forma
//transparente al usuario (el usuario no participa). Esos son los únicos
//cambios en el programa, al inicio y fin del mismo, no cambies nada
//más. Hazlo en la carpeta %APPDATA% del usuario.
//Si no hiciste el ejercicio completo hazlo al menos con la posibilidad
//de disponer de una colección donde se puedan añadir y eliminar
//Planetas y Cometas para poder hacer el apartado de archivos.
