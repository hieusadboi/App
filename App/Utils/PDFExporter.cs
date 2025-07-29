using App.DTO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace App.Utils
{
    public static class PDFExporter
    {
        public static void ExportBillToPDF(ListView lsvBill, Table table, string userName, string totalPriceText, string filePath, bool isPaid)
        {
            if (lsvBill.Items.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu hóa đơn để in.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Document doc = new Document(PageSize.A5, 20f, 20f, 20f, 20f);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            // Trích số thứ tự từ tên file
            string fileName = Path.GetFileNameWithoutExtension(filePath); // Ví dụ: "HD005"
            string numberPart = "";

            for (int i = fileName.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(fileName[i]))
                    numberPart = fileName[i] + numberPart;
                else
                    break;
            }

            string orderText = string.IsNullOrEmpty(numberPart) ? "??" : int.Parse(numberPart).ToString(); // Xóa số 0 đầu


            orderText = orderText.TrimStart('0');
            if (string.IsNullOrEmpty(orderText)) orderText = "0";


            doc.Open();

            BaseFont baseFont = BaseFont.CreateFont(@"c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font normalFont = new Font(baseFont, 10, Font.NORMAL);
            Font boldFont = new Font(baseFont, 11, Font.BOLD);
            Font headerFont = new Font(baseFont, 14, Font.BOLD);

            Paragraph header = new Paragraph("QUÁN ĂN GIA ĐÌNH", headerFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 5f
            };
            doc.Add(header);

            Paragraph subTitle = new Paragraph(isPaid ? "HÓA ĐƠN THANH TOÁN\n\n" : "HÓA ĐƠN TẠM\n\n", new Font(baseFont, 12, Font.BOLD, BaseColor.DARK_GRAY))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(subTitle);

            doc.Add(new Paragraph($"Đơn số: #{orderText}", normalFont) { Alignment = Element.ALIGN_CENTER });
            string tableDisplayName = table.Name;
            if (tableDisplayName.StartsWith("Bàn "))
            {
                tableDisplayName = tableDisplayName.Substring(4); // Bỏ chữ "Bàn "
            }
            doc.Add(new Paragraph($"Bàn: {tableDisplayName}", normalFont));
            doc.Add(new Paragraph($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", normalFont));
            doc.Add(new Paragraph($"Nhân viên: {userName}", normalFont));
            doc.Add(new Paragraph("\n"));

            PdfPTable pdfTable = isPaid ? new PdfPTable(4) : new PdfPTable(2);
            pdfTable.WidthPercentage = 100;
            if (isPaid)
                pdfTable.SetWidths(new float[] { 3f, 1f, 2f, 2f });
            else
                pdfTable.SetWidths(new float[] { 4f, 1.5f });

            string[] headers = isPaid
                ? new[] { "Tên món", "Số lượng", "Đơn giá", "Thành tiền" }
                : new[] { "Tên món", "Số lượng" };


            foreach (string text in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(text, boldFont))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5f
                };
                pdfTable.AddCell(cell);
            }

            foreach (ListViewItem item in lsvBill.Items)
            {
                PdfPCell nameCell = new PdfPCell(new Phrase(item.SubItems[0].Text, normalFont)) { Padding = 4f };
                PdfPCell countCell = new PdfPCell(new Phrase(item.SubItems[1].Text, normalFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 4f
                };

                pdfTable.AddCell(nameCell);
                pdfTable.AddCell(countCell);

                if (isPaid)
                {
                    PdfPCell priceCell = new PdfPCell(new Phrase(item.SubItems[2].Text, normalFont))
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        Padding = 4f
                    };
                    PdfPCell totalCell = new PdfPCell(new Phrase(item.SubItems[3].Text, normalFont))
                    {
                        HorizontalAlignment = Element.ALIGN_RIGHT,
                        Padding = 4f
                    };

                    pdfTable.AddCell(priceCell);
                    pdfTable.AddCell(totalCell);
                }
            }

            doc.Add(pdfTable);

            bool isVipTable = table.Name.ToLower().Contains("vip");
            if (isPaid && isVipTable)
            {
                Paragraph vipCharge = new Paragraph("\n(Đã bao gồm phụ phí bàn VIP: 20.000 đ)", new Font(baseFont, 9, Font.ITALIC, BaseColor.GRAY))
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                doc.Add(vipCharge);
            }

            if (isPaid)
            {
                Paragraph total = new Paragraph($"\nTổng cộng: {totalPriceText}", boldFont)
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                doc.Add(total);
            }
            else
            {
                doc.Add(new Paragraph("\nLưu ý: Đây là hóa đơn tạm, chưa phải tổng tiền cuối cùng.", new Font(baseFont, 9, Font.ITALIC, BaseColor.GRAY)));
            }

            Paragraph contact = new Paragraph(
                 "\nLiên hệ: 0901 036 971" +
                 "\nĐịa chỉ: Tổ 18, Tân Lộc, Tân Lược, Bình Tân, Vĩnh Long" +
                 "\nCảm ơn quý khách đã sử dụng dịch vụ!", normalFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 10f
            };

            doc.Add(contact);

            doc.Close();
        }
    }
}
