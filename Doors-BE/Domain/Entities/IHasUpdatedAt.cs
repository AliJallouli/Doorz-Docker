namespace Domain.Entities;

public interface IHasUpdatedAt
{
    DateTime UpdatedAt { get; set; }
}