using System.Data;
using System.Data.OleDb;
using IronXL;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ITDocumentation
{
    public class SpreedsheetHandler
    {

        public DataTable ReadSpreadsheet(string fileName) {
            DataTable data = new DataTable();
            List<string> rowList = new List<string>();
            XSSFWorkbook xssWorkbook;
            string sheetName;
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                stream.Position = 0;
                xssWorkbook = new XSSFWorkbook(stream);
                sheetName = xssWorkbook.GetSheetAt(0).SheetName;
            }

            XSSFSheet sh = (XSSFSheet)xssWorkbook.GetSheet(sheetName);
            int row = 0;
            while (sh.GetRow(row) != null) {

                if (data.Columns.Count < sh.GetRow(row).Cells.Count){

                    for (int j=0;j< sh.GetRow(row).Cells.Count;j++) {

                        data.Columns.Add(sh.GetRow(row).GetCell(j).ToString(),typeof(string));

                    }
                }

                data.Rows.Add();

                for (int j = 0;j<sh.GetRow(row).Cells.Count;j++) { 
                
                    var cell = sh.GetRow(row).GetCell(j);
                    if (cell != null)
                    {
                       
                        data.Rows[row][j] = sh.GetRow(row).GetCell(j).ToString();

                    }
                }
                row++;
            }
            

            return data;
        }

    }
}
