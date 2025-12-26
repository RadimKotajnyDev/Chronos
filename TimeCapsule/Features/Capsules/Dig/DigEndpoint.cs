using FastEndpoints;
using TimeCapsule.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TimeCapsule.Features.Capsules.Dig;

public class DigEndpoint(AppDbContext dbContext) : Endpoint<DigRequest, DigResponse>
{
    private readonly AppDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/api/capsules/dig/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DigRequest req, CancellationToken ct)
    {
        var capsule = await _dbContext.TimeCapsules
            .FirstOrDefaultAsync(c => c.Id == req.Id, ct);

        if (capsule == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        var isTimeLocked = DateTime.UtcNow < capsule.UnlockAt;
        var hasValidKey = capsule.OverriderId == req.OverriderId;
        
        if (isTimeLocked && !hasValidKey)
        {
            ThrowError($"Ještě je brzy! Kapsle se otevře až: {capsule.UnlockAt}");
        }

        await Send.OkAsync(new DigResponse
        {
            Message = capsule.Message,
            CreatedAt = capsule.CreatedAt
        }, cancellation: ct);
    }
}