using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sacmy.Shared.ViewModels.PurchaseViewModel
{
    public class ReadExcelPurchaseViewModel
    {
        public string Code { get; set; }
        public string Sku { get; set; }
        public string Name {  get; set; }
        public string CartonCost { get; set; }

        public List<ReadExcelPurchaseViewModel> ReadExcel() { 
        
            List<ReadExcelPurchaseViewModel> items = new List<ReadExcelPurchaseViewModel>();

            string FilePath = "D:/test.xlsx";

            FileInfo fileInfo = new FileInfo(FilePath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                for(int row = 1; row <= rowCount; row++)
                {
                    ReadExcelPurchaseViewModel readExcelPurchaseViewModel = new ReadExcelPurchaseViewModel();
                    for(int col = 1; col <= colCount; col++)
                    {
                        if (col == 1) readExcelPurchaseViewModel.Code = worksheet.Cells[row, col].Value.ToString();
                        else if(col == 2) readExcelPurchaseViewModel.Sku = worksheet.Cells[row, col].Value.ToString();
                    }
                    items.Add(readExcelPurchaseViewModel);
                }
            }

            return items;
        }
    }
}
