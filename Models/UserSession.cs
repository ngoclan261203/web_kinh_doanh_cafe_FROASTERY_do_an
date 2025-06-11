using System.ComponentModel.DataAnnotations;

namespace FROASTERY.Models
{

public class UserSession
{
    public string? UserName { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}
}
