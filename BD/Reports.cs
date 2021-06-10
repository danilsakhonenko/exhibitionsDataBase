using System;
using System.Data;
using System.IO;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;

namespace BD
{
    class Reports
    {
        DataTable Data { get; set; }
        string FileName { get; set; }
        public Reports(string file, DataTable dt)
        {
            Data = dt;
            FileName = file;
        }


        public void HtmlReport()
        {
            StreamWriter sw = new StreamWriter(FileName+".html", false);
            sw.Write("<table border=\"1\">\n<tbody>");
            sw.WriteLine("<tr>");
            foreach (DataColumn col in Data.Columns)
            {
                sw.WriteLine(String.Format("<th>{0}</th>", col.ColumnName));
            }
            sw.WriteLine("</tr>");
            for(int i = 0; i < Data.Rows.Count; i++)
            {
                sw.WriteLine("<tr>");
                for (int j = 0; j < Data.Columns.Count; j++)
                {
                    sw.WriteLine(String.Format("<td>{0}</td>", Data.Rows[i].ItemArray[j]));
                }
                sw.WriteLine("</tr>");
            }
            sw.WriteLine("</tbody>\n</table>");
            sw.Close();
        }

        public void ExcelReport()
        {
            Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
            ex.Visible = false;
            ex.SheetsInNewWorkbook = 1;
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            ex.DisplayAlerts = false;
            Excel.Range range;
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(1);
            sheet.Name = "Отчет по запросу ("+FileName+")";
            for (int i = 1;i<=Data.Columns.Count;i++)
            {
                sheet.Cells[1, i] = Data.Columns[i - 1].ColumnName;
            }
            range= sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, Data.Columns.Count]];
            range.Font.Bold = true;
            for (int i = 1; i <= Data.Rows.Count; i++)
            {
                for (int j = 1; j <= Data.Columns.Count; j++)
                    sheet.Cells[i+1, j] = Data.Rows[i-1].ItemArray[j-1].ToString();
            }
            range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[Data.Rows.Count, Data.Columns.Count]];
            range.EntireColumn.AutoFit();
            range.EntireRow.AutoFit();
            ex.Application.ActiveWorkbook.SaveAs(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+"\\" + FileName + ".xlsx", 
                Type.Missing,Type.Missing,Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            workBook.Close(false, Type.Missing, Type.Missing);
            ex.Quit();
        }

    }
}
