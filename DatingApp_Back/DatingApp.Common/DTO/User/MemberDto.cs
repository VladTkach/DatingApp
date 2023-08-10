using DatingApp.Common.DTO.Photo;

namespace DatingApp.Common.DTO.User;

public class MemberDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string PhotoUrl { get; set; }
    public int Age { get; set; }
    public string KnowAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string Gender;
    public string Introduction { get; set; }
    public string LookingFor { get; set; }
    public string Interests { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public List<PhotoDto> Photos { get; set; }
}