using System.Text.Json.Serialization;

namespace ApiGateway.Models;

public class StaffViewModel 
{
    public int Id { get; set; }

    public string FullName { get; set; } 

    public string IdentificationColor { get; set; }

    public string Initials { get; set; }

    public UserViewModel User { get; set; }

    public StaffViewModel? Superior { get; set; }
    
    public RankViewModel Rank { get; set; }
}