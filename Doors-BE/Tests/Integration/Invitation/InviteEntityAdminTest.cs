using FluentAssertions;
using Infrastructure.Ef.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.Integration.Invitation;

public class InviteEntityAdminTest
{
    private readonly DoorsDbContext _db;

    public InviteEntityAdminTest()
    {
        var options = new DbContextOptionsBuilder<DoorsDbContext>()
            .UseMySql("Server=localhost;Database=doors;Uid=root;Pwd=;AllowZeroDateTime=True;ConvertZeroDateTime=True",
                new MySqlServerVersion(new Version(8, 0, 32)))
            .Options;


        _db = new DoorsDbContext(options);
    }

    [Theory]
    [InlineData("sfdsfdz@feffzedfgz.ddc", "EntityAdmin", "Institution", "febgb")]
    public async Task Should_Have_All_Data_After_EntityAdmin_Invitation(
        string email,
        string expectedInvitationType,
        string expectedEntityTypeName,
        string expectedEntityName)
    {
        // 📩 Invitation
        var invite = await _db.SuperadminInvitations
            .Include(i => i.InvitationType)
            .FirstOrDefaultAsync(i => i.Email == email);

        invite.Should().NotBeNull();
        invite!.Used.Should().BeFalse();
        invite.InvitationType!.Name.Should().Be(expectedInvitationType);

        // 🔗 Lien entité/role
        var link = await _db.SuperadminInvitationEntities
            .FirstOrDefaultAsync(l => l.SuperadminInvitationId == invite.SuperadminInvitationId);
        link.Should().NotBeNull();

        // 🧱 Vérifie entité générique
        var entity = await _db.Entities
            .Include(e => e.EntityType)
            .FirstOrDefaultAsync(e => e.EntityId == link!.EntityId);
        entity.Should().NotBeNull();
        entity!.Name.Should().Be(expectedEntityName);
        entity.EntityType!.Name.Should().Be(expectedEntityTypeName);

        // 🏢 Vérifie la table spécifique (ex: company)
        if (expectedEntityTypeName == "Company")
        {
            var company = await _db.Companies
                .FirstOrDefaultAsync(c => c.EntityId == entity.EntityId);
            company.Should().NotBeNull("La company doit être créée");
            company!.Name.Should().Be(expectedEntityName);
        }

        // 🎓 Si c'était une institution :
        if (expectedEntityTypeName == "Institution")
        {
            var institution = await _db.Institutions
                .FirstOrDefaultAsync(i => i.EntityId == entity.EntityId);
            institution.Should().NotBeNull("L'institution doit être créée");
            institution!.Name.Should().Be(expectedEntityName);
        }

        // 👤 Vérifie qu’aucun user n’existe encore
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        user.Should().BeNull();
    }
}