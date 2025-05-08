using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;

namespace Infrastructure.Ef;

public class StudentRepository : IStudentRepository
{
    private readonly DoorsDbContext _context;

    public StudentRepository(DoorsDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Student> AddAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student; // Retourne l’entité avec l’ID généré
    }


    public Task<Student?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int studentId)
    {
        throw new NotImplementedException();
    }
}