using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class StudentMovementRepository:IStudentMovementRepository
{
    private readonly DoorsDbContext _context;

    public StudentMovementRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<StudentMovement> AddAsync(StudentMovement studentMovement)
    {
        _context.StudentMovements.Add(studentMovement);
        await _context.SaveChangesAsync();
        return studentMovement;
    }

    public async Task<StudentMovement?> GetByIdAsync(int studentMovementId)
    {
        return await _context.StudentMovements
            .FirstOrDefaultAsync(sm => sm.StudentMovementId == studentMovementId);
    }

    public async Task<bool> ExistsAsync(int studentMovementId)
    {
        return await _context.StudentMovements
            .AnyAsync(sm => sm.StudentMovementId == studentMovementId);
    }

    public async Task<bool> ExistNameAsync(string name)
    {
        return await _context.StudentMovements
            .AnyAsync(sm => sm.Name.ToLower() == name.ToLower());
    }
}