using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Files
{
    public class FileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment _env;
        public FileStorage(IWebHostEnvironment env) { _env = env; }

        public async Task<string> SaveAsync(IFormFile file, string relativeFolder)
        {
            // relativeFolder không có slash đầu; ví dụ: "Uploads/Workflow/Submissions/{id}/Files"
            var baseFolder = Path.Combine(_env.ContentRootPath, relativeFolder.Replace("/", Path.DirectorySeparatorChar.ToString()));
            Directory.CreateDirectory(baseFolder);

            var safeName = file.FileName; // hoặc thêm Guid prefix nếu sợ trùng tên
            var phys = Path.Combine(baseFolder, safeName);
            using (var fs = new FileStream(phys, FileMode.Create)) { await file.CopyToAsync(fs); }

            // StoredPath: "/{relativeFolder}/{fileName}"
            return "/" + Path.Combine(relativeFolder, safeName).Replace("\\", "/");
        }

        public bool Delete(string storedPath)
        {
            var phys = Path.Combine(_env.ContentRootPath, storedPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (File.Exists(phys)) { File.Delete(phys); return true; }
            return false;
        }

        public bool DeleteDirectory(string relativeFolder)
        {
            var phys = Path.Combine(_env.ContentRootPath, relativeFolder.Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (Directory.Exists(phys)) { Directory.Delete(phys, true); return true; }
            return false;
        }

        public string BuildDownloadUrl(string storedPath)
            => $"/files{storedPath}"; // Controller /files phục vụ download; hoặc trả storedPath nếu bạn serve tĩnh

        public (string PhysicalPath, string ContentType, string FileName)? ResolveForDownload(string storedPath, string? fileName = null, string? contentType = null)
        {
            var phys = Path.Combine(_env.ContentRootPath, storedPath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (!System.IO.File.Exists(phys)) return null;
            var ct = string.IsNullOrWhiteSpace(contentType) ? "application/octet-stream" : contentType!;
            var fn = string.IsNullOrWhiteSpace(fileName) ? Path.GetFileName(phys) : fileName!;
            return (phys, ct, fn);
        }
    }
}
