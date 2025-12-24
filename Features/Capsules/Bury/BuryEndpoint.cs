using FastEndpoints;
using TimeCapsule.Infrastructure.Data;
using DomainEntity = TimeCapsule.Domain.TimeCapsule; 

namespace TimeCapsule.Features.Capsules.Bury;

public class BuryEndpoint : Endpoint<BuryRequest, BuryResponse>
{
    private readonly AppDbContext _dbContext;

    public BuryEndpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/api/capsules/bury");
        AllowAnonymous();
    }

    public override async Task HandleAsync(BuryRequest req, CancellationToken ct)
    {
        if (req.UnlockAt <= DateTime.UtcNow)
        {
            ThrowError("Čas odemčení musí být v budoucnosti!");
        }

        var capsule = new DomainEntity
        {
            Id = Guid.NewGuid(),
            Message = req.Message,
            CreatedAt = DateTime.UtcNow,
            UnlockAt = req.UnlockAt,
            OverriderId = req.OverriderId
        };

        await _dbContext.TimeCapsules.AddAsync(capsule, ct);
        await _dbContext.SaveChangesAsync(ct);

        await Send.OkAsync(new BuryResponse
        {
            Id = capsule.Id
        }, cancellation: ct);
    }
}