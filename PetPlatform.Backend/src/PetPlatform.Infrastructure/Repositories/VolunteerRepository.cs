using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetPlatform.Application.Features.VolunteerFeature;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot;
using PetPlatform.Domain.Shared;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Volunteer model, CancellationToken cancellationToken)
    {
        await _dbContext.Volunteers.AddAsync(model, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return model.Id;
    }

    public async Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Email == email, cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }
}