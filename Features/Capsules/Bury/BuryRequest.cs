namespace TimeCapsule.Features.Capsules.Bury;

public class BuryRequest
{
    public string Message { get; set; }
    public DateTime UnlockAt { get; set; }
}