namespace Domain.Exceptions;

public interface IHasExtraData
{
    object? ExtraData { get; }
}