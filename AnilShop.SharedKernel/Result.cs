using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;

namespace AnilShop.SharedKernel;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }
    
    protected internal Result(bool isValid, params ValidationError[] validationErrors)
    {
        IsInvalid = isValid;
        ValidationErrors = [..validationErrors];
    }
    
    public bool IsSuccess { get; }
    
    public bool IsInvalid { get; }

    public bool IsFailure => !IsSuccess;

    public List<ValidationError> ValidationErrors { get; protected set; } = new();
    
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);
    
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    
    public static Result<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result<TValue> Invalid<TValue>(ValidationError validationError) =>
        new(default, false, validationError);
    
    public static Result<TValue> Invalid<TValue>(params ValidationError[] validationErrors) =>
        new(default, false, validationErrors);
    
    public static Result<TValue> Invalid<TValue>(List<ValidationError> validationErrors) =>
        new(default, false, validationErrors.ToArray());
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }
    
    protected internal Result(TValue? value, bool isValid, params ValidationError[] validationErrors)
        : base(isValid, validationErrors)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue? value) => Create(value);
}


public class ValidationError
{
    public ValidationError(string errorMessage) => this.ErrorMessage = errorMessage;
    public string ErrorMessage { get; set; }
}

public static class FluentValidationResultExtensions
{
    public static List<ValidationError> AsErrors(this ValidationResult valResult)
    {
        List<ValidationError> validationErrorList = new List<ValidationError>();
        foreach (ValidationFailure error in valResult.Errors)
            validationErrorList.Add(new ValidationError(error.ErrorMessage));
        return validationErrorList;
    }
}