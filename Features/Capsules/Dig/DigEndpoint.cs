using FastEndpoints;

namespace TimeCapsule.Features.Capsules.Dig;

public class DigEndpoint: Endpoint<DigRequest, DigResponse>
{
    public override void Configure()
    {
        Get("/api/capsules/{Id:guid}/dig");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DigRequest req, CancellationToken ct)
    {
        var capsule = await _db.Capsules.FindAsync(req);

        if (capsule is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }
        
        
        if (DateTime.UtcNow < capsule.UnlockAt)
        {
            var timeRemaining = capsule.UnlockAt - DateTime.UtcNow;
            AddError($"Časová kapsle je zamčená. Odemkne se za {timeRemaining.TotalSeconds} sekund.");
            await Send.ErrorsAsync(403, ct);
            return;
        }

        await Send.ResponseAsync(new DigResponse
        {
            Message = capsule.Message,
            WrittenAt = capsule.CreatedAt
        });
    }
}