using FastMember;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ToDo.Business.Interfaces;

namespace ToDo.Business.Concrete
{
    public class DosyaManager : IDosyaService
    {
        public byte[] AktarExcel<T>(List<T> list) where T : class, new()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var excelPackage = new ExcelPackage();
            var excelBlank = excelPackage.Workbook.Worksheets.Add("Calisma1");
            excelBlank.Cells["A1"].LoadFromCollection(list, true, OfficeOpenXml.Table.TableStyles.Light15);

            return excelPackage.GetAsByteArray();
        }

        public string AktarPdf<T>(List<T> list) where T : class, new()
        {
            DataTable dataTable = new();
            dataTable.Load(ObjectReader.Create(list));

            string fileName = Guid.NewGuid() + ".pdf";
            string filePath = "wwwroot/documents/" + fileName;
            string pathCombine = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            var stream = new FileStream(pathCombine, FileMode.Create);

            string arialTtf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
            BaseFont baseFont = BaseFont.CreateFont(arialTtf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new(baseFont, 12, Font.NORMAL);


            Document document = new(PageSize.A4, 15f, 15f, 25f, 25f);
            PdfWriter.GetInstance(document, stream);

            document.Open();

            PdfPTable pdfPTable = new(dataTable.Columns.Count);

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                pdfPTable.AddCell(new Phrase(dataTable.Columns[i].ColumnName, font));
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    pdfPTable.AddCell(new Phrase(dataTable.Rows[i][j].ToString(), font));
                }
            }

            document.Add(pdfPTable);

            document.Close();

            return "/documents/" + fileName;
        }
    }
}
