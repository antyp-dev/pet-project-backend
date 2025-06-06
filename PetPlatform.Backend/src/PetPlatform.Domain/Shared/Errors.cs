namespace PetPlatform.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }

        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return Error.NotFound("record.not.found", $"record not found{forId}");
        }

        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.required", $"{label} is required");
        }

        public static Error ValueTooLong(string name, int maxLength)
        {
            return Error.Validation("value.too.long", $"{name} must not exceed {maxLength} characters");
        }

        public static Error ValueIsInFuture(string name, DateOnly futureLimit)
        {
            return Error.Validation("value.in.future", $"{name} cannot be later than {futureLimit:yyyy-MM-dd}");
        }

        public static Error ValueIsTooOld(string name, DateOnly minDate)
        {
            return Error.Validation("value.too.old", $"{name} cannot be earlier than {minDate:yyyy-MM-dd}");
        }

        public static Error ValueTooSmall(string name, int min)
        {
            return Error.Validation("value.too.small", $"{name} must be at least {min}");
        }

        public static Error ValueTooLarge(string name, int max)
        {
            return Error.Validation("value.too.large", $"{name} must not exceed {max}");
        }

        public static Error UnknownValue(string name, string value)
        {
            return Error.Validation("value.unknown", $"Unknown {name}: '{value}'");
        }

        public static Error AlreadyExists(string name, string? value = null)
        {
            var label = value is null ? "" : $" '{value}'";
            return Error.Conflict("value.already.exists", $"{name}{label} already exists");
        }
    }
}