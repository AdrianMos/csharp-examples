using System;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace npoi_excel_workbook
{
    class Program
    {
        static void Main(string[] args)
        {
            Example example = new Example();
            example.CopyRangeExample();
        }
    }

    class Example
    {
        public void CopyRangeExample()
        {
            var filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\input\\test.xlsx"));
            var workbook = OpenWorkbook(filePath);

            var destinationSheetName = "destination" + (workbook.NumberOfSheets + 1).ToString();
            workbook.CreateSheet(destinationSheetName);

            ISheet sourceSheet = workbook.GetSheet("source");
            ISheet destinationSheet = workbook.GetSheet(destinationSheetName);

            CopyColumn("I", sourceSheet, destinationSheet);
            CopyRange(CellRangeAddress.ValueOf("C6:E15"), sourceSheet, destinationSheet);

            SaveWorkbook(workbook, filePath);
        }


        private IWorkbook OpenWorkbook(string path)
        {
            IWorkbook workbook;
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                workbook = WorkbookFactory.Create(fileStream);
            }
            return workbook;
        }

        private void SaveWorkbook(IWorkbook workbook, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fileStream);
            }
        }


        private void CopyCell(ICell source, ICell destination)
        {
            if (destination != null && source != null)
            {
                //you can copy other attributes
                destination.CellComment = source.CellComment;
                destination.CellStyle = source.CellStyle;
                destination.Hyperlink = source.Hyperlink;

                switch (source.CellType)
                {
                    case CellType.Formula:
                        destination.CellFormula = source.CellFormula; break;
                    case CellType.Numeric:
                        destination.SetCellValue(source.NumericCellValue); break;
                    case CellType.String:
                        destination.SetCellValue(source.StringCellValue); break;
                }
            }
        }

        private void CopyRange(CellRangeAddress range, ISheet sourceSheet, ISheet destinationSheet)
        {
            for (var rowNum = range.FirstRow; rowNum <= range.LastRow; rowNum++)
            {
                IRow sourceRow = sourceSheet.GetRow(rowNum);

                if (destinationSheet.GetRow(rowNum) == null)
                    destinationSheet.CreateRow(rowNum);

                if (sourceRow != null)
                {
                    IRow destinationRow = destinationSheet.GetRow(rowNum);

                    for (var col = range.FirstColumn; col < sourceRow.LastCellNum && col <= range.LastColumn; col++)
                    {
                        destinationRow.CreateCell(col);
                        CopyCell(sourceRow.GetCell(col), destinationRow.GetCell(col));
                    }
                }
            }
        }

        private void CopyColumn(string column, ISheet sourceSheet, ISheet destinationSheet)
        {
            int columnNum = CellReference.ConvertColStringToIndex(column);
            var range = new CellRangeAddress(0, sourceSheet.LastRowNum, columnNum, columnNum);
            CopyRange(range, sourceSheet, destinationSheet);
        }
    }
}
