namespace TimeCapsule.Domain;

public class TimeCapsule
{
    public Guid Id { get; init; }
    public string Message { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UnlockAt { get; init; }
}