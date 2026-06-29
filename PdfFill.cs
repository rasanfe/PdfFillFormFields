
using iText.Kernel.Pdf;
using iText.Kernel.Exceptions;
using iText.Forms;
using iText.Forms.Fields;


namespace PdfFillFormFields
{
    /// <summary>
    /// Rellena formularios PDF (AcroForms) con iText7. Esta es la versión
    /// <b>migrada desde iTextSharp 5</b>: lo que antes hacíamos con
    /// <c>PdfStamper</c> + <c>AcroFields</c> ahora se hace con
    /// <c>PdfDocument</c> (abierto a la vez para lectura y escritura) +
    /// <c>PdfAcroForm</c>.
    ///
    /// Cómo se consume desde PowerBuilder: instanciáis la clase vía .NET DLL
    /// Importer y llamáis a los métodos. Devuelven un <see cref="string"/> que
    /// es la "cadena de error": si vuelve <b>vacía</b> ("") todo fue bien; si
    /// trae texto, ahí tenéis el motivo del fallo. Así evitamos propagar
    /// excepciones .NET al runtime de PowerBuilder.
    /// </summary>
    public class PdfFill
    {
        /// <summary>
        /// Abre <paramref name="inputFile"/>, escribe los valores indicados en
        /// los campos del formulario y guarda el resultado en
        /// <paramref name="outputFile"/>. Al final <b>aplana</b> el formulario
        /// (los campos dejan de ser editables y se "queman" en la página).
        /// </summary>
        /// <param name="inputFile">PDF de origen con AcroForm.</param>
        /// <param name="outputFile">Ruta del PDF resultante (.pdf).</param>
        /// <param name="fieldNames">Nombres de los campos a rellenar.</param>
        /// <param name="dataField">Valores, en el mismo orden que <paramref name="fieldNames"/>.</param>
        /// <returns>Cadena vacía si todo fue bien; el mensaje de error en caso contrario.</returns>
        public string FillFormFields(string inputFile, string outputFile, string[] fieldNames, string[] dataField)
        {
            try
            {
                Controls.FileCheck(inputFile, "pdf");
                Controls.NullEmty(outputFile);
                Controls.Extension(outputFile, "pdf");

                // SetUnethicalReading(true): permite abrir PDFs con permisos de
                // propietario sin tener la contraseña. Lo necesitamos para poder
                // editar formularios que vienen "capados" pero no cifrados.
                PdfReader reader = new PdfReader(inputFile).SetUnethicalReading(true);
                PdfWriter writer = new PdfWriter(outputFile);
                // En iText7 el mismo PdfDocument lleva reader + writer: es la lectura
                // y escritura en un solo objeto (lo que en iTextSharp 5 era PdfStamper).
                PdfDocument pdfDoc = new PdfDocument(reader, writer);

                // true => crea el AcroForm si el PDF no lo tuviera todavia.
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

                for (int i = 0; i <= fieldNames.GetUpperBound(0); i++)
                {
                    string field = fieldNames[i];
                    PdfFormField formField = form.GetField(field);
                    // Si el campo no existe en el PDF, GetField devuelve null: lo
                    // saltamos en silencio en vez de reventar (mismos índices que
                    // los arrays de PowerBuilder, que a veces traen campos de más).
                    if (formField != null)
                    {
                        formField.SetValue(dataField[i]);
                    }
                }

                // FlattenFields: "quema" los valores en la página y elimina la capa
                // de formulario. Equivale al FormFlattening = true de iTextSharp 5;
                // el PDF resultante ya no es editable.
                form.FlattenFields();
                pdfDoc.Close();         // cierra y vuelca el documento al fichero de salida

                return "";
            }
            catch (PdfException ex)
            {
                string result = $"Error al llenar los campos del formulario: {ex.Message}" + $"Detalles del error: {ex.InnerException?.Message}";
                return result;
            }
            catch (Exception ex)
            {
                string result = $"Ocurrió un error inesperado: {ex.Message}";
                return result;

            }
        }

        /// <summary>
        /// Devuelve, por el parámetro <paramref name="fieldNames"/>, los nombres
        /// de todos los campos del formulario del PDF. Útil para saber qué se
        /// puede rellenar antes de llamar a <see cref="FillFormFields"/>.
        /// </summary>
        /// <param name="inputFile">PDF a inspeccionar.</param>
        /// <param name="fieldNames">
        /// Array de salida (<c>ref</c>) con los nombres de los campos. Desde
        /// PowerBuilder se pasa por referencia y se rellena aquí.
        /// </param>
        /// <returns>Cadena vacía si todo fue bien; el mensaje de error en caso contrario.</returns>
        public string GetFormFields(string inputFile, ref string[] fieldNames)
        {
            List<string> formFields = new List<string>();

            try
            {
                // Solo lectura: aquí no escribimos, así que el PdfDocument lleva
                // únicamente el reader (sin writer).
                PdfDocument pdfDoc = new PdfDocument(new PdfReader(inputFile));

                // false => no crear el AcroForm; si el PDF no tiene formulario, devuelve null.
                PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, false);
                if (form != null)
                {
                    foreach (string key in form.GetAllFormFields().Keys)
                    {
                        formFields.Add(key);
                    }
                }

                pdfDoc.Close();
                fieldNames = formFields.ToArray();

                return "";

            }
            catch (Exception ex)
            {
                string result = $"Error al obtener los campos del formulario: {ex.Message}";
                return result;

            }
        }

    }
}
