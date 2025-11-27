using FastEndpoints;
using TimeCapsule.Infrastructure.Data;

namespace TimeCapsule.Features.Capsules.Dig;

public class DigEndpoint : Endpoint<DigRequest, DigResponse>
{
    public AppDbContext Db { get; set; } 

    public override void Configure()
    {
        Post("/api/capsules/dig");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DigRequest req, CancellationToken ct)
    {
        var capsule = new Domain.TimeCapsule
        {
            Id = Guid.NewGuid(),
            Message = req.Message,
            CreatedAt = DateTime.UtcNow,
            UnlockAt = DateTime.UtcNow.AddYears(1) 
        };

        Db.TimeCapsules.Add(capsule);
        await Db.SaveChangesAsync(ct);

        await Send.OkAsync(new DigResponse { Message = "Kapsle zakopána!" });
    }
}