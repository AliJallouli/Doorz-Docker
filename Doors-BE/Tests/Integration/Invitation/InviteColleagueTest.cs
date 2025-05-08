using FluentAssertions;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Invitation;

public class InviteColleagueTest
{
    private readonly DoorsDbContext _db;

    public InviteColleagueTest()
    {
        var options = new DbContextOptionsBuilder<DoorsDbContext>()
            .UseMySql("Server=localhost;Database=doors;Uid=root;Pwd=;", new MySqlServerVersion(new Version(8, 0, 32)))
            .Options;

        _db = new DoorsDbContext(options);
    }

    [Theory]
    [InlineData("moncollegue@hsggfe.hdg", "Colleague")]
    public async Task Should_Have_All_Data_After_Colleague_Invitation(string email, string expectedType)
    {
        // 🔍 1. Vérifie que l'invitation existe
        var invite = await _db.SuperadminInvitations
            .Include(i => i.InvitationType)
            .FirstOrDefaultAsync(i => i.Email == email);

        invite.Should().NotBeNull("L'invitation doit être enregistrée");
        invite!.Used.Should().BeFalse("L'invitation ne doit pas encore être utilisée");
        invite.InvitationToken.Should().NotBeNullOrWhiteSpace("Le token doit être généré");
        invite.InvitationType!.Name.Should().Be(expectedType);

        // 🔗 2. Vérifie le lien entité / rôle
        var link = await _db.SuperadminInvitationEntities
            .FirstOrDefaultAsync(e => e.SuperadminInvitationId == invite.SuperadminInvitationId);

        link.Should().NotBeNull("L'invitation doit être liée à une entité");
        link!.RoleId.Should().BeGreaterThan(0);
        link.EntityId.Should().BeGreaterThan(0);

        // 🧪 3. Optionnel : vérifie qu'aucun utilisateur n'existe encore avec cet email
        var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        existingUser.Should().BeNull("L'utilisateur ne doit pas encore être enregistré");
    }
}