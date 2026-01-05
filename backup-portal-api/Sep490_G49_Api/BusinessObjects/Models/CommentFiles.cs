namespace BusinessObjects.Models
{
    public class CommentFile
    {
        public Guid Id { get; set; }

        // FK -> OkrHistory
        public Guid OkrHistoryId { get; set; }
        public OkrHistory OkrHistory { get; set; }

        // Thông tin file
        public string FileName { get; set; }           // tên hiển thị
        public string StoredPath { get; set; }         // đường dẫn tương đối để FE tải (vd: /uploads/okr/..)
        public string ContentType { get; set; }        // image/png, application/pdf...
        public long FileSize { get; set; }             // bytes

        // Một số flag hữu ích cho FE
        public bool IsImage { get; set; }              // true nếu là ảnh
        public DateTime CreatedAt { get; set; }
    }
}
