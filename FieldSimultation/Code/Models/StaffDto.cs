namespace Code.Models;
public class StaffDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string IdentificationColor { get; set; }
    public string Initials { get; set; }
    public UserDto User { get; set; }
    public StaffDto? Superior { get; set; } 
    public string? Rank { get; set; }  
}