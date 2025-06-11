using Aspose.Words;
using Aspose.Pdf;
using System.Text;

public class TinTucService
{
    private readonly IWebHostEnvironment _env;

    public TinTucService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string DocNoiDungTinTuc(string tenFile)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(tenFile))
                return "Tên file không hợp lệ.";

            var filePath = Path.Combine(_env.WebRootPath, "uploads", tenFile);

            if (!File.Exists(filePath))
                return $"Không tìm thấy file tại: {filePath}";

            var extension = Path.GetExtension(filePath).ToLowerInvariant();

            return extension switch
            {
                ".docx" => ReadWordToHtml(filePath),
                ".pdf" => ReadPdfToHtml(filePath),
                ".txt" => File.ReadAllText(filePath, Encoding.UTF8),
                ".html" => File.ReadAllText(filePath, Encoding.UTF8),
                _ => "Định dạng file không được hỗ trợ."
            };
        }
        catch (Exception ex)
        {
            return $"<p>Lỗi đọc file: {ex.Message}</p>";
        }
    }

    private string ReadWordToHtml(string path)
    {
        try
        {
            var doc = new Aspose.Words.Document(path);
            var options = new Aspose.Words.Saving.HtmlSaveOptions(Aspose.Words.SaveFormat.Html)
            {
                ExportImagesAsBase64 = true
            };

            using var stream = new MemoryStream();
            doc.Save(stream, options);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            return $"<p>Lỗi đọc file Word: {ex.Message}</p>";
        }
    }

    private string ReadPdfToHtml(string path)
    {
        try
        {
            using var doc = new Aspose.Pdf.Document(path);
            using var stream = new MemoryStream();
            doc.Save(stream, Aspose.Pdf.SaveFormat.Html);
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            return $"<p>Lỗi đọc file PDF: {ex.Message}</p>";
        }
    }
}
