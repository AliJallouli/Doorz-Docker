using Domain.Entities;

namespace Domain.Interfaces;

public interface IStudentRepository
{
    Task<Student> AddAsync(Student student);
    Task<Student?> GetByIdAsync(int id);

    Task<bool> ExistsAsync(int studentId);
}