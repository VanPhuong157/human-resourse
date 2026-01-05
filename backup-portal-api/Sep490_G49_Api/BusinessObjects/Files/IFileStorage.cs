using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Files
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(IFormFile file, string relativeFolder); // return StoredPath (ví dụ: /Uploads/Workflow/Submissions/{id}/Files/{name})
        bool Delete(string storedPath);                                 // storedPath bắt đầu bằng /Uploads/...
        bool DeleteDirectory(string relativeFolder);                    // relativeFolder: "Uploads/Workflow/Submissions/{id}/Files"
        string BuildDownloadUrl(string storedPath);                     // map -> /files/{storedPath...} hoặc trả storedPath
        (string PhysicalPath, string ContentType, string FileName)? ResolveForDownload(string storedPath, string? fileName = null, string? contentType = null);
    }
}
