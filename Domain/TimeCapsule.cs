namespace TimeCapsule.Domain;

public class TimeCapsule
{
    public Guid Id { get; set; }
    
    public Guid? OverriderId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UnlockAt { get; set; }
}