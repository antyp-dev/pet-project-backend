using CSharpFunctionalExtensions;
using PetPlatform.Domain.Aggregates.VolunteerManagement.AggregateRoot;
using PetPlatform.Domain.Shared;
using PetPlatform.Domain.Shared.EntityIds;
using PetPlatform.Domain.Shared.ValueObjects;

namespace PetPlatform.Application.Features.VolunteerFeature;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer model, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetById(VolunteerId id, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken);
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken);
    Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken);
}