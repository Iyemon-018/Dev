using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;

namespace EPPlustSample
{
    public class ExcelManager
    {
        public void Collect(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            using (var excel = new ExcelPackage(fileInfo))
            {
                var workSheet = excel.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Sheet1");

                int row = 0;
                ReadChild(workSheet.Cells["A2"], ref row, 0);
            }
        }

        private void ReadChild(ExcelRangeBase cell, ref int row, int level)
        {
            do
            {
                bool isValueTag = !string.IsNullOrEmpty(cell.Offset(row, 6).Text);

                int depth;
                int.TryParse(cell.Offset(row, 1).Text, out depth);

                if (level != 0 && depth <= level)
                {
                    break;
                }

                string tagName = cell.Offset(row, 2 + depth).Text;

                row += 1;

                if (isValueTag)
                {
                    Console.WriteLine("{2}<{0}>{1}</{0}>", tagName, "ValueTag", new string(' ', depth * 4));
                }
                else
                {
                    Console.WriteLine("{1}<{0}>", tagName, new string(' ', depth * 4));
                    ReadChild(cell, ref row, depth);
                    Console.WriteLine("{1}</{0}>", tagName, new string(' ', depth * 4));
                }

            } while (!string.IsNullOrEmpty(cell.Offset(row, 0).Text));
        }
    }
}
