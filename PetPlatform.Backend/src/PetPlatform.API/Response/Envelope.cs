﻿using System.Text.Json.Serialization;
using PetPlatform.Domain.Shared;

namespace PetPlatform.API.Response;

public record Envelope
{
    public object? Result { get; }
    public ErrorList? Errors { get; }
    public bool IsError => Errors != null || (Errors != null && Errors.Any());
    public DateTime TimeGenerated { get; }

    [JsonConstructor]
    private Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null) =>
        new(result, null);

    public static Envelope Error(ErrorList errors) =>
        new(null, errors);
}

public record Envelope<T>
{
    public T? Result { get; }
    public ErrorList? Errors { get; }
    public bool IsError => Errors != null || (Errors != null && Errors.Any());
    public DateTime TimeGenerated { get; }

    [JsonConstructor]
    private Envelope(T? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }

    public static Envelope<T> Ok(T? result = default) =>
        new(result, null);

    public static Envelope<T> Error(ErrorList errors) =>
        new(default, errors);
}