using Domain.Entities;

namespace Domain.Interfaces;

public interface IStudentMovementRepository
{
    Task<StudentMovement> AddAsync(StudentMovement studentMovement);
    Task<StudentMovement?> GetByIdAsync(int studentMovementId);
    Task<bool> ExistsAsync(int studentMovementId);
    Task<bool> ExistNameAsync(string name);
}