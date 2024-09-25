using BusinessObjects.Mapping;
using System.Text.Json.Serialization;

namespace BusinessObjects.DTO.HomePage
{
    public class HomePageDTO
    {
        public int Id { get; set; }

        public string TitleBody { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string StatusJobPost { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]

        public DateTime CreateAt { get; set; }
        public string Status { get; set; }
        public string ImageBackgroundDetail { get; set; }
        public string ImageBackgroundPath { get; set; }
    }
}
