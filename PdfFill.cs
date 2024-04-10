
using iTextSharp.text.pdf;


namespace PdfFillFormFields
{
    public class PdfFill
    {
        public string FillFormFields(string inputFile, string outputFile, string[] fieldNames, string[] dataField)
        {
            try
            {
                Controls.FileCheck(inputFile, "pdf");
                Controls.NullEmty(outputFile);
                Controls.Extension(outputFile, "pdf");

                Stream outputPdfStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None);

                PdfReader.unethicalreading = true;
                var reader = new PdfReader(inputFile);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfFormFields = stamper.AcroFields;

                for (int i = 0; i <= fieldNames.GetUpperBound(0); i++)
                {
                    string field = fieldNames[i];
                    stamper.AcroFields.SetField(field, dataField[i]);
                }
                stamper.FormFlattening = true;
                stamper.Dispose();
                stamper.Close();
                reader.Dispose();
                reader.Close();
   
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

        public string GetFormFields(string inputFile, ref string[] fieldNames)
        {
            List<string> formFields = new List<string>();

            try
            {
                PdfReader reader = new PdfReader(inputFile);

                foreach (KeyValuePair<string, AcroFields.Item> de in reader.AcroFields.Fields)
                {
                    formFields.Add(de.Key);
                }

                reader.Dispose();
                reader.Close();
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
