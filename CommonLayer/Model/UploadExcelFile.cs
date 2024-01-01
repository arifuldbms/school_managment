
namespace ReactWeb.CommonLayer.Model
{
    public class UploadExcelFileRequest
    {
        public IFormFile File { get; set; }
    }

    public class UploadExcelFileResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ExcelBulkUploadParameter
    {
       
        public string? StudentName { get; set; }
        public string? StudentRoll { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StudentEmail { get; set; }
        public string? StudentAddress { get; set; }
    }

}

