using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace App.Utils
{
    public static class InvoiceFileHelper
    {
        private static readonly string RootFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HoaDon");
        private static readonly int DaysToKeep = 7;

        public static string GetInvoiceFilePath(bool isFinalBill)
        {
            string typeFolder = isFinalBill ? "HoaDon" : "HoaDonTam";
            string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
            string fullPath = Path.Combine(RootFolder, typeFolder, dateFolder);

            // Tạo thư mục nếu chưa có
            Directory.CreateDirectory(fullPath);

            // Xóa thư mục cũ hơn 7 ngày
            CleanOldFolders(Path.Combine(RootFolder, typeFolder));

            // Sinh file theo dạng HD001.pdf, HD002.pdf
            int count = Directory.GetFiles(fullPath, "HD*.pdf").Length + 1;
            string fileName = $"HD{count:D3}.pdf";

            return Path.Combine(fullPath, fileName);
        }

        private static void CleanOldFolders(string basePath)
        {
            if (!Directory.Exists(basePath)) return;

            foreach (string folder in Directory.GetDirectories(basePath))
            {
                string folderName = Path.GetFileName(folder);
                if (DateTime.TryParse(folderName, out DateTime folderDate))
                {
                    if ((DateTime.Now - folderDate).TotalDays > DaysToKeep)
                    {
                        try { Directory.Delete(folder, true); } catch { /* ignore error */ }
                    }
                }
            }
        }
        public static string GetInvoiceFilePath(bool isPaid, int billId)
        {
            string today = DateTime.Now.ToString("yyyyMMdd");
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invoices", today);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // Kiểm tra file đã tồn tại (1 hóa đơn tạm, 1 hóa đơn chính cho mỗi billId)
            string baseFile = $"Bill_{today}_*_{billId}";
            string[] existing = Directory.GetFiles(folder, $"{baseFile}*.pdf");

            if (isPaid && existing.Any(f => !f.Contains("TAM")))
                return existing.First(f => !f.Contains("TAM"));

            if (!isPaid && existing.Any(f => f.Contains("TAM")))
                return existing.First(f => f.Contains("TAM"));

            // Tạo mới nếu chưa tồn tại
            int order = existing.Length + 1;
            string suffix = isPaid ? ".pdf" : "_TAM.pdf";
            string fileName = $"Bill_{today}_{order:000}_{billId}{suffix}";
            return Path.Combine(folder, fileName);
        }

    }
}
