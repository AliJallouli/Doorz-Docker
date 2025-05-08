using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Ef;

public class UserAgentRepository : IUserAgentRepository
{
    private readonly DoorsDbContext _context;
    private readonly ILogger<UserAgentRepository> _logger;

    public UserAgentRepository(DoorsDbContext context, ILogger<UserAgentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    
    // Méthode pour récupérer un user agent existant
    public async Task<UserAgent?> GetAsync(string userAgentValue)
    {
        var existing = await _context.UserAgents
            .AsNoTracking()
            .FirstOrDefaultAsync(ua => ua.UserAgentValue == userAgentValue);
        
        // Si trouvé, retourne l'ID, sinon retourne 0
        return existing;
    }

    // Méthode pour ajouter un user agent
    public async Task<int> AddAsync(UserAgent userAgent)
    {
        // Log avant l'ajout
        _logger.LogInformation("Tentative d'ajout du UserAgent : {UserAgent}", userAgent.UserAgentValue);

        // Ajout du UserAgent
        _context.UserAgents.Add(userAgent);
        await _context.SaveChangesAsync(); // Sauvegarde les changements dans la base de données

        // Vérification si l'ajout a réussi
        if (userAgent.UserAgentId > 0)
        {
            _logger.LogInformation("UserAgent ajouté avec succès avec l'ID : {UserAgentId}", userAgent.UserAgentId);
        }
        else
        {
            _logger.LogWarning("Échec de l'ajout du UserAgent.");
        }

        // Retourne l'ID du UserAgent ajouté
        return userAgent.UserAgentId;
    }


 
}