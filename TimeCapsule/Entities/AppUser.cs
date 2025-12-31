namespace TimeCapsule.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
}