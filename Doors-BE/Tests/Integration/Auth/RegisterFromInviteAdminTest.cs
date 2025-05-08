using FluentAssertions;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

public class RegisterFromInviteAdminTest
{
    private readonly DoorsDbContext _db;

    public RegisterFromInviteAdminTest()
    {
        var options = new DbContextOptionsBuilder<DoorsDbContext>()
            .UseMySql("Server=localhost;Database=doors;Uid=root;Pwd=;", new MySqlServerVersion(new Version(8, 0, 32)))
            .Options;

        _db = new DoorsDbContext(options);
    }

    [Theory]
    [InlineData("test@test.fsd", "EntityAdmin")]
    public async Task Should_Have_All_Data_After_Register_From_Invite_Admin(string email, string expectedInvitationType)
    {
        // 🔍 1. Vérifie l'utilisateur
        var user = await _db.Users
            .Include(u => u!.EntityUser).ThenInclude(eu => eu!.Entity)
            .FirstOrDefaultAsync(u => u!.Email == email);

        user.Should().NotBeNull("L'utilisateur doit exister");
        user!.EntityUser.Should().NotBeNull("L'utilisateur doit être lié à une entité");
        user.EntityUser!.RoleId.Should().BeGreaterThan(0);
        user.EntityUser.Entity.Should().NotBeNull();

        // 📩 2. Invitation utilisée
        var invite = await _db.SuperadminInvitations
            .Include(i => i.InvitationType)
            .FirstOrDefaultAsync(i => i.Email == email);

        invite.Should().NotBeNull("L'invitation doit exister");
        invite!.Used.Should().BeTrue("L'invitation doit être marquée utilisée");
        invite.InvitationType!.Name.Should().Be(expectedInvitationType);

        // 🔗 3. Lien entité/rôle
        var link = await _db.SuperadminInvitationEntities
            .FirstOrDefaultAsync(e => e.SuperadminInvitationId == invite.SuperadminInvitationId);

        link.Should().NotBeNull("Lien entité/rôle manquant");

        // 🧠 4. Tentatives de connexion
        var login = await _db.LoginAttempts
            .Where(l => l.Email == email)
            .OrderByDescending(l => l.AttemptTime)
            .ToListAsync();

        login.Should().NotBeEmpty("Aucune tentative de login trouvée");

        // 📘 5. Session enregistrée
        var session = await _db.SessionEvents
            .Where(s => s.UserId == user.UserId)
            .OrderByDescending(s => s.EventTime)
            .ToListAsync();

        session.Should().NotBeEmpty("Aucun événement de session trouvé");

        // 🔐 6. Refresh token généré
        var refresh = await _db.RefreshTokens
            .Where(r => r.UserId == user.UserId)
            .OrderByDescending(r => r.CreatedAt)
            .FirstOrDefaultAsync();

        refresh.Should().NotBeNull("Aucun refresh token trouvé");
    }
}