using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Ef;

public class InvitationRequestRepository: IInvitationRequestRepository
{
    private readonly DoorsDbContext _context;

    public InvitationRequestRepository(DoorsDbContext context)
    {
        _context = context;
    }

    public async Task<InvitationRequest> AddAsync(InvitationRequest request)
    {
        _context.InvitationRequests.Add(request);
        await _context.SaveChangesAsync();
        return request;
    }

    public async Task<InvitationRequest?> GetByIdAsync(int id)
    {
        return await _context.InvitationRequests
            .Include(r => r.EntityType)
            .Include(r => r.InstitutionType)
            .FirstOrDefaultAsync(r => r.InvitationRequestId == id);
    }

    public async Task<bool> ExistEmailAsync(string invitationEmail)
    {
        return await _context.InvitationRequests
            .AnyAsync(r => r.InvitationEmail.ToLower() == invitationEmail.ToLower());
    }

    public async Task<IEnumerable<InvitationRequest>> GetPendingAsync()
    {
        return await _context.InvitationRequests
            .Where(r => r.Status == InvitationRequestStatus.PENDING)
            .ToListAsync();
    }

    public async Task UpdateStatusAsync(int id, InvitationRequestStatus status, string? reason = null)
    {
        var request = await _context.InvitationRequests.FindAsync(id);
        if (request == null) return;

        request.Status = status;
        request.RejectionReason = reason;
        request.UpdatedAt = DateTime.UtcNow;

        _context.InvitationRequests.Update(request);
        await _context.SaveChangesAsync();
    }
    
    public async Task<(int Total, List<InvitationRequest> Requests)> GetByStatusAsync(
        InvitationRequestStatus? status,
        string? entityTypeName,
        int page,
        int pageSize)
    {
        var query = _context.InvitationRequests
            .Include(r => r.EntityType)
            .Include(r => r.InstitutionType)
            .AsQueryable();

        if (status.HasValue)
        {
            query = query.Where(r => r.Status == status.Value);
        }

        if (!string.IsNullOrEmpty(entityTypeName))
        {
            query = query.Where(r => r.EntityType.Name == entityTypeName);
        }

        // Compter le total
        var total = await query.CountAsync();

        // Appliquer la pagination
        var requests = await query
            .OrderBy(r => r.CreatedAt) // Optionnel : tri par date de création
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (total, requests);
    }
    
    public async Task<bool> ExistsNameInRequestsAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        return await _context.InvitationRequests
            .AnyAsync(r => r.Name.ToLower() == name.Trim().ToLower());
    }
    public async Task<bool> ExistsCompanyNumberInRequestsAsync(string companyNumber)
    {
        if (string.IsNullOrWhiteSpace(companyNumber))
            return false;

        return await _context.InvitationRequests
            .AnyAsync(r => r.CompanyNumber != null && r.CompanyNumber == companyNumber.Trim());
    }



}