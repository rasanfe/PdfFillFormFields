using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFillFormFields
{
    /// <summary>
    /// Validaciones de entrada reutilizables (nulos, extensión, existencia del
    /// fichero). Es <c>internal</c> y <c>static</c>: solo se usa dentro de esta
    /// librería como "guardas" antes de tocar el PDF. Lanzan excepciones que
    /// luego el llamador captura y convierte en la cadena de error.
    /// </summary>
    internal static class Controls
    {
        /// <summary>Comprueba de una vez que el fichero no es nulo, tiene la extensión esperada y existe.</summary>
        public static void FileCheck(string inputFile, string extension)
        {
            NullEmty(inputFile);
            Extension(inputFile, extension);
            Exist(inputFile);

        }
        /// <summary>Lanza <see cref="ArgumentNullException"/> si el argumento es nulo, vacío o solo espacios.</summary>
        public static void NullEmty(string argument)
        {
            string errorText;

            if (String.IsNullOrWhiteSpace(argument))
            {
                errorText = nameof(argument) + " cannot be null";
                throw new ArgumentNullException(paramName: nameof(argument), message: errorText);
            }
        }
        /// <summary>Verifica que <paramref name="inputFile"/> termina en la extensión indicada (sin el punto).</summary>
        public static void Extension(string inputFile, string extension)
        {
            string errorText;

            // Path.GetExtension devuelve el punto incluido (".pdf"), por eso lo
            // anteponemos al comparar.
            if (Path.GetExtension(inputFile) != "." + extension.ToLower())
            {
                errorText = nameof(inputFile) + " Extension is not " + extension.ToUpper();
                throw new ArgumentException(paramName: nameof(inputFile), message: errorText);
            }
        }
        /// <summary>Lanza si el fichero no existe en disco.</summary>
        public static void Exist(string inputFile)
        {
            string errorText;

            bool fileExist = File.Exists(inputFile);

            if (fileExist == false)
            {
                errorText = nameof(inputFile) + " Not Exist";
                throw new ArgumentNullException(paramName: nameof(inputFile), message: errorText);

            }

        }
    }
}
