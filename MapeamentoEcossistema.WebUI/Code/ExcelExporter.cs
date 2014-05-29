using System;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using ClosedXML.Excel;

namespace MapeamentoEcossistema.WebUI.Code
{
    public class ExcelExporter
    {
        // Public methods.
        public static Stream Export(DataTable dataTable)
        {
            var book = CreateWorkbook(dataTable);
            var stream = new MemoryStream();
            
            book.SaveAs(stream);
            stream.Position = 0;

            return stream;
        }

        // Private methods.
        private static XLWorkbook CreateWorkbook(DataTable dataTable)
        {
            if (String.IsNullOrEmpty(dataTable.TableName))
                dataTable.TableName = "Dados";

            var book = new XLWorkbook();
            var sheet = book.Worksheets.Add(dataTable);

            return book;
        }
    }
}
