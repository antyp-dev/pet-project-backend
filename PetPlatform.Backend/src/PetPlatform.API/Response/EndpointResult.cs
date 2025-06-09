using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http.Metadata;
using PetPlatform.Domain.Shared;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace PetPlatform.API.Response;

public sealed class EndpointResult<TValue> : IResult
{
    private readonly IResult _result;

    public EndpointResult(Result<TValue, Error> result)
    {
        _result = result.IsSuccess
            ? new SuccessResult<TValue>(result.Value)
            : new ErrorsResult(result.Error);
    }

    public EndpointResult(Result<TValue, ErrorList> result)
    {
        _result = result.IsSuccess
            ? new SuccessResult<TValue>(result.Value)
            : new ErrorsResult(result.Error);
    }

    public Task ExecuteAsync(HttpContext httpContext) => _result.ExecuteAsync(httpContext);

    public static implicit operator EndpointResult<TValue>(Result<TValue, Error> result) => new(result);
    public static implicit operator EndpointResult<TValue>(Result<TValue, ErrorList> result) => new(result);
}