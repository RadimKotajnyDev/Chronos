using FastEndpoints;
using TimeCapsule.Infrastructure.Data;
using DomainEntity = TimeCapsule.Domain.TimeCapsule; 

namespace TimeCapsule.Features.Capsules.Bury;

public class BuryEndpoint : Endpoint<BuryRequest, BuryResponse>
{
    // Injektujeme DbContext přes konstruktor (DI)
    private readonly AppDbContext _dbContext;

    public BuryEndpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/api/capsules/bury"); // Definice URL
        AllowAnonymous(); // Zatím neřešíme přihlašování
    }

    public override async Task HandleAsync(BuryRequest req, CancellationToken ct)
    {
        // 1. Validace (pro jistotu, i když FE má taky validaci)
        if (req.UnlockAt <= DateTime.UtcNow)
        {
            ThrowError("Čas odemčení musí být v budoucnosti!");
        }

        // 2. Mapování Request -> Domain Entity
        var capsule = new DomainEntity
        {
            Id = Guid.NewGuid(),
            Message = req.Message,
            CreatedAt = DateTime.UtcNow,
            UnlockAt = req.UnlockAt
        };

        // 3. Uložení do DB
        await _dbContext.TimeCapsules.AddAsync(capsule, ct);
        await _dbContext.SaveChangesAsync(ct);

        // 4. Odeslání odpovědi
        await Send.OkAsync(new BuryResponse
        {
            Id = capsule.Id
        }, cancellation: ct);
    }
}