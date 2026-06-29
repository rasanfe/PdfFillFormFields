# 📝 PdfFillFormFields

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![iText7](https://img.shields.io/badge/iText7-9.6-007E33?style=flat-square)
![Blog](https://img.shields.io/badge/blog-rsrsystem-FF5722?style=flat-square&logo=blogger&logoColor=white)

> Librería **.NET 10** para **rellenar campos de formularios PDF** (AcroForms) desde PowerBuilder, con [iText7](https://github.com/itext/itext-dotnet).

## 📋 ¿Qué es esto?

Le pasas un PDF con formulario, los **nombres de los campos** y sus **valores**, y te devuelve el PDF
relleno (con opción de **aplanar** el formulario). También sabe **listar** los campos de un PDF. Se
consume desde PowerBuilder como un `dotnetobject`.

## ✨ API (`PdfFillFormFields.PdfFill`)

```csharp
// Rellena los campos indicados y aplana el formulario
string FillFormFields(string inputFile, string outputFile, string[] fieldNames, string[] dataField);

// Devuelve por referencia los nombres de los campos del PDF
string GetFormFields(string inputFile, ref string[] fieldNames);
```

> Las funciones devuelven `""` si todo fue bien, o el mensaje de error en caso contrario.

## 🧩 Dependencias

| Paquete | Versión |
|---------|---------|
| [itext7](https://www.nuget.org/packages/itext7) | `9.6.0` |

> 🆕 **Migración a .NET 10:** reescrito de **iTextSharp 5** (abandonado) a **iText7 9.6.0**
> (`PdfStamper`/`AcroFields` → `PdfDocument` + `PdfAcroForm`).

## 🛠️ Requisitos

- **.NET SDK 10.0** o superior

## 🚀 Compilar

```bat
dotnet build PdfFillFormFields.csproj -c Release
```

La DLL queda en `bin\Release\net10.0\`.

## 🔗 Proyecto PowerBuilder relacionado

👉 **pbPdfFillFormFields** — https://github.com/rasanfe/pbPdfFillFormFields

---

📨 **Blog:** <https://rsrsystem.blogspot.com/>

> ¡Nos vemos en el próximo artículo! Y recuerda: en PowerBuilder, los límites solo están en nuestra imaginación. 🚀
