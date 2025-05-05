using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFillFormFields
{
    internal static class Controls
    {
        public static void FileCheck(string inputFile, string extension)
        {
            NullEmty(inputFile);
            Extension(inputFile, extension);
            Exist(inputFile);

        }
        public static void NullEmty(string argument)
        {
            string errorText;

            if (String.IsNullOrWhiteSpace(argument))
            {
                errorText = nameof(argument) + " cannot be null";
                throw new ArgumentNullException(paramName: nameof(argument), message: errorText);
            }
        }
        public static void Extension(string inputFile, string extension)
        {
            string errorText;

            if (Path.GetExtension(inputFile) != "." + extension.ToLower())
            {
                errorText = nameof(inputFile) + " Extension is not " + extension.ToUpper();
                throw new ArgumentException(paramName: nameof(inputFile), message: errorText);
            }
        }
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
