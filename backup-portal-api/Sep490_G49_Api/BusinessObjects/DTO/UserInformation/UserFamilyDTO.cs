namespace BusinessObjects.DTO.UserInformation
{
    public class UserFamilyDTO
    {
        public Guid UserId { get; set; }
        public List<FamilyDTO> FamilyMembers { get; set; } = new List<FamilyDTO>();
    }
}
