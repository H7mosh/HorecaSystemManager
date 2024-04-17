using OfficeOpenXml;
using sacmy.Shared.ViewModels.PurchaseViewModel;
using System.IO;

namespace sacmy.Client.Services
{
    public class PurchaseService
    {
        public List<PurchaseViewModel> GetPurchaseFromoExcel() {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<PurchaseViewModel> purchaseViewModels = new List<PurchaseViewModel>();
            string filePath = Path.GetFullPath("D:/test2.xlsx");
            FileInfo fileInfo = new FileInfo(filePath);
            ExcelPackage pck = new ExcelPackage(filePath);

            using (var stream = File.OpenRead(filePath))
            {
                pck.Load(stream);
            }
            using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
            {
              
                var worksheet = excelPackage.Workbook.Worksheets.FirstOrDefault();
                int totalColumn = worksheet.Dimension.End.Column;
                int totalRow = worksheet.Dimension.End.Row;

                for(int row = 1; row <= totalRow; row++)
                {
                    PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
                    for(int col = 1; col <= totalColumn; col++) {
                        if (col == 1) purchaseViewModel.Sku = worksheet.Cells[row, col].ToString();
                        if (col == 2) purchaseViewModel.Code = worksheet.Cells[row, col].ToString();
                        if (col == 2) purchaseViewModel.Name = worksheet.Cells[row, col].ToString();
                        if (col == 1) purchaseViewModel.CartonCost = worksheet.Cells[row, col].ToString();
                    }
                    purchaseViewModels.Add(purchaseViewModel);
                }
            }
            return purchaseViewModels;  
        }
    }
}
