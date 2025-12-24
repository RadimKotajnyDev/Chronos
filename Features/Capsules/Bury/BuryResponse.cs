namespace TimeCapsule.Features.Capsules.Bury;

public class BuryResponse
{
    public Guid Id { get; set; }
    public string Message { get; set; } = "Kapsle byla úspěšně zakopána.";
}