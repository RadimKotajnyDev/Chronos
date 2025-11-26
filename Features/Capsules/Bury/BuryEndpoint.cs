using FastEndpoints;

namespace TimeCapsule.Features.Capsules.Bury;

public class BuryEndpoint : Endpoint<BuryRequest, BuryResponse>
{
    public override void Configure()
    {
        Post("/api/capsules/{Id:guid}/bury");
        AllowAnonymous();
    }

    public async override Task HandleAsync(BuryRequest req, CancellationToken ct)
    {
        Domain.TimeCapsule timeCapsule = new();
    }
}