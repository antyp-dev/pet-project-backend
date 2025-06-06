using CSharpFunctionalExtensions;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot;
using PetPlatform.Domain.Shared;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer model, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken);
}