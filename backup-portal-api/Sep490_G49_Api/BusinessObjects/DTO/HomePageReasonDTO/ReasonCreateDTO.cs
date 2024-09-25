using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.DTO.HomePageReasonDTO
{
    public class ReasonCreateDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "SubTitle is required.")]
        [MaxLength(200, ErrorMessage = "SubTitle cannot exceed 200 characters.")]
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(200, ErrorMessage = "Content cannot exceed 200 characters.")]
        public string Content { get; set; }
    }
}
