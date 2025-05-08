using Domain.Entities;
using Domain.Entities.Legals;
using Domain.Entities.Support;
using Domain.Entities.Translations;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.References;

namespace Infrastructure.Ef.Data;

public class DoorsDbContext : DbContext
{
    public DoorsDbContext(DbContextOptions<DoorsDbContext> options) : base(options)
    {
    }

    // DbSets pour toutes les entités
    public DbSet<SuperRole> SuperRoles { get; set; }
    public DbSet<InstitutionType> InstitutionTypes { get; set; }
    public DbSet<Community> Communities { get; set; }
    public DbSet<LegalStatus> LegalStatuses { get; set; }
    public DbSet<EducationLevel> EducationLevels { get; set; }
    public DbSet<Network> Networks { get; set; }
    public DbSet<Authority> Authorities { get; set; }
    public DbSet<CampusType> CampusTypes { get; set; }
    public DbSet<CompanyType> CompanyTypes { get; set; }
    public DbSet<StudyLevel> StudyLevels { get; set; }
    public DbSet<OwnerType> OwnerTypes { get; set; }
    public DbSet<HousingType> HousingTypes { get; set; }
    public DbSet<PebRating> PebRatings { get; set; }
    public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
    public DbSet<OfferType> OfferTypes { get; set; }
    public DbSet<ContractType> ContractTypes { get; set; }
    public DbSet<ScheduleType> ScheduleTypes { get; set; }
    public DbSet<SpokenLanguage> SpokenLanguages { get; set; }
    public DbSet<DurationUnit> DurationUnits { get; set; }
    public DbSet<FacilityType> FacilityTypes { get; set; }
    public DbSet<DegreeCategory> DegreeCategories { get; set; }
    public DbSet<TuitionType> TuitionTypes { get; set; }
    public DbSet<Cycle> Cycles { get; set; }
    public DbSet<DegreeType> DegreeTypes { get; set; }
    public DbSet<CertificationType> CertificationTypes { get; set; }
    public DbSet<DeliveryMode> DeliveryModes { get; set; }
    public DbSet<MimeType> MimeTypes { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<LoginAttempt> LoginAttempts { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentFriendship> StudentFriendships { get; set; }
    public DbSet<CV> CVs { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<CVSkill> CvSkills { get; set; }
    public DbSet<CVLanguage> CvLanguages { get; set; }
    public DbSet<HousingOwner> HousingOwners { get; set; }
    public DbSet<EventOwner> EventOwners { get; set; }
    public DbSet<Institution> Institutions { get; set; }
    public DbSet<Campus> Campuses { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Landlord> Landlords { get; set; }
    public DbSet<InstitutionInvitation> InstitutionInvitations { get; set; }
    public DbSet<InstitutionCampusInvitation> InstitutionCampusInvitations { get; set; }
    public DbSet<CompanyInvitation> CompanyInvitations { get; set; }
    public DbSet<CampusAllowedDomain> CampusAllowedDomains { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Housing> Housings { get; set; }
    public DbSet<HousingAmenity> HousingAmenities { get; set; }
    public DbSet<HousingApplication> HousingApplications { get; set; }
    public DbSet<HousingVisit> HousingVisits { get; set; }
    public DbSet<HousingVisitRange> HousingVisitRanges { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<OfferFavorite> OfferFavorites { get; set; }
    public DbSet<WorkHours> WorkHourss { get; set; }
    public DbSet<EntityReview> EntityReviews { get; set; }
    public DbSet<StudentReferral> StudentReferrals { get; set; }
    public DbSet<CampusFacility> CampusFacilities { get; set; }
    public DbSet<StudyDomain> StudyDomains { get; set; }
    public DbSet<Degree> Degrees { get; set; }
    public DbSet<DegreePartnership> DegreePartnerships { get; set; }
    public DbSet<OfferDegree> OfferDegrees { get; set; }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<ActivityType> ActivityTypes { get; set; }
    public DbSet<UE> UEs { get; set; }
    public DbSet<UA> UAs { get; set; }
    public DbSet<Bridge> Bridges { get; set; }
    public DbSet<PrerequisiteType> PrerequisiteTypes { get; set; }
    public DbSet<PrerequisiteSource> PrerequisiteSources { get; set; }
    public DbSet<DegreePrerequisite> DegreePrerequisites { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventInterest> EventInterests { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<EntityLike> EntityLikes { get; set; }
    public DbSet<PaymentStatus> PaymentStatuses { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<PaymentPlan> PaymentPlans { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentLog> PaymentLogs { get; set; }
    public DbSet<PaymentItem> PaymentItems { get; set; }
    public DbSet<PaymentItemEntity> PaymentItemEntities { get; set; }
    public DbSet<Refund> Refunds { get; set; }
    public DbSet<NotificationType> NotificationTypes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<EntityType> EntityTypes { get; set; }
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<EntityUser> EntityUsers { get; set; }
    public DbSet<SuperadminInvitation> SuperadminInvitations { get; set; }
    public DbSet<SuperadminInvitationEntity> SuperadminInvitationEntities { get; set; }
    public DbSet<SessionEvent> SessionEvents { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<RoleTranslation> RoleTranslations { get; set; }
    public DbSet<EntityTypeTranslation> EntityTypeTranslations { get; set; }
    public DbSet<InvitationType> InvitationTypes { get; set; }
    public DbSet<TokenType> TokenTypes { get; set; }
    public DbSet<SecurityToken> SecurityTokens { get; set; }
    public DbSet<UserAgent> UserAgents { get; set; }
    public DbSet<OtpSendLog> OtpSendLogs { get; set; }
    public DbSet<LegalDocumentType> LegalDocumentTypes { get; set; }
    public DbSet<LegalDocumentTypeTranslation> LegalDocumentTypeTranslations { get; set; }
    public DbSet<LegalDocument> LegalDocuments { get; set; }
    public DbSet<LegalDocumentClause> LegalDocumentClauses { get; set; }
    public DbSet<LegalDocumentClauseTranslation> LegalDocumentClauseTranslations { get; set; }

    public DbSet<UserLegalConsent> UserLegalConsents { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<ContactMessageType> ContactMessageTypes { get; set; }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added && entry.Entity is IHasCreatedAt created)
            {
                created.CreatedAt = now;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
            {
                if (entry.Entity is IHasUpdatedAt updated)
                {
                    updated.UpdatedAt = now;
                }
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TokenType>(entity =>
        {
            entity.ToTable("token_type");

            entity.HasKey(e => e.TokenTypeId);

            entity.Property(e => e.TokenTypeId).HasColumnName("token_type_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DefaultTokenExpirationMinutes).HasColumnName("default_token_expiration_minutes")
                .IsRequired();
            entity.Property(e => e.DefaultOtpExpirationMinutes)
                .HasColumnName("default_otp_expiration_minutes")
                .IsRequired()
                .HasDefaultValue(120);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.MinDelayMinutes)
                .HasColumnName("min_delay_minutes")
                .IsRequired()
                .HasDefaultValue(0);

            entity.Property(e => e.MaxRequestsPerWindow)
                .HasColumnName("max_requests_per_window")
                .IsRequired()
                .HasDefaultValue(0);

            entity.Property(e => e.IsRateLimited)
                .HasColumnName("is_rate_limited")
                .IsRequired()
                .HasDefaultValue(true);
            entity.Property(e => e.RateLimitWindowMinutes)
                .HasColumnName("rate_limit_window_minutes")
                .IsRequired()
                .HasDefaultValue(60);

            entity.Property(e => e.TokenRequired)
                .HasColumnName("token_required")
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(e => e.CodeOtpRequired)
                .HasColumnName("code_otp_required")
                .IsRequired()
                .HasDefaultValue(false);
            entity.Property(e => e.MaxOtpAttempts)
                .HasColumnName("max_otp_attempts")
                .IsRequired()
                .HasDefaultValue(5);


            entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("ux_token_type_name");
        });


        modelBuilder.Entity<SecurityToken>(entity =>
        {
            entity.ToTable("security_token");

            entity.HasKey(e => e.SecurityTokenId);

            entity.Property(e => e.SecurityTokenId)
                .HasColumnName("security_token_id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(e => e.TokenTypeId)
                .HasColumnName("token_type_id")
                .IsRequired();

            entity.Property(e => e.TokenHash)
                .HasColumnName("token_hash")
                .IsRequired()
                .HasMaxLength(191);
            entity.Property(e => e.CodeOtpHash)
                .HasColumnName("code_otp_hash")
                .HasMaxLength(191);
            entity.Property(e => e.OtpAttemptCount)
                .HasColumnName("otp_attempt_count")
                .IsRequired()
                .HasDefaultValue(0);


            entity.Property(e => e.IpAddress)
                .HasColumnName("ip_address")
                .HasMaxLength(45);

            entity.Property(e => e.DeviceId)
                .HasColumnName("device_id")
                .HasMaxLength(255);

            entity.Property(e => e.Metadata)
                .HasColumnName("metadata");

            entity.Property(e => e.TokenExpiresAt)
                .HasColumnName("token_expires_at")
                .IsRequired();
            entity.Property(e => e.OtpExpiresAt)
                .HasColumnName("otp_expires_at");

            entity.Property(e => e.OtpRevokedAt)
                .HasColumnName("otp_revoked_at");


            entity.Property(e => e.Used)
                .HasColumnName("used")
                .HasDefaultValue(false);

            entity.Property(e => e.Revoked)
                .HasColumnName("revoked")
                .HasDefaultValue(false);
            entity.Property(e => e.RevokeReason)
                .HasColumnName("revoke_reason")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.RevokedAt).HasColumnName("revoked_at");
            entity.Property(e => e.ConsumedAt)
                .HasColumnName("consumed_at");

            entity.Property(e => e.ResendCount)
                .HasColumnName("resend_count")
                .HasDefaultValue(0);

            entity.Property(e => e.LastSentAt)
                .HasColumnName("last_sent_at");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

            entity.HasIndex(e => e.TokenHash)
                .IsUnique()
                .HasDatabaseName("ux_token_hash_unique");

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_security_token_user")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.TokenType)
                .WithMany(t => t.SecurityTokens) // Référence explicite à la collection SecurityTokens
                .HasForeignKey(e => e.TokenTypeId)
                .HasConstraintName("fk_security_token_type")
                .OnDelete(DeleteBehavior.Restrict);
            entity.Property(e => e.UserAgentId).HasColumnName("user_agent_id");
            entity.HasOne(e => e.UserAgent)
                .WithMany()
                .HasForeignKey(e => e.UserAgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<OtpSendLog>(entity =>
        {
            entity.ToTable("otp_send_log");

            entity.HasKey(e => e.OtpSendLogId);

            entity.Property(e => e.OtpSendLogId)
                .HasColumnName("otp_send_log_id");

            entity.Property(e => e.SecurityTokenId)
                .HasColumnName("security_token_id")
                .IsRequired();

            entity.Property(e => e.SentAt)
                .HasColumnName("sent_at")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UserAgentId)
                .HasColumnName("user_agent_id");

            entity.Property(e => e.IpAddress)
                .HasColumnName("ip_address")
                .HasMaxLength(45);

            entity.HasOne(e => e.SecurityToken)
                .WithMany()
                .HasForeignKey(e => e.SecurityTokenId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.UserAgent)
                .WithMany()
                .HasForeignKey(e => e.UserAgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });


        modelBuilder.Entity<UserAgent>(entity =>
        {
            entity.ToTable("user_agents");

            entity.HasKey(e => e.UserAgentId);

            entity.Property(e => e.UserAgentId)
                .HasColumnName("user_agent_id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UserAgentValue)
                .HasColumnName("user_agent_value")
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.Browser)
                .HasColumnName("browser")
                .HasMaxLength(100);

            entity.Property(e => e.OperatingSystem)
                .HasColumnName("operating_system")
                .HasMaxLength(100);

            entity.Property(e => e.DeviceType)
                .HasColumnName("device_type")
                .HasMaxLength(50);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            entity.HasIndex(e => e.UserAgentValue)
                .IsUnique()
                .HasDatabaseName("ux_user_agents_user_agent");
        });
        modelBuilder.Entity<ContactMessage>(entity =>
        {
            entity.ToTable("contact_message");

            entity.HasKey(e => e.ContactMessageId);
            entity.Property(e => e.ContactMessageId)
                .HasColumnName("message_id")
                .IsRequired()
                .HasComment("Identifiant unique du message de contact");

            entity.Property(e => e.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(100)
                .IsRequired()
                .HasComment("Nom complet de l’expéditeur");

            entity.Property(e => e.Email)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired()
                .HasComment("Adresse email de contact");

            entity.Property(e => e.Subject)
                .HasColumnName("subject")
                .HasMaxLength(255)
                .IsRequired()
                .HasComment("Sujet du message");

            entity.Property(e => e.Message)
                .HasColumnName("message")
                .IsRequired()
                .HasComment("Contenu du message");

            entity.Property(e => e.LanguageId)
                .HasColumnName("language_id")
                .IsRequired()
                .HasComment("Langue du message (référence à spoken_language)");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .HasComment("Utilisateur connecté (null si anonyme)");

            entity.Property(e => e.IpAddress)
                .HasColumnName("ip_address")
                .HasMaxLength(45)
                .HasComment("Adresse IP de l’expéditeur (IPv4 ou IPv6)");

            entity.Property(e => e.UserAgentId)
                .HasColumnName("user_agent_id")
                .HasComment("Identifiant du user agent utilisé");

            entity.Property(e => e.ReceivedAt)
                .HasColumnName("received_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired()
                .HasComment("Date et heure de réception du message");

            entity.Property(e => e.ContactMessageTypeId)
                .HasColumnName("contact_message_type_id")
                .HasComment("Type de message de contact");
            
            entity.Property(e => e.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20)
                .HasComment("Numéro de téléphone de l’expéditeur (optionnel)");



            entity.HasOne(e => e.Users)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.UserAgent)
                .WithMany()
                .HasForeignKey(e => e.UserAgentId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ContactMessageType)
                .WithMany(t => t.ContactMessages)
                .HasForeignKey(e => e.ContactMessageTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<ContactMessageType>(entity =>
        {
            entity.ToTable("contact_message_type");

            entity.HasKey(e => e.ContactMessageTypeId);

            entity.Property(e => e.ContactMessageTypeId)
                .HasColumnName("contact_message_type_id")
                .IsRequired()
                .HasComment("Identifiant unique du type de message");

            entity.Property(e => e.Priority)
                .HasColumnName("priority")
                .IsRequired()
                .HasDefaultValue(0)
                .HasComment("Niveau de priorité du type de message");

            entity.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .IsRequired()
                .HasDefaultValue(true)
                .HasComment("Le type est-il actif ?");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("Date de création");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                .HasComment("Date de mise à jour");

            entity.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .HasComment("Référence à l’utilisateur créateur");

            entity.Property(e => e.UpdatedBy)
                .HasColumnName("updated_by")
                .HasComment("Référence à l’utilisateur ayant mis à jour");
            entity.Property(e => e.KeyName)
                .HasColumnName("key_name")
                .HasMaxLength(50)
                .IsRequired()
                .HasComment("Clé technique unique pour identifier le type de message");

            // Relations
            entity.HasMany(e => e.Translations)
                .WithOne(t => t.ContactMessageType)
                .HasForeignKey(t => t.ContactMessageTypeId);

            entity.HasMany(e => e.ContactMessages)
                .WithOne(m => m.ContactMessageType)
                .HasForeignKey(m => m.ContactMessageTypeId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ContactMessageTypeTranslation>(entity =>
        {
            entity.ToTable("contact_message_type_translation");

            entity.HasKey(e => e.ContactMessageTypeTranslationId);

            entity.Property(e => e.ContactMessageTypeTranslationId)
                .HasColumnName("contact_message_type_translation_id")
                .IsRequired()
                .HasComment("Identifiant unique de la traduction");

            entity.Property(e => e.ContactMessageTypeId)
                .HasColumnName("contact_message_type_id")
                .IsRequired();

            entity.Property(e => e.LanguageId)
                .HasColumnName("language_id")
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnName("description");

            // Relations avec ContactMessageType
            entity.HasOne(e => e.ContactMessageType)
                .WithMany(t => t.Translations)
                .HasForeignKey(e => e.ContactMessageTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relations avec SpokenLanguage
            entity.HasOne(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        modelBuilder.Entity<LegalDocumentType>(entity =>
        {
            entity.ToTable("legal_document_type");

            entity.HasKey(e => e.DocumentTypeId);
            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasIndex(e => e.Name).HasDatabaseName("idx_name");
        });

        modelBuilder.Entity<LegalDocumentTypeTranslation>(entity =>
        {
            entity.ToTable("legal_document_type_translation");

            entity.HasKey(e => e.TranslationId);
            entity.Property(e => e.TranslationId).HasColumnName("translation_id");

            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id").IsRequired();
            entity.Property(e => e.LanguageId).HasColumnName("language_id").IsRequired();
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasIndex(e => new { e.DocumentTypeId, e.LanguageId }).IsUnique();

            entity.HasOne(e => e.DocumentType)
                .WithMany(d => d.Translations)
                .HasForeignKey(e => e.DocumentTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<LegalDocument>(entity =>
        {
            entity.ToTable("legal_document");

            entity.HasKey(e => e.DocumentId);
            entity.Property(e => e.DocumentId).HasColumnName("document_id");

            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id").IsRequired();
            entity.Property(e => e.Version).HasColumnName("version").HasMaxLength(20).IsRequired();
            entity.Property(e => e.PublishedAt).HasColumnName("published_at").IsRequired();
            entity.Property(e => e.ValidUntil).HasColumnName("valid_until");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);

            entity.HasIndex(e => e.IsActive).HasDatabaseName("idx_is_active");
            entity.HasIndex(e => e.DocumentTypeId).HasDatabaseName("idx_document_type_id");

            entity.HasOne(e => e.DocumentType)
                .WithMany(t => t.Documents)
                .HasForeignKey(e => e.DocumentTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LegalDocumentClause>(entity =>
        {
            entity.ToTable("legal_document_clause");

            entity.HasKey(e => e.ClauseId);
            entity.Property(e => e.ClauseId).HasColumnName("clause_id");

            entity.Property(e => e.DocumentId).HasColumnName("document_id").IsRequired();
            entity.Property(e => e.OrderIndex).HasColumnName("order_index").IsRequired();

            entity.HasIndex(e => new { e.DocumentId, e.OrderIndex }).HasDatabaseName("idx_document_order");

            entity.HasOne(e => e.Document)
                .WithMany(d => d.Clauses)
                .HasForeignKey(e => e.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<LegalDocumentClauseTranslation>(entity =>
        {
            entity.ToTable("legal_document_clause_translation");

            entity.HasKey(e => e.TranslationId);
            entity.Property(e => e.TranslationId).HasColumnName("translation_id");

            entity.Property(e => e.ClauseId).HasColumnName("clause_id").IsRequired();
            entity.Property(e => e.LanguageId).HasColumnName("language_id").IsRequired();
            entity.Property(e => e.Title).HasColumnName("title").HasMaxLength(255);
            entity.Property(e => e.Content).HasColumnName("content").IsRequired();

            entity.HasIndex(e => new { e.ClauseId, e.LanguageId }).IsUnique();

            entity.HasOne(e => e.Clause)
                .WithMany(c => c.Translations)
                .HasForeignKey(e => e.ClauseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserLegalConsent>(entity =>
        {
            entity.ToTable("user_legal_consent");

            entity.HasKey(e => e.ConsentId);
            entity.Property(e => e.ConsentId).HasColumnName("consent_id");

            entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(e => e.DocumentId).HasColumnName("document_id").IsRequired();
            entity.Property(e => e.DocumentVersion).HasColumnName("document_version").HasMaxLength(20).IsRequired();
            entity.Property(e => e.AcceptedAt).HasColumnName("accepted_at").IsRequired();
            entity.Property(e => e.IpAddress).HasColumnName("ip_address").HasMaxLength(45);
            entity.Property(e => e.UserAgentId).HasColumnName("user_agent_id");

            entity.Property(e => e.Revoked).HasColumnName("revoked").HasDefaultValue(false);
            entity.Property(e => e.RevokedAt).HasColumnName("revoked_at");
            entity.Property(e => e.RevokeReason).HasColumnName("revoke_reason").HasMaxLength(100);

            entity.HasIndex(e => new { e.UserId, e.DocumentId, e.Revoked }).HasDatabaseName("idx_user_document");

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Document)
                .WithMany(d => d.Consents)
                .HasForeignKey(e => e.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.UserAgent)
                .WithMany()
                .HasForeignKey(e => e.UserAgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });


        // SuperRole
        modelBuilder.Entity<SuperRole>(entity =>
        {
            entity.ToTable("super_role");
            entity.HasKey(e => e.SuperRoleId);
            entity.Property(e => e.SuperRoleId).HasColumnName("super_role_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_super_role_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_super_role_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // SessionEvents
        modelBuilder.Entity<SessionEvent>(entity =>
        {
            entity.ToTable("sessionEvents");
            entity.HasKey(se => se.SessionEventId).HasName("PK_sessionEvents");
            entity.Property(se => se.SessionEventId).HasColumnName("sessionEvent_id").ValueGeneratedOnAdd();
            entity.Property(se => se.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(se => se.EventType).HasColumnName("eventType").IsRequired().HasMaxLength(50)
                .HasColumnType("varchar(50)");
            entity.Property(se => se.IpAddress).HasColumnName("ipAddress").IsRequired().HasMaxLength(45)
                .HasColumnType("varchar(45)");
            entity.Property(se => se.EventTime).HasColumnName("eventTime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(se => se.User)
                .WithMany(u => u.SessionEvents)
                .HasForeignKey(se => se.UserId)
                .HasConstraintName("FK_sessionEvents_users")
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation inverse avec RefreshTokens
            entity.HasMany(se => se.RefreshTokens)
                .WithOne(rt => rt.SessionEvent)
                .HasForeignKey(rt => rt.SessionEventId)
                .HasConstraintName("FK_RefreshToken_SessionEvents")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.UserAgentId).HasColumnName("user_agent_id");
            entity.HasOne(e => e.UserAgent)
                .WithMany()
                .HasForeignKey(e => e.UserAgentId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Role
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role");
            entity.HasKey(e => e.RoleId);
            entity.Property(e => e.RoleId).HasColumnName("role_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(30);
            entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.EntityType).WithMany(e => e.Roles).HasForeignKey(e => e.EntityTypeId)
                .OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_role_entity_type");
            entity.HasIndex(e => new { e.Name, e.EntityTypeId }).IsUnique().HasDatabaseName("uk_role_name_entity");
        });

        modelBuilder.Entity<RoleTranslation>(entity =>
        {
            entity.ToTable("role_translations");
            entity.HasKey(e => e.RoleTranslationId);
            entity.Property(e => e.RoleTranslationId).HasColumnName("role_translation_id").ValueGeneratedOnAdd();
            entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired();
            entity.Property(e => e.LanguageId).HasColumnName("language_id").IsRequired();
            entity.Property(e => e.TranslatedName).HasColumnName("translated_name").IsRequired().HasMaxLength(30);
            entity.Property(e => e.TranslatedDescription).HasColumnName("translated_description");
            // Relation claire avec Role
            entity.HasOne(e => e.Role)
                .WithMany(r => r.Translations)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_role_translation");

            // Relation avec SpokenLanguage
            entity.HasOne(e => e.Language)
                .WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_role_translation_language");
        });


        // EntityType
        modelBuilder.Entity<EntityType>(entity =>
        {
            entity.ToTable("entity_types");
            entity.HasKey(e => e.EntityTypeId);
            entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<EntityTypeTranslation>(entity =>
        {
            entity.ToTable("entity_type_translations");
            entity.HasKey(e => e.EntityTypeTranslationId);
            entity.Property(e => e.EntityTypeTranslationId).HasColumnName("entity_type_translation_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id").IsRequired();
            entity.Property(e => e.LanguageId).HasColumnName("language_id").IsRequired();
            entity.Property(e => e.TranslatedName).HasColumnName("translated_name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.TranslatedDescription).HasColumnName("translated_description");

            entity.HasOne(e => e.EntityType).WithMany(e => e.Translations)
                .HasForeignKey(e => e.EntityTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_entity_type_translation_entity_type");

            entity.HasOne(e => e.Language).WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_entity_type_translation_language");

            entity.HasIndex(e => new { e.EntityTypeId, e.LanguageId }).IsUnique()
                .HasDatabaseName("uk_entity_type_lang");
        });
        //Invitation Types;
        modelBuilder.Entity<InvitationType>(entity =>
        {
            entity.ToTable("invitation_type");

            entity.HasKey(e => e.InvitationTypeId);

            entity.Property(e => e.InvitationTypeId)
                .HasColumnName("invitation_type_id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasMaxLength(255);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("NULL")
                .ValueGeneratedOnUpdate();

            entity.HasIndex(e => e.Name)
                .IsUnique()
                .HasDatabaseName("ux_invitation_type_name");
        });


        // SuperadminInvitation
        modelBuilder.Entity<SuperadminInvitation>(entity =>
        {
            entity.ToTable("superadmin_invitation");
            entity.HasKey(e => e.SuperadminInvitationId);
            entity.Property(e => e.SuperadminInvitationId).HasColumnName("superadmin_invitation_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(191);
            entity.Property(e => e.InvitationToken).HasColumnName("invitation_token").IsRequired().HasMaxLength(191);
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at").IsRequired();
            entity.Property(e => e.Used).HasColumnName("used").HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.InvitationTypeId)
                .HasColumnName("invitation_type_id")
                .IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.InvitationToken).IsUnique();
            entity.HasOne(e => e.CreatedByUser).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_invitation_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.InvitationType)
                .WithMany(t => t.Invitations)
                .HasForeignKey(e => e.InvitationTypeId)
                .HasConstraintName("fk_invitation_type")
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Entity>(entity =>
        {
            // Nom de la table
            entity.ToTable("entities");

            // Clé primaire
            entity.HasKey(e => e.EntityId);

            // Propriétés
            entity.Property(e => e.EntityId).HasColumnName("entity_id").ValueGeneratedOnAdd();
            entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id").IsRequired();
            entity.Property(e => e.SpecificEntityId).HasColumnName("specific_entity_id").IsRequired();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(191);
            entity.Property(e => e.ParentEntityId).HasColumnName("parent_entity_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");

            // Relations
            entity.HasOne(e => e.EntityType)
                .WithMany(et => et.Entities)
                .HasForeignKey(e => e.EntityTypeId)
                .HasConstraintName("fk_entities_type")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ParentEntity)
                .WithMany(e => e.ChildEntities)
                .HasForeignKey(e => e.ParentEntityId)
                .HasConstraintName("fk_entities_parent")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.CreatedByUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_entities_created_by")
                .OnDelete(DeleteBehavior.SetNull);

            // Configuration du moteur (optionnel, pour information)
            entity.ToTable(t => t.HasComment("ENGINE=InnoDB"));
        });

        // SuperadminInvitationEntity
        modelBuilder.Entity<SuperadminInvitationEntity>(entity =>
        {
            entity.ToTable("superadmin_invitation_entity");
            entity.HasKey(e => e.SuperadminInvitationEntityId);
            entity.Property(e => e.SuperadminInvitationEntityId).HasColumnName("superadmin_invitation_entity_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.SuperadminInvitationId).HasColumnName("superadmin_invitation_id").IsRequired();
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(e => e.SuperadminInvitation).WithOne(s => s.SuperadminInvitationEntity)
                .HasForeignKey<SuperadminInvitationEntity>(e => e.SuperadminInvitationId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .HasPrincipalKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_invitation_entity_entity");
            entity.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(e => e.SuperadminInvitationId).IsUnique();
        });

        // EntityUser
        modelBuilder.Entity<EntityUser>(entity =>
        {
            entity.ToTable("entity_user");
            entity.HasKey(e => e.EntityUserId);
            entity.Property(e => e.EntityUserId).HasColumnName("entity_user_id").ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.RoleId).HasColumnName("role_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasIndex(e => e.UserId).IsUnique();
            entity.HasOne(e => e.User).WithOne(u => u.EntityUser).HasForeignKey<EntityUser>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade).HasConstraintName("fk_entity_user_user");
            entity.HasOne(e => e.Role).WithMany(r => r.EntityUsers).HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict).HasConstraintName("fk_entity_user_role");
            entity.HasOne(e => e.Entity)
                .WithMany(en => en.EntityUsers) // Assurez-vous que la classe Entity possède cette collection.
                .HasForeignKey(e => e.EntityId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_entity_user_entity");
        });

        // InstitutionType
        modelBuilder.Entity<InstitutionType>(entity =>
        {
            entity.ToTable("institution_type");
            entity.HasKey(e => e.InstitutionTypeId);
            entity.Property(e => e.InstitutionTypeId)
                .HasColumnName("institution_type_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Description) // Added description property
                .HasColumnName("description")
                .HasMaxLength(191)
                .IsRequired(false); // NULL allowed
            entity.HasIndex(e => e.Name)
                .IsUnique();
        });


        modelBuilder.Entity<InstitutionTypeTranslation>(entity =>
        {
            entity.ToTable("institution_type_translations");
            entity.HasKey(e => e.InstitutionTypeTranslationId);
            entity.Property(e => e.InstitutionTypeTranslationId).HasColumnName("institution_type_translation_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.InstitutionTypeId).HasColumnName("institution_type_id").IsRequired();
            entity.Property(e => e.LanguageId).HasColumnName("language_id").IsRequired();
            entity.Property(e => e.TranslatedName).HasColumnName("translated_name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.TranslatedDescription).HasColumnName("translated_description");

            entity.HasOne(e => e.InstitutionType).WithMany(e => e.Translations)
                .HasForeignKey(e => e.InstitutionTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_institution_type_translation_institution_type");

            entity.HasOne(e => e.Language).WithMany()
                .HasForeignKey(e => e.LanguageId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_institution_type_translation_language");

            entity.HasIndex(e => new { e.InstitutionTypeId, e.LanguageId }).IsUnique()
                .HasDatabaseName("uk_institution_type_lang");
        });

        // Community
        modelBuilder.Entity<Community>(entity =>
        {
            entity.ToTable("community");
            entity.HasKey(e => e.CommunityId);
            entity.Property(e => e.CommunityId).HasColumnName("community_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // LegalStatus
        modelBuilder.Entity<LegalStatus>(entity =>
        {
            entity.ToTable("legal_status");
            entity.HasKey(e => e.LegalStatusId);
            entity.Property(e => e.LegalStatusId).HasColumnName("legal_status_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // EducationLevel
        modelBuilder.Entity<EducationLevel>(entity =>
        {
            entity.ToTable("education_level");
            entity.HasKey(e => e.EducationLevelId);
            entity.Property(e => e.EducationLevelId).HasColumnName("education_level_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Network
        modelBuilder.Entity<Network>(entity =>
        {
            entity.ToTable("network");
            entity.HasKey(e => e.NetworkId);
            entity.Property(e => e.NetworkId).HasColumnName("network_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // Authority
        modelBuilder.Entity<Authority>(entity =>
        {
            entity.ToTable("authority");
            entity.HasKey(e => e.AuthorityId);
            entity.Property(e => e.AuthorityId).HasColumnName("authority_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // CampusType
        modelBuilder.Entity<CampusType>(entity =>
        {
            entity.ToTable("campus_type");
            entity.HasKey(e => e.CampusTypeId);
            entity.Property(e => e.CampusTypeId).HasColumnName("campus_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // CompanyType
        modelBuilder.Entity<CompanyType>(entity =>
        {
            entity.ToTable("company_type");
            entity.HasKey(e => e.CompanyTypeId);
            entity.Property(e => e.CompanyTypeId).HasColumnName("company_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Acronym).HasColumnName("acronym").HasMaxLength(10);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // StudyLevel
        modelBuilder.Entity<StudyLevel>(entity =>
        {
            entity.ToTable("study_level");
            entity.HasKey(e => e.StudyLevelId);
            entity.Property(e => e.StudyLevelId).HasColumnName("study_level_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_study_level_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_study_level_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // OwnerType
        modelBuilder.Entity<OwnerType>(entity =>
        {
            entity.ToTable("owner_type");
            entity.HasKey(e => e.OwnerTypeId);
            entity.Property(e => e.OwnerTypeId).HasColumnName("owner_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // HousingType
        modelBuilder.Entity<HousingType>(entity =>
        {
            entity.ToTable("housing_type");
            entity.HasKey(e => e.HousingTypeId);
            entity.Property(e => e.HousingTypeId).HasColumnName("housing_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // PebRating
        modelBuilder.Entity<PebRating>(entity =>
        {
            entity.ToTable("peb_rating");
            entity.HasKey(e => e.PebRatingId);
            entity.Property(e => e.PebRatingId).HasColumnName("peb_rating_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(10);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // ApplicationStatus
        modelBuilder.Entity<ApplicationStatus>(entity =>
        {
            entity.ToTable("application_status");
            entity.HasKey(e => e.ApplicationStatusId);
            entity.Property(e => e.ApplicationStatusId).HasColumnName("application_status_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_application_status_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_application_status_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // OfferType
        modelBuilder.Entity<OfferType>(entity =>
        {
            entity.ToTable("offer_type");
            entity.HasKey(e => e.OfferTypeId);
            entity.Property(e => e.OfferTypeId).HasColumnName("offer_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_offer_type_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_offer_type_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // ContractType
        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.ToTable("contract_type");
            entity.HasKey(e => e.ContractTypeId);
            entity.Property(e => e.ContractTypeId).HasColumnName("contract_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_contract_type_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_contract_type_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // ScheduleType
        modelBuilder.Entity<ScheduleType>(entity =>
        {
            entity.ToTable("schedule_type");
            entity.HasKey(e => e.ScheduleTypeId);
            entity.Property(e => e.ScheduleTypeId).HasColumnName("schedule_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // SpokenLanguage
        modelBuilder.Entity<SpokenLanguage>(entity =>
        {
            entity.ToTable("spoken_language");

            entity.HasKey(e => e.LanguageId);
            entity.Property(e => e.LanguageId).HasColumnName("language_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Code).HasColumnName("code").IsRequired().HasMaxLength(5);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasIndex(e => e.Name).IsUnique();

            entity.HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_spoken_language_created_by")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Updater)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_spoken_language_updated_by")
                .OnDelete(DeleteBehavior.SetNull);

            
        });

   

        // DurationUnit
        modelBuilder.Entity<DurationUnit>(entity =>
        {
            entity.ToTable("duration_unit");
            entity.HasKey(e => e.DurationUnitId);
            entity.Property(e => e.DurationUnitId).HasColumnName("duration_unit_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(20);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_duration_unit_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_duration_unit_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // FacilityType
        modelBuilder.Entity<FacilityType>(entity =>
        {
            entity.ToTable("facility_type");
            entity.HasKey(e => e.FacilityTypeId);
            entity.Property(e => e.FacilityTypeId).HasColumnName("facility_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description");
        });

        // DegreeCategory
        modelBuilder.Entity<DegreeCategory>(entity =>
        {
            entity.ToTable("degree_category");
            entity.HasKey(e => e.DegreeCategoryId);
            entity.Property(e => e.DegreeCategoryId).HasColumnName("degree_category_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
        });

        // TuitionType
        modelBuilder.Entity<TuitionType>(entity =>
        {
            entity.ToTable("tuition_type");
            entity.HasKey(e => e.TuitionTypeId);
            entity.Property(e => e.TuitionTypeId).HasColumnName("tuition_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // Cycle
        modelBuilder.Entity<Cycle>(entity =>
        {
            entity.ToTable("cycle");
            entity.HasKey(e => e.CycleId);
            entity.Property(e => e.CycleId).HasColumnName("cycle_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
        });

        // DegreeType
        modelBuilder.Entity<DegreeType>(entity =>
        {
            entity.ToTable("degree_type");
            entity.HasKey(e => e.DegreeTypeId);
            entity.Property(e => e.DegreeTypeId).HasColumnName("degree_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CycleId).HasColumnName("cycle_id");
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(255);
            entity.HasOne(e => e.Cycle).WithMany(e => e.DegreeTypes).HasForeignKey(e => e.CycleId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // CertificationType
        modelBuilder.Entity<CertificationType>(entity =>
        {
            entity.ToTable("certification_type");
            entity.HasKey(e => e.CertificationTypeId);
            entity.Property(e => e.CertificationTypeId).HasColumnName("certification_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // DeliveryMode
        modelBuilder.Entity<DeliveryMode>(entity =>
        {
            entity.ToTable("delivery_mode");
            entity.HasKey(e => e.DeliveryModeId);
            entity.Property(e => e.DeliveryModeId).HasColumnName("delivery_mode_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // MimeType
        modelBuilder.Entity<MimeType>(entity =>
        {
            entity.ToTable("mime_type");
            entity.HasKey(e => e.MimeTypeId);
            entity.Property(e => e.MimeTypeId).HasColumnName("mime_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // Amenity
        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.ToTable("amenity");
            entity.HasKey(e => e.AmenityId);
            entity.Property(e => e.AmenityId).HasColumnName("amenity_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // Location
        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("location");
            entity.HasKey(e => e.LocationId);
            entity.Property(e => e.LocationId).HasColumnName("location_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Street).HasColumnName("street").IsRequired().HasMaxLength(191);
            entity.Property(e => e.Number).HasColumnName("number").IsRequired().HasMaxLength(10);
            entity.Property(e => e.PostalCode).HasColumnName("postal_code").IsRequired().HasMaxLength(10);
            entity.Property(e => e.City).HasColumnName("city").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Country).HasColumnName("country").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Latitude).HasColumnName("latitude").HasColumnType("DECIMAL(9,6)");
            entity.Property(e => e.Longitude).HasColumnName("longitude").HasColumnType("DECIMAL(9,6)");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_location_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_location_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // Users
        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId).HasColumnName("user_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(191);
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired().HasMaxLength(191);
            entity.Property(e => e.FirstName).HasColumnName("first_name").HasMaxLength(100);
            entity.Property(e => e.LastName).HasColumnName("last_name").HasMaxLength(100);
            entity.Property(e => e.SuperRoleId).HasColumnName("super_role_id").IsRequired();
            entity.Property(e => e.IsVerified).HasColumnName("is_verified").HasDefaultValue(false);
            entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasOne(e => e.SuperRole).WithMany(e => e.Users).HasForeignKey(e => e.SuperRoleId)
                .HasConstraintName("fk_user_super_role").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_users_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_users_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(u => u.EntityUser).WithOne(eu => eu.User).HasForeignKey<EntityUser>(eu => eu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LoginAttempt>(entity =>
        {
            entity.ToTable("login_attempt");
            entity.HasKey(e => e.LoginAttemptId);
            entity.Property(e => e.LoginAttemptId).HasColumnName("login_attempt_id").ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(191);
            entity.Property(e => e.IpAddress).HasColumnName("ip_address").IsRequired().HasMaxLength(45);
            entity.Property(e => e.AttemptTime).HasColumnName("attempt_time").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.LockedUntil).HasColumnName("locked_until");
            entity.Property(e => e.Success).HasColumnName("success").HasDefaultValue(false);

            // Relation avec User
            entity.HasOne(e => e.User)
                .WithMany(u => u.LoginAttempts)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_login_attempt_user")
                .OnDelete(DeleteBehavior.SetNull); // OnDelete behavior en cas de suppression de User

            // Relation avec UserAgent
            entity.Property(e => e.UserAgentId).HasColumnName("user_agent_id");
            entity.HasOne(e => e.UserAgent)
                .WithMany() // Ajoutez la navigation inverse dans UserAgent
                .HasForeignKey(e => e.UserAgentId)
                .OnDelete(DeleteBehavior.SetNull);

            // Index pour améliorer les recherches
            entity.HasIndex(e => new { e.Email, e.AttemptTime }).HasDatabaseName("ix_login_attempt_email_attempttime");
        });


        // Contact
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("contact");
            entity.HasKey(e => e.ContactId);
            entity.Property(e => e.ContactId).HasColumnName("contact_id").ValueGeneratedOnAdd();
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Phone).HasColumnName("phone").HasMaxLength(20);
            entity.Property(e => e.ContactEmail).HasColumnName("contact_email").HasMaxLength(191);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Location)
                .WithMany(l => l.Contacts)
                .HasForeignKey(e => e.LocationId)
                .HasConstraintName("fk_contact_location")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Creator)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_contact_created_by")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Updater)
                .WithMany()
                .HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_contact_updated_by")
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Student
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("student");
            entity.HasKey(e => e.StudentId);
            entity.Property(e => e.StudentId)
                .HasColumnName("student_id")
                .ValueGeneratedOnAdd();
            // Correction du nom de colonne sans espace en fin
            entity.Property(e => e.EntityId)
                .HasColumnName("entity_id")
                .IsRequired();
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Bio).HasColumnName("bio").HasColumnType("TEXT");
            entity.Property(e => e.Linkedin).HasColumnName("linkedin").HasMaxLength(191);
            entity.Property(e => e.Github).HasColumnName("github").HasMaxLength(191);
            entity.Property(e => e.Portfolio).HasColumnName("portfolio").HasMaxLength(191);
            entity.Property(e => e.ExpectedGraduationYear).HasColumnName("expected_graduation_year");
            entity.Property(e => e.PreferredJobType).HasColumnName("preferred_job_type")
                .HasConversion(v => v.ToString(), v => (PreferredJobType)Enum.Parse(typeof(PreferredJobType), v));
            entity.Property(e => e.PreferredLocation).HasColumnName("preferred_location").HasMaxLength(100);
            entity.Property(e => e.NotificationEnabled).HasColumnName("notification_enabled").HasDefaultValue(true);
            entity.Property(e => e.StudyField).HasColumnName("study_field").HasMaxLength(100);
            entity.Property(e => e.CvPath).HasColumnName("cv_path").HasMaxLength(191);
            entity.Property(e => e.StudyLevelId).HasColumnName("study_level_id");
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();

            // Association avec la table Entities
            entity.HasOne(e => e.Entity)
                .WithMany() // ou .WithMany(e => e.Students) si vous avez une collection dans Entity
                .HasForeignKey(e => e.EntityId)
                .HasConstraintName("fk_student_entity")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.StudyLevel)
                .WithMany(sl => sl.Students)
                .HasForeignKey(e => e.StudyLevelId)
                .HasConstraintName("fk_student_study_level")
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(e => e.Contact)
                .WithMany(c => c.Students)
                .HasForeignKey(e => e.ContactId)
                .HasConstraintName("fk_student_contact")
                .OnDelete(DeleteBehavior.SetNull);

            // Autres relations (CVs, Applications, etc.)
            entity.HasMany(e => e.CVs).WithOne(cv => cv.Student).HasForeignKey(cv => cv.StudentId);
            entity.HasMany(e => e.HousingApplications).WithOne(ha => ha.Student).HasForeignKey(ha => ha.StudentId);
            entity.HasMany(e => e.HousingVisits).WithOne(hv => hv.Student).HasForeignKey(hv => hv.StudentId);
            entity.HasMany(e => e.Applications).WithOne(a => a.Student).HasForeignKey(a => a.StudentId);
            entity.HasMany(e => e.OfferFavorites).WithOne(of => of.Student).HasForeignKey(of => of.StudentId);
            entity.HasMany(e => e.WorkHours).WithOne(wh => wh.Student).HasForeignKey(wh => wh.StudentId);
            entity.HasMany(e => e.EntityReviews).WithOne(er => er.Student).HasForeignKey(er => er.StudentId);
            entity.HasMany(e => e.ReferralsAsReferring).WithOne(sr => sr.ReferringStudent)
                .HasForeignKey(sr => sr.ReferringStudentId).OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(e => e.ReferralsAsReferred).WithOne(sr => sr.ReferredStudent)
                .HasForeignKey(sr => sr.ReferredStudentId).OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(e => e.EventInterests).WithOne(ei => ei.Student).HasForeignKey(ei => ei.StudentId);
            entity.HasMany(e => e.EntityLikes).WithOne(el => el.Student).HasForeignKey(el => el.StudentId);
        });


        // StudentFriendship
        modelBuilder.Entity<StudentFriendship>(entity =>
        {
            entity.ToTable("student_friendship");
            entity.HasKey(e => e.StudentFriendshipId);
            entity.Property(e => e.StudentFriendshipId).HasColumnName("student_friendship_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId1).HasColumnName("student_id_1").IsRequired();
            entity.Property(e => e.StudentId2).HasColumnName("student_id_2").IsRequired();
            entity.Property(e => e.Status).HasColumnName("status").IsRequired().HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.RequestDate).HasColumnName("request_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ResponseDate).HasColumnName("response_date");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_student_friendship_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_student_friendship_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => new { e.StudentId1, e.StudentId2 }).IsUnique();
            entity.ToTable(t =>
                t.HasCheckConstraint("CK_StudentFriendship_Status", "status IN ('Pending', 'Accepted', 'Rejected')"));
            entity.ToTable(t => t.HasCheckConstraint("chk_not_self_friendship", "student_id_1 != student_id_2"));
        });

        // CV
        modelBuilder.Entity<CV>(entity =>
        {
            entity.ToTable("cv");
            entity.HasKey(e => e.CvId);
            entity.Property(e => e.CvId).HasColumnName("cv_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Objective).HasColumnName("objective");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(false);
            entity.Property(e => e.IsDefault).HasColumnName("is_default").HasDefaultValue(false);
            entity.HasOne(e => e.Student).WithMany(e => e.CVs).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_cv_student").OnDelete(DeleteBehavior.Cascade);
        });

        // Experience
        modelBuilder.Entity<Experience>(entity =>
        {
            entity.ToTable("experience");
            entity.HasKey(e => e.ExperienceId);
            entity.Property(e => e.ExperienceId).HasColumnName("experience_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CvId).HasColumnName("cv_id").IsRequired();
            entity.Property(e => e.JobTitle).HasColumnName("job_title").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Company).HasColumnName("company").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Location).HasColumnName("location").HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnName("start_date").IsRequired();
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.HasOne(e => e.CV).WithMany(e => e.Experiences).HasForeignKey(e => e.CvId)
                .HasConstraintName("fk_experience_cv").OnDelete(DeleteBehavior.Cascade);
        });

        // Education
        modelBuilder.Entity<Education>(entity =>
        {
            entity.ToTable("education");
            entity.HasKey(e => e.EducationId);
            entity.Property(e => e.EducationId).HasColumnName("education_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CvId).HasColumnName("cv_id").IsRequired();
            entity.Property(e => e.Degree).HasColumnName("degree").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Institution).HasColumnName("institution").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Location).HasColumnName("location").HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnName("start_date").IsRequired();
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.HasOne(e => e.CV).WithMany(e => e.Educations).HasForeignKey(e => e.CvId)
                .HasConstraintName("fk_education_cv").OnDelete(DeleteBehavior.Cascade);
        });

        // CVSkill
        modelBuilder.Entity<CVSkill>(entity =>
        {
            entity.ToTable("cv_skill");
            entity.HasKey(e => e.CvSkillId);
            entity.Property(e => e.CvSkillId).HasColumnName("cv_skill_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CvId).HasColumnName("cv_id").IsRequired();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Proficiency).HasColumnName("proficiency").IsRequired().HasMaxLength(20)
                .HasDefaultValue("Intermédiaire");
            entity.HasOne(e => e.CV).WithMany(e => e.Skills).HasForeignKey(e => e.CvId)
                .HasConstraintName("fk_cv_skill_cv").OnDelete(DeleteBehavior.Cascade);
            entity.ToTable(t => t.HasCheckConstraint("CK_CVSkill_Proficiency",
                "proficiency IN ('Débutant', 'Intermédiaire', 'Avancé', 'Expert')"));
        });

        // CVLanguage
        modelBuilder.Entity<CVLanguage>(entity =>
        {
            entity.ToTable("cv_language");
            entity.HasKey(e => e.CvLanguageId);
            entity.Property(e => e.CvLanguageId).HasColumnName("cv_language_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CvId).HasColumnName("cv_id").IsRequired();
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Level).HasColumnName("level").IsRequired().HasMaxLength(10).HasDefaultValue("B1");
            entity.HasOne(e => e.CV).WithMany(e => e.Languages).HasForeignKey(e => e.CvId)
                .HasConstraintName("fk_cv_language_cv").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Language).WithMany(e => e.CVLanguages).HasForeignKey(e => e.LanguageId)
                .HasConstraintName("fk_cv_language_language").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t =>
                t.HasCheckConstraint("CK_CVLanguage_Level", "level IN ('A1', 'A2', 'B1', 'B2', 'C1', 'C2')"));
        });

        // HousingOwner
        modelBuilder.Entity<HousingOwner>(entity =>
        {
            entity.ToTable("housing_owner");
            entity.HasKey(e => e.HousingOwnerId);
            entity.Property(e => e.HousingOwnerId).HasColumnName("housing_owner_id").ValueGeneratedOnAdd();
            entity.Property(e => e.OwnerType).HasColumnName("owner_type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasMany(e => e.Housings).WithOne(h => h.HousingOwner).HasForeignKey(h => h.HousingOwnerId);
        });

        // EventOwner
        modelBuilder.Entity<EventOwner>(entity =>
        {
            entity.ToTable("event_owner");
            entity.HasKey(e => e.EventOwnerId);
            entity.Property(e => e.EventOwnerId).HasColumnName("event_owner_id").ValueGeneratedOnAdd();
            entity.Property(e => e.OwnerType).HasColumnName("owner_type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasMany(e => e.Events).WithOne(e => e.EventOwner).HasForeignKey(e => e.EventOwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Institution
        modelBuilder.Entity<Institution>(entity =>
        {
            entity.ToTable("institution");
            entity.HasKey(e => e.InstitutionId);
            entity.Property(e => e.InstitutionId).HasColumnName("institution_id").ValueGeneratedOnAdd();
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.EventOwnerId).HasColumnName("event_owner_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Acronym).HasColumnName("acronym").HasMaxLength(10);
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.Website).HasColumnName("website").HasMaxLength(191);
            entity.Property(e => e.Logo).HasColumnName("logo").HasMaxLength(191);
            entity.Property(e => e.InstitutionTypeId).HasColumnName("institution_type_id").IsRequired();
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.LegalStatusId).HasColumnName("legal_status_id");
            entity.Property(e => e.NetworkId).HasColumnName("network_id");
            entity.Property(e => e.OfficialCode).HasColumnName("official_code").HasMaxLength(20);
            entity.Property(e => e.FoundingDate).HasColumnName("founding_date");
            entity.Property(e => e.StudentCount).HasColumnName("student_count");
            entity.Property(e => e.IsOfficiallyRecognized).HasColumnName("is_officially_recognized")
                .HasDefaultValue(true);
            entity.Property(e => e.AuthorityId).HasColumnName("authority_id");
            entity.Property(e => e.EducationLevelId).HasColumnName("education_level_id");
            entity.Property(e => e.IsModular).HasColumnName("is_modular").HasDefaultValue(false);
            entity.Property(e => e.TargetAudience).HasColumnName("target_audience").HasMaxLength(50);
            entity.Property(e => e.AverageTuitionFee).HasColumnName("average_tuition_fee")
                .HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.LikeCount).HasColumnName("like_count").HasDefaultValue(0);
            entity.Property(e => e.VisitCount).HasColumnName("visit_count").HasDefaultValue(0);
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Entity).WithMany().HasForeignKey(e => e.EntityId)
                .HasConstraintName("fk_institution_entity").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.EventOwner).WithOne().HasForeignKey<Institution>(e => e.EventOwnerId)
                .HasConstraintName("fk_institution_owner").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.InstitutionType).WithMany(it => it.Institutions)
                .HasForeignKey(e => e.InstitutionTypeId).HasConstraintName("fk_institution_type")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Community).WithMany(c => c.Institutions).HasForeignKey(e => e.CommunityId)
                .HasConstraintName("fk_institution_community").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.LegalStatus).WithMany(ls => ls.Institutions).HasForeignKey(e => e.LegalStatusId)
                .HasConstraintName("fk_institution_legal_status").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Network).WithMany(n => n.Institutions).HasForeignKey(e => e.NetworkId)
                .HasConstraintName("fk_institution_network").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Authority).WithMany(a => a.Institutions).HasForeignKey(e => e.AuthorityId)
                .HasConstraintName("fk_institution_authority").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.EducationLevel).WithMany(el => el.Institutions).HasForeignKey(e => e.EducationLevelId)
                .HasConstraintName("fk_institution_education_level").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Contact).WithMany(c => c.Institutions).HasForeignKey(e => e.ContactId)
                .HasConstraintName("fk_institution_contact").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany(u => u.CreatedInstitutions).HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_institution_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany(u => u.UpdatedInstitutions).HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_institution_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => e.EventOwnerId).IsUnique().HasFilter("[event_owner_id] IS NOT NULL");
            entity.HasIndex(e => e.OfficialCode).IsUnique().HasFilter("[official_code] IS NOT NULL");
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Institution_StudentCount", "student_count >= 0 OR student_count IS NULL");
                t.HasCheckConstraint("CK_Institution_AverageTuitionFee",
                    "average_tuition_fee >= 0 OR average_tuition_fee IS NULL");
                t.HasCheckConstraint("CK_Institution_LikeCount", "like_count >= 0");
                t.HasCheckConstraint("CK_Institution_VisitCount", "visit_count >= 0");
            });
        });

        // Campus
        modelBuilder.Entity<Campus>(entity =>
        {
            entity.ToTable("campus");
            entity.HasKey(e => e.CampusId);
            entity.Property(e => e.CampusId).HasColumnName("campus_id").ValueGeneratedOnAdd();
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.EventOwnerId).HasColumnName("event_owner_id");
            entity.Property(e => e.HousingOwnerId).HasColumnName("housing_owner_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Acronym).HasColumnName("acronym").HasMaxLength(10);
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.OfficialCode).HasColumnName("official_code").HasMaxLength(20);
            entity.Property(e => e.OpeningDate).HasColumnName("opening_date");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Area).HasColumnName("area").HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.LikeCount).HasColumnName("like_count").HasDefaultValue(0);
            entity.Property(e => e.Rating).HasColumnName("rating").HasColumnType("DECIMAL(3,2)");
            entity.Property(e => e.VisitCount).HasColumnName("visit_count").HasDefaultValue(0);
            entity.Property(e => e.Logo).HasColumnName("logo").HasMaxLength(191);
            entity.Property(e => e.UseInstitutionData).HasColumnName("use_institution_data").HasDefaultValue(true);
            entity.Property(e => e.CampusTypeId).HasColumnName("campus_type_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Entity).WithMany().HasForeignKey(e => e.EntityId).HasConstraintName("fk_campus_entity")
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.EventOwner).WithOne().HasForeignKey<Campus>(e => e.EventOwnerId)
                .HasConstraintName("fk_campus_owner").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.HousingOwner).WithOne().HasForeignKey<Campus>(e => e.HousingOwnerId)
                .HasConstraintName("fk_campus_housing_owner").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.CampusType).WithMany(e => e.Campuses).HasForeignKey(e => e.CampusTypeId)
                .HasConstraintName("fk_campus_type").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Contact).WithMany(e => e.Campuses).HasForeignKey(e => e.ContactId)
                .HasConstraintName("fk_campus_contact").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_campus_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_campus_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => e.OfficialCode).IsUnique().HasFilter("[official_code] IS NOT NULL");
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Campus_Capacity", "capacity >= 0 OR capacity IS NULL");
                t.HasCheckConstraint("CK_Campus_Area", "area >= 0 OR area IS NULL");
                t.HasCheckConstraint("CK_Campus_LikeCount", "like_count >= 0");
                t.HasCheckConstraint("CK_Campus_Rating", "rating BETWEEN 0 AND 5 OR rating IS NULL");
                t.HasCheckConstraint("CK_Campus_VisitCount", "visit_count >= 0");
            });
        });

        // Company
        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("company");
            entity.HasKey(e => e.CompanyId);
            entity.Property(e => e.CompanyId).HasColumnName("company_id").ValueGeneratedOnAdd();
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.EventOwnerId).HasColumnName("event_owner_id");
            entity.Property(e => e.HousingOwnerId).HasColumnName("housing_owner_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Acronym).HasColumnName("acronym").HasMaxLength(10);
            entity.Property(e => e.CompanyNumber).HasColumnName("company_number").HasMaxLength(12).IsRequired();
            entity.Property(e => e.Sector).HasColumnName("sector").HasMaxLength(50);
            entity.Property(e => e.Website).HasColumnName("website").HasMaxLength(191);
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.CollaboratorCount).HasColumnName("collaborator_count");
            entity.Property(e => e.Logo).HasColumnName("logo").HasMaxLength(191);
            entity.Property(e => e.ResponsibleUserId).HasColumnName("responsible_user_id");
            entity.Property(e => e.CompanyTypeId).HasColumnName("company_type_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.FoundingDate).HasColumnName("founding_date");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.LikeCount).HasColumnName("like_count").HasDefaultValue(0);
            entity.Property(e => e.Rating).HasColumnName("rating").HasColumnType("DECIMAL(3,2)");
            entity.Property(e => e.VisitCount).HasColumnName("visit_count").HasDefaultValue(0);
            entity.Property(e => e.ContactId).HasColumnName("contact_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Entity).WithMany().HasForeignKey(e => e.EntityId)
                .HasConstraintName("fk_company_entity").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.EventOwner).WithOne().HasForeignKey<Company>(e => e.EventOwnerId)
                .HasConstraintName("fk_company_event_owner").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.HousingOwner).WithOne().HasForeignKey<Company>(e => e.HousingOwnerId)
                .HasConstraintName("fk_company_housing_owner").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ResponsibleUser).WithMany().HasForeignKey(e => e.ResponsibleUserId)
                .HasConstraintName("fk_company_responsible_user").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.CompanyType).WithMany(ct => ct.Companies).HasForeignKey(e => e.CompanyTypeId)
                .HasConstraintName("fk_company_type").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Contact).WithMany(c => c.Companies).HasForeignKey(e => e.ContactId)
                .HasConstraintName("fk_company_contact").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_company_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_company_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasMany(e => e.Offers).WithOne(o => o.Company).HasForeignKey(o => o.CompanyId);
            entity.HasMany(e => e.CompanyInvitations).WithOne(ci => ci.Company).HasForeignKey(ci => ci.CompanyId);
            entity.HasMany(e => e.DegreePartnerships).WithOne(dp => dp.Company).HasForeignKey(dp => dp.CompanyId);
            entity.HasIndex(e => e.EventOwnerId).IsUnique().HasFilter("[event_owner_id] IS NOT NULL");
            entity.HasIndex(e => e.HousingOwnerId).IsUnique().HasFilter("[housing_owner_id] IS NOT NULL");
            entity.HasIndex(e => e.CompanyNumber).IsUnique().HasFilter("[company_number] IS NOT NULL");
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Company_Capacity", "capacity >= 0 OR capacity IS NULL");
                t.HasCheckConstraint("CK_Company_CollaboratorCount",
                    "collaborator_count >= 0 OR collaborator_count IS NULL");
                t.HasCheckConstraint("CK_Company_LikeCount", "like_count >= 0");
                t.HasCheckConstraint("CK_Company_Rating", "rating BETWEEN 0 AND 5 OR rating IS NULL");
                t.HasCheckConstraint("CK_Company_VisitCount", "visit_count >= 0");
            });
        });

        // Landlord
        modelBuilder.Entity<Landlord>(entity =>
        {
            // Configuration de la table et des contraintes CHECK
            entity.ToTable("landlord", t =>
            {
                t.HasCheckConstraint("CK_landlord_housing_count", "housing_count >= 0");
                t.HasCheckConstraint("CK_landlord_rating", "rating >= 0 AND rating <= 5");
            });

            // Clé primaire et colonnes
            entity.HasKey(e => e.LandlordId).HasName("PK_landlord");
            entity.Property(e => e.LandlordId)
                .HasColumnName("landlord_id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.EntityId)
                .HasColumnName("entity_id")
                .IsRequired();

            entity.Property(e => e.HousingOwnerId)
                .HasColumnName("housing_owner_id");

            entity.Property(e => e.ContactId)
                .HasColumnName("contact_id");

            entity.Property(e => e.HousingCount)
                .HasColumnName("housing_count")
                .HasDefaultValue(0);

            entity.Property(e => e.Rating)
                .HasColumnName("rating")
                .HasColumnType("decimal(3,2)");

            entity.Property(e => e.IsActive)
                .HasColumnName("is_active")
                .HasDefaultValue(true);

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            // Configuration des relations
            entity.HasOne(e => e.Entity)
                .WithMany()
                .HasForeignKey(e => e.EntityId)
                .HasConstraintName("fk_landlord_entity")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.HousingOwner)
                .WithOne()
                .HasForeignKey<Landlord>(e => e.HousingOwnerId)
                .HasConstraintName("fk_landlord_owner")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Contact)
                .WithMany(c => c.Landlords) // Assurez-vous que la classe Contact possède une collection Landlords
                .HasForeignKey(e => e.ContactId)
                .HasConstraintName("fk_landlord_contact")
                .OnDelete(DeleteBehavior.SetNull);

            // Index unique pour housing_owner_id
            entity.HasIndex(e => e.HousingOwnerId)
                .IsUnique();
        });


        // InstitutionInvitation
        modelBuilder.Entity<InstitutionInvitation>(entity =>
        {
            entity.ToTable("institution_invitation");
            entity.HasKey(e => e.InstitutionInvitationId);
            entity.Property(e => e.InstitutionInvitationId).HasColumnName("institution_invitation_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.InstitutionId).HasColumnName("institution_id").IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(191);
            entity.Property(e => e.InviteCode).HasColumnName("invite_code").IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Institution).WithMany(e => e.InstitutionInvitations)
                .HasForeignKey(e => e.InstitutionId).OnDelete(DeleteBehavior.Cascade);
        });

        // InstitutionCampusInvitation
        modelBuilder.Entity<InstitutionCampusInvitation>(entity =>
        {
            entity.ToTable("institution_campus_invitation");
            entity.HasKey(e => e.InstitutionCampusInvitationId);
            entity.Property(e => e.InstitutionCampusInvitationId).HasColumnName("institution_campus_invitation_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.InstitutionId).HasColumnName("institution_id").IsRequired();
            entity.Property(e => e.CampusId).HasColumnName("campus_id").IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(191);
            entity.Property(e => e.InviteCode).HasColumnName("invite_code").IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Institution).WithMany(e => e.InstitutionCampusInvitations)
                .HasForeignKey(e => e.InstitutionId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Campus).WithMany(e => e.InstitutionCampusInvitations).HasForeignKey(e => e.CampusId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // CompanyInvitation
        modelBuilder.Entity<CompanyInvitation>(entity =>
        {
            entity.ToTable("company_invitation");
            entity.HasKey(e => e.CompanyInvitationId);
            entity.Property(e => e.CompanyInvitationId).HasColumnName("company_invitation_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CompanyId).HasColumnName("company_id").IsRequired();
            entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(191);
            entity.Property(e => e.InviteCode).HasColumnName("invite_code").IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Company).WithMany(e => e.CompanyInvitations).HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // CampusAllowedDomain
        modelBuilder.Entity<CampusAllowedDomain>(entity =>
        {
            entity.ToTable("campus_allowed_domain");
            entity.HasKey(e => e.CampusAllowedDomainId);
            entity.Property(e => e.CampusAllowedDomainId).HasColumnName("campus_allowed_domain_id")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.CampusId).HasColumnName("campus_id").IsRequired();
            entity.Property(e => e.Domain).HasColumnName("domain").IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(e => e.Campus).WithMany(e => e.CampusAllowedDomains).HasForeignKey(e => e.CampusId)
                .HasConstraintName("fk_campus_allowed_domain_campus").OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => new { e.CampusId, e.Domain }).IsUnique();
        });

        // RefreshToken
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("refresh_token");
            entity.HasKey(e => e.RefreshTokenId);
            entity.Property(e => e.RefreshTokenId).HasColumnName("refresh_token_id").ValueGeneratedOnAdd();
            entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            entity.Property(e => e.Token).HasColumnName("token").IsRequired().HasMaxLength(200);
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            // Ajout de la propriété SessionEventId
            entity.Property(e => e.SessionEventId)
                .HasColumnName("sessionEvent_id")
                .IsRequired();

            // Relation avec SessionEvent
            entity.HasOne(e => e.SessionEvent)
                .WithMany(se => se.RefreshTokens)
                .HasForeignKey(e => e.SessionEventId)
                .HasConstraintName("FK_RefreshToken_SessionEvents")
                .OnDelete(DeleteBehavior.Cascade);

            // Relation avec Users
            entity.HasOne(e => e.Users)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("fk_refresh_token_users")
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.Used)
                .HasColumnName("used")
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(e => e.UsedAt)
                .HasColumnName("used_at")
                .IsRequired(false);


            entity.HasIndex(e => e.Token).IsUnique();
        });

        // Housing
        modelBuilder.Entity<Housing>(entity =>
        {
            entity.ToTable("housing");
            entity.HasKey(e => e.HousingId);
            entity.Property(e => e.HousingId).HasColumnName("housing_id").ValueGeneratedOnAdd();
            entity.Property(e => e.HousingOwnerId).HasColumnName("housing_owner_id").IsRequired();
            entity.Property(e => e.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnName("description").IsRequired().HasColumnType("TEXT");
            entity.Property(e => e.Price).HasColumnName("price").IsRequired().HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.LocationId).HasColumnName("location_id").IsRequired();
            entity.Property(e => e.Charges).HasColumnName("charges").HasDefaultValue(0).HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.Deposit).HasColumnName("deposit").HasDefaultValue(0).HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.Size).HasColumnName("size").HasColumnType("DECIMAL(5,2)");
            entity.Property(e => e.BedroomCount).HasColumnName("bedroom_count");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.HousingTypeId).HasColumnName("housing_type_id").IsRequired();
            entity.Property(e => e.PebRatingId).HasColumnName("peb_rating_id").IsRequired();
            entity.Property(e => e.Status).HasColumnName("status").IsRequired().HasMaxLength(20)
                .HasDefaultValue("Disponible");
            entity.Property(e => e.AvailabilityDate).HasColumnName("availability_date");
            entity.Property(e => e.EndAvailabilityDate).HasColumnName("end_availability_date");
            entity.Property(e => e.Preferences).HasColumnName("preferences");
            entity.Property(e => e.LegalCompliance).HasColumnName("legal_compliance").HasDefaultValue(false);
            entity.Property(e => e.Sponsored).HasColumnName("sponsored").HasDefaultValue(false);
            entity.Property(e => e.ViewCount).HasColumnName("view_count").HasDefaultValue(0);
            entity.Property(e => e.ApplicationCount).HasColumnName("application_count").HasDefaultValue(0);
            entity.Property(e => e.LikeCount).HasColumnName("like_count").HasDefaultValue(0);
            entity.Property(e => e.VisitCount).HasColumnName("visit_count").HasDefaultValue(0);
            entity.Property(e => e.LastViewedAt).HasColumnName("last_viewed_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.HousingOwner).WithMany(e => e.Housings).HasForeignKey(e => e.HousingOwnerId)
                .HasConstraintName("fk_housing_owner").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Location).WithMany(e => e.Housings).HasForeignKey(e => e.LocationId)
                .HasConstraintName("fk_housing_location").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.HousingType).WithMany(e => e.Housings).HasForeignKey(e => e.HousingTypeId)
                .HasConstraintName("fk_housing_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.PebRating).WithMany(e => e.Housings).HasForeignKey(e => e.PebRatingId)
                .HasConstraintName("fk_housing_peb_rating").OnDelete(DeleteBehavior.Restrict);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Housing_Price", "price >= 0");
                t.HasCheckConstraint("CK_Housing_Charges", "charges >= 0");
                t.HasCheckConstraint("CK_Housing_Deposit", "deposit >= 0");
                t.HasCheckConstraint("CK_Housing_Size", "size > 0 OR size IS NULL");
                t.HasCheckConstraint("CK_Housing_BedroomCount", "bedroom_count >= 1 OR bedroom_count IS NULL");
                t.HasCheckConstraint("CK_Housing_Capacity", "capacity >= 1 OR capacity IS NULL");
                t.HasCheckConstraint("CK_Housing_ViewCount", "view_count >= 0");
                t.HasCheckConstraint("CK_Housing_ApplicationCount", "application_count >= 0");
                t.HasCheckConstraint("CK_Housing_LikeCount", "like_count >= 0");
                t.HasCheckConstraint("CK_Housing_VisitCount", "visit_count >= 0");
                t.HasCheckConstraint("CK_Housing_Status", "status IN ('Disponible', 'Réservé', 'Loué')");
            });
        });

        // HousingAmenity
        modelBuilder.Entity<HousingAmenity>(entity =>
        {
            entity.ToTable("housing_amenity");
            entity.HasKey(e => new { e.HousingId, e.AmenityId });
            entity.Property(e => e.HousingId).HasColumnName("housing_id");
            entity.Property(e => e.AmenityId).HasColumnName("amenity_id");
            entity.HasOne(e => e.Housing).WithMany(e => e.HousingAmenities).HasForeignKey(e => e.HousingId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Amenity).WithMany(e => e.HousingAmenities).HasForeignKey(e => e.AmenityId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // HousingApplication
        modelBuilder.Entity<HousingApplication>(entity =>
        {
            entity.ToTable("housing_application");
            entity.HasKey(e => e.HousingApplicationId);
            entity.Property(e => e.HousingApplicationId).HasColumnName("housing_application_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.HousingId).HasColumnName("housing_id").IsRequired();
            entity.Property(e => e.ApplicationStatusId).HasColumnName("application_status_id").HasDefaultValue(1);
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.AppliedAt).HasColumnName("applied_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(e => e.Student).WithMany(e => e.HousingApplications).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_housing_application_student").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Housing).WithMany(e => e.HousingApplications).HasForeignKey(e => e.HousingId)
                .HasConstraintName("fk_housing_application_housing").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ApplicationStatus).WithMany(e => e.HousingApplications)
                .HasForeignKey(e => e.ApplicationStatusId).HasConstraintName("fk_housing_application_status")
                .OnDelete(DeleteBehavior.SetNull);
        });

        // HousingVisit
        modelBuilder.Entity<HousingVisit>(entity =>
        {
            entity.ToTable("housing_visit");
            entity.HasKey(e => e.HousingVisitId);
            entity.Property(e => e.HousingVisitId).HasColumnName("housing_visit_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.HousingId).HasColumnName("housing_id").IsRequired();
            entity.Property(e => e.ApplicationStatusId).HasColumnName("application_status_id").HasDefaultValue(1);
            entity.Property(e => e.ConfirmedDateTime).HasColumnName("confirmed_date_time");
            entity.Property(e => e.StudentMessage).HasColumnName("student_message");
            entity.Property(e => e.LandlordMessage).HasColumnName("landlord_message");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Student).WithMany(e => e.HousingVisits).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_housing_visit_student").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Housing).WithMany(e => e.HousingVisits).HasForeignKey(e => e.HousingId)
                .HasConstraintName("fk_housing_visit_housing").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ApplicationStatus).WithMany(e => e.HousingVisits)
                .HasForeignKey(e => e.ApplicationStatusId).HasConstraintName("fk_housing_visit_status")
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_housing_visit_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_housing_visit_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // HousingVisitRange
        modelBuilder.Entity<HousingVisitRange>(entity =>
        {
            entity.ToTable("housing_visit_range");
            entity.HasKey(e => e.HousingVisitRangeId);
            entity.Property(e => e.HousingVisitRangeId).HasColumnName("housing_visit_range_id").ValueGeneratedOnAdd();
            entity.Property(e => e.HousingVisitId).HasColumnName("housing_visit_id").IsRequired();
            entity.Property(e => e.StartDateTime).HasColumnName("start_datetime").IsRequired();
            entity.Property(e => e.EndDateTime).HasColumnName("end_datetime").IsRequired();
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.RangeStatus).HasColumnName("range_status").IsRequired().HasMaxLength(20)
                .HasDefaultValue("Proposed");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.HousingVisit).WithMany(e => e.HousingVisitRanges).HasForeignKey(e => e.HousingVisitId)
                .HasConstraintName("fk_housing_visit_range_visit").OnDelete(DeleteBehavior.Cascade);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_HousingVisitRange_RangeStatus",
                    "range_status IN ('Proposed', 'Selected', 'Declined', 'Countered')");
                t.HasCheckConstraint("chk_range_dates", "end_datetime > start_datetime");
            });
        });

        // Offer
        modelBuilder.Entity<Offer>(entity =>
        {
            entity.ToTable("offer");
            entity.HasKey(e => e.OfferId);
            entity.Property(e => e.OfferId).HasColumnName("offer_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CompanyId).HasColumnName("company_id").IsRequired();
            entity.Property(e => e.OfferTypeId).HasColumnName("offer_type_id").IsRequired();
            entity.Property(e => e.Title).HasColumnName("title").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Duration).HasColumnName("duration").HasColumnType("SMALLINT");
            entity.Property(e => e.DurationUnitId).HasColumnName("duration_unit_id");
            entity.Property(e => e.WorkHours).HasColumnName("work_hours").HasColumnType("SMALLINT");
            entity.Property(e => e.StudentJobHours).HasColumnName("student_job_hours");
            entity.Property(e => e.Salary).HasColumnName("salary").HasColumnType("DECIMAL(7,2)");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.ContractTypeId).HasColumnName("contract_type_id");
            entity.Property(e => e.ScheduleTypeId).HasColumnName("schedule_type_id");
            entity.Property(e => e.EctsCredits).HasColumnName("ects_credits");
            entity.Property(e => e.LanguageId).HasColumnName("language_id").IsRequired();
            entity.Property(e => e.RemotePossible).HasColumnName("remote_possible").HasDefaultValue(false);
            entity.Property(e => e.Sponsored).HasColumnName("sponsored").HasDefaultValue(false);
            entity.Property(e => e.CvRequired).HasColumnName("cv_required").HasDefaultValue(false);
            entity.Property(e => e.CoverLetterRequired).HasColumnName("cover_letter_required").HasDefaultValue(false);
            entity.Property(e => e.ExperienceRequired).HasColumnName("experience_required").HasDefaultValue(false);
            entity.Property(e => e.Deadline).HasColumnName("deadline");
            entity.Property(e => e.Active).HasColumnName("active").HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.HasOne(e => e.Company).WithMany(e => e.Offers).HasForeignKey(e => e.CompanyId)
                .HasConstraintName("fk_offer_company").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.OfferType).WithMany(e => e.Offers).HasForeignKey(e => e.OfferTypeId)
                .HasConstraintName("fk_offer_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Location).WithMany(e => e.Offers).HasForeignKey(e => e.LocationId)
                .HasConstraintName("fk_offer_location").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.DurationUnit).WithMany(e => e.Offers).HasForeignKey(e => e.DurationUnitId)
                .HasConstraintName("fk_offer_duration_unit").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.ContractType).WithMany(e => e.Offers).HasForeignKey(e => e.ContractTypeId)
                .HasConstraintName("fk_offer_contract_type").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.ScheduleType).WithMany(e => e.Offers).HasForeignKey(e => e.ScheduleTypeId)
                .HasConstraintName("fk_offer_schedule_type").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Language).WithMany(e => e.Offers).HasForeignKey(e => e.LanguageId)
                .HasConstraintName("fk_offer_language").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Payment).WithMany(e => e.Offers).HasForeignKey(e => e.PaymentId)
                .HasConstraintName("fk_offer_payment").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_offer_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_offer_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Offer_Duration", "duration > 0 OR duration IS NULL");
                t.HasCheckConstraint("CK_Offer_WorkHours", "work_hours > 0 OR work_hours IS NULL");
                t.HasCheckConstraint("CK_Offer_StudentJobHours",
                    "student_job_hours >= 0 AND student_job_hours <= 475 OR student_job_hours IS NULL");
                t.HasCheckConstraint("CK_Offer_Salary", "salary >= 0 OR salary IS NULL");
                t.HasCheckConstraint("CK_Offer_EctsCredits", "ects_credits >= 0 OR ects_credits IS NULL");
            });
        });

        // Application
        modelBuilder.Entity<Application>(entity =>
        {
            entity.ToTable("application");
            entity.HasKey(e => e.ApplicationId);
            entity.Property(e => e.ApplicationId).HasColumnName("application_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");
            entity.Property(e => e.ApplicationStatusId).HasColumnName("application_status_id").HasDefaultValue(1);
            entity.Property(e => e.CvPath).HasColumnName("cv_path").HasMaxLength(191);
            entity.Property(e => e.CoverLetterPath).HasColumnName("cover_letter_path").HasMaxLength(191);
            entity.Property(e => e.Reason).HasColumnName("reason");
            entity.Property(e => e.ViewedByCompany).HasColumnName("viewed_by_company").HasDefaultValue(false);
            entity.Property(e => e.AppliedAt).HasColumnName("applied_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(e => e.Student).WithMany(e => e.Applications).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_application_student").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Offer).WithMany(e => e.Applications).HasForeignKey(e => e.OfferId)
                .HasConstraintName("fk_application_offer").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ApplicationStatus).WithMany(e => e.Applications)
                .HasForeignKey(e => e.ApplicationStatusId).HasConstraintName("fk_application_status")
                .OnDelete(DeleteBehavior.SetNull);
        });

        // OfferFavorite
        modelBuilder.Entity<OfferFavorite>(entity =>
        {
            entity.ToTable("offer_favorite");
            entity.HasKey(e => e.OfferFavoriteId);
            entity.Property(e => e.OfferFavoriteId).HasColumnName("offer_favorite_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.OfferId).HasColumnName("offer_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(e => e.Student).WithMany(e => e.OfferFavorites).HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Offer).WithMany(e => e.OfferFavorites).HasForeignKey(e => e.OfferId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => new { e.StudentId, e.OfferId }).IsUnique();
        });

        // WorkHours
        modelBuilder.Entity<WorkHours>(entity =>
        {
            entity.ToTable("work_hours");
            entity.HasKey(e => e.WorkHourId);
            entity.Property(e => e.WorkHourId).HasColumnName("work_hour_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.OfferId).HasColumnName("offer_id").IsRequired();
            entity.Property(e => e.EmployerId).HasColumnName("employer_id").IsRequired();
            entity.Property(e => e.HoursWorked).HasColumnName("hours_worked").HasColumnType("DECIMAL(5,2)");
            entity.Property(e => e.WorkDate).HasColumnName("work_date").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.HasOne(e => e.Student).WithMany(e => e.WorkHours).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_work_hours_student").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Offer).WithMany(e => e.WorkHourss).HasForeignKey(e => e.OfferId)
                .HasConstraintName("fk_work_hours_offer").OnDelete(DeleteBehavior.Cascade);
            entity.ToTable(t => t.HasCheckConstraint("CK_WorkHours_HoursWorked", "hours_worked >= 0"));
        });

        // EntityReview
        modelBuilder.Entity<EntityReview>(entity =>
        {
            entity.ToTable("entity_review");
            entity.HasKey(e => e.EntityReviewId);
            entity.Property(e => e.EntityReviewId).HasColumnName("entity_review_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.EntityType).HasColumnName("entity_type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.Rating).HasColumnName("rating").HasColumnType("DECIMAL(2,1)");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Student).WithMany(e => e.EntityReviews).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_entity_review_student").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_entity_review_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_entity_review_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => new { e.StudentId, e.EntityType, e.EntityId }).IsUnique();
            entity.ToTable(t => t.HasCheckConstraint("CK_EntityReview_Rating", "rating >= 0 AND rating <= 5"));
        });

        // StudentReferral
        modelBuilder.Entity<StudentReferral>(entity =>
        {
            entity.ToTable("student_referral");
            entity.HasKey(e => e.StudentReferralId);
            entity.Property(e => e.StudentReferralId).HasColumnName("student_referral_id").ValueGeneratedOnAdd();
            entity.Property(e => e.ReferringStudentId).HasColumnName("referring_student_id").IsRequired();
            entity.Property(e => e.ReferredStudentId).HasColumnName("referred_student_id").IsRequired();
            entity.Property(e => e.EntityType).HasColumnName("entity_type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.Reward).HasColumnName("reward").HasColumnType("DECIMAL(7,2)");
            entity.Property(e => e.ReferralStatus).HasColumnName("referral_status").IsRequired().HasMaxLength(20)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.ReferringStudent).WithMany(e => e.ReferralsAsReferring)
                .HasForeignKey(e => e.ReferringStudentId).HasConstraintName("fk_referral_referring_student")
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ReferredStudent).WithMany(e => e.ReferralsAsReferred)
                .HasForeignKey(e => e.ReferredStudentId).HasConstraintName("fk_referral_referred_student")
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_referral_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_referral_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => new { e.ReferringStudentId, e.ReferredStudentId, e.EntityType, e.EntityId })
                .IsUnique();
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_StudentReferral_Reward", "reward >= 0 OR reward IS NULL");
                t.HasCheckConstraint("CK_StudentReferral_Status",
                    "referral_status IN ('Pending', 'Completed', 'Rejected')");
                t.HasCheckConstraint("chk_not_self_referral", "referring_student_id != referred_student_id");
            });
        });

        // CampusFacility
        modelBuilder.Entity<CampusFacility>(entity =>
        {
            entity.ToTable("campus_facility");
            entity.HasKey(e => e.CampusFacilityId);
            entity.Property(e => e.CampusFacilityId).HasColumnName("campus_facility_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CampusId).HasColumnName("campus_id").IsRequired();
            entity.Property(e => e.FacilityTypeId).HasColumnName("facility_type_id").IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("quantity").HasDefaultValue(1);
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Campus).WithMany(e => e.CampusFacilities).HasForeignKey(e => e.CampusId)
                .HasConstraintName("fk_campus_facility_campus").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.FacilityType).WithMany(e => e.CampusFacilities).HasForeignKey(e => e.FacilityTypeId)
                .HasConstraintName("fk_campus_facility_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_campus_facility_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_campus_facility_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t => t.HasCheckConstraint("CK_CampusFacility_Quantity", "quantity >= 0"));
        });

        // StudyDomain
        modelBuilder.Entity<StudyDomain>(entity =>
        {
            entity.ToTable("study_domain");
            entity.HasKey(e => e.DomainId);
            entity.Property(e => e.DomainId).HasColumnName("domain_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // Degree
        modelBuilder.Entity<Degree>(entity =>
        {
            entity.ToTable("degree");
            entity.HasKey(e => e.DegreeId);
            entity.Property(e => e.DegreeId).HasColumnName("degree_id").ValueGeneratedOnAdd();
            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.DegreeTypeId).HasColumnName("degree_type_id").IsRequired();
            entity.Property(e => e.DegreeCategoryId).HasColumnName("degree_category_id");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.DurationUnitId).HasColumnName("duration_unit_id");
            entity.Property(e => e.Credits).HasColumnName("credits");
            entity.Property(e => e.Cost).HasColumnName("cost").HasColumnType("DECIMAL(7,2)");
            entity.Property(e => e.TuitionTypeId).HasColumnName("tuition_type_id").IsRequired().HasDefaultValue(1);
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.ScheduleTypeId).HasColumnName("schedule_type_id").IsRequired();
            entity.Property(e => e.IsAlternance).HasColumnName("is_alternance").HasDefaultValue(false);
            entity.Property(e => e.CertificationTypeId).HasColumnName("certification_type_id");
            entity.Property(e => e.DeliveryModeId).HasColumnName("delivery_mode_id").IsRequired().HasDefaultValue(1);
            entity.Property(e => e.QualificationLevel).HasColumnName("qualification_level");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.LikeCount).HasColumnName("like_count").HasDefaultValue(0);
            entity.Property(e => e.VisitCount).HasColumnName("visit_count").HasDefaultValue(0);
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.FinancabilityRequiredCredits).HasColumnName("financability_required_credits");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Campus).WithMany(e => e.Degrees).HasForeignKey(e => e.CampusId)
                .HasConstraintName("fk_degree_campus").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.DegreeType).WithMany(e => e.Degrees).HasForeignKey(e => e.DegreeTypeId)
                .HasConstraintName("fk_degree_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.DegreeCategory).WithMany(e => e.Degrees).HasForeignKey(e => e.DegreeCategoryId)
                .HasConstraintName("fk_degree_category").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.DurationUnit).WithMany(e => e.Degrees).HasForeignKey(e => e.DurationUnitId)
                .HasConstraintName("fk_degree_duration_unit").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.TuitionType).WithMany(e => e.Degrees).HasForeignKey(e => e.TuitionTypeId)
                .HasConstraintName("fk_degree_tuition_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Language).WithMany(e => e.Degrees).HasForeignKey(e => e.LanguageId)
                .HasConstraintName("fk_degree_language").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.ScheduleType).WithMany(e => e.Degrees).HasForeignKey(e => e.ScheduleTypeId)
                .HasConstraintName("fk_degree_schedule_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.CertificationType).WithMany(e => e.Degrees).HasForeignKey(e => e.CertificationTypeId)
                .HasConstraintName("fk_degree_certification_type").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.DeliveryMode).WithMany(e => e.Degrees).HasForeignKey(e => e.DeliveryModeId)
                .HasConstraintName("fk_degree_delivery_mode").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_degree_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_degree_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Degree_Duration", "duration > 0 OR duration IS NULL");
                t.HasCheckConstraint("CK_Degree_Credits", "credits > 0 OR credits IS NULL");
                t.HasCheckConstraint("CK_Degree_Cost", "cost >= 0 OR cost IS NULL");
                t.HasCheckConstraint("CK_Degree_QualificationLevel",
                    "qualification_level BETWEEN 1 AND 8 OR qualification_level IS NULL");
                t.HasCheckConstraint("CK_Degree_LikeCount", "like_count >= 0");
                t.HasCheckConstraint("CK_Degree_VisitCount", "visit_count >= 0");
                t.HasCheckConstraint("CK_Degree_FinancabilityRequiredCredits",
                    "financability_required_credits >= 0 OR financability_required_credits IS NULL");
            });
        });

        // DegreePartnership
        modelBuilder.Entity<DegreePartnership>(entity =>
        {
            entity.ToTable("degree_partnership");
            entity.HasKey(e => e.DegreePartnershipId);
            entity.Property(e => e.DegreePartnershipId).HasColumnName("degree_partnership_id").ValueGeneratedOnAdd();
            entity.Property(e => e.DegreeId).HasColumnName("degree_id").IsRequired();
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.PartnerName).HasColumnName("partner_name").HasMaxLength(100);
            entity.Property(e => e.PartnershipType).HasColumnName("partnership_type").HasMaxLength(50);
            entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.Degree).WithMany(e => e.DegreePartnerships).HasForeignKey(e => e.DegreeId)
                .HasConstraintName("fk_degree_partnership_degree").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Company).WithMany(e => e.DegreePartnerships).HasForeignKey(e => e.CompanyId)
                .HasConstraintName("fk_degree_partnership_company").OnDelete(DeleteBehavior.SetNull);
        });

        // OfferDegree
        modelBuilder.Entity<OfferDegree>(entity =>
        {
            entity.ToTable("offer_degree");
            entity.HasKey(e => e.OfferDegreeId);
            entity.Property(e => e.OfferDegreeId).HasColumnName("offer_degree_id").ValueGeneratedOnAdd();
            entity.Property(e => e.OfferId).HasColumnName("offer_id").IsRequired();
            entity.Property(e => e.DegreeId).HasColumnName("degree_id").IsRequired();
            entity.Property(e => e.Mandatory).HasColumnName("mandatory").HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.Offer).WithMany(e => e.OfferDegrees).HasForeignKey(e => e.OfferId)
                .HasConstraintName("fk_offer_degree_offer").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Degree).WithMany(e => e.OfferDegrees).HasForeignKey(e => e.DegreeId)
                .HasConstraintName("fk_offer_degree_degree").OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => new { e.OfferId, e.DegreeId }).IsUnique();
        });

        // Specialty
        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.ToTable("specialty");
            entity.HasKey(e => e.SpecialtyId);
            entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id").ValueGeneratedOnAdd();
            entity.Property(e => e.DegreeId).HasColumnName("degree_id");
            entity.Property(e => e.DomainId).HasColumnName("domain_id").IsRequired();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.Outcomes).HasColumnName("outcomes");
            entity.Property(e => e.OfficiallyRecognized).HasColumnName("officially_recognized").HasDefaultValue(false);
            entity.Property(e => e.LikeCount).HasColumnName("like_count").HasDefaultValue(0);
            entity.Property(e => e.VisitCount).HasColumnName("visit_count").HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Degree).WithMany(e => e.Specialties).HasForeignKey(e => e.DegreeId)
                .HasConstraintName("fk_specialty_degree").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Domain).WithMany(e => e.Specialties).HasForeignKey(e => e.DomainId)
                .HasConstraintName("fk_specialty_domain").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_specialty_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_specialty_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Specialty_LikeCount", "like_count >= 0");
                t.HasCheckConstraint("CK_Specialty_VisitCount", "visit_count >= 0");
            });
        });

        // Semester
        modelBuilder.Entity<Semester>(entity =>
        {
            entity.ToTable("semester");
            entity.HasKey(e => e.SemesterId);
            entity.Property(e => e.SemesterId).HasColumnName("semester_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
        });

        // ActivityType
        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.ToTable("activity_type");
            entity.HasKey(e => e.ActivityTypeId);
            entity.Property(e => e.ActivityTypeId).HasColumnName("activity_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_activity_type_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_activity_type_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // UE
        modelBuilder.Entity<UE>(entity =>
        {
            entity.ToTable("ue");
            entity.HasKey(e => e.UeId);
            entity.Property(e => e.UeId).HasColumnName("ue_id").ValueGeneratedOnAdd();
            entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Year).HasColumnName("year").IsRequired();
            entity.Property(e => e.SemesterId).HasColumnName("semester_id").IsRequired();
            entity.Property(e => e.CreditCount).HasColumnName("credit_count").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.Mandatory).HasColumnName("mandatory").HasDefaultValue(true);
            entity.Property(e => e.PrerequisiteUeId).HasColumnName("prerequisite_ue_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Specialty).WithMany(e => e.UEs).HasForeignKey(e => e.SpecialtyId)
                .HasConstraintName("fk_ue_specialty").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Semester).WithMany(e => e.UEs).HasForeignKey(e => e.SemesterId)
                .HasConstraintName("fk_ue_semester").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.PrerequisiteUE).WithMany(e => e.DependentUEs).HasForeignKey(e => e.PrerequisiteUeId)
                .HasConstraintName("fk_ue_prerequisite").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_ue_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_ue_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_UE_Year", "year >= 1");
                t.HasCheckConstraint("CK_UE_CreditCount", "credit_count > 0");
            });
        });

        // UA
        modelBuilder.Entity<UA>(entity =>
        {
            entity.ToTable("ua");
            entity.HasKey(e => e.UaId);
            entity.Property(e => e.UaId).HasColumnName("ua_id").ValueGeneratedOnAdd();
            entity.Property(e => e.UeId).HasColumnName("ue_id");
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            entity.Property(e => e.ActivityTypeId).HasColumnName("activity_type_id").IsRequired();
            entity.Property(e => e.CreditCount).HasColumnName("credit_count").IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.Mandatory).HasColumnName("mandatory").HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.UE).WithMany(e => e.UAs).HasForeignKey(e => e.UeId).HasConstraintName("fk_ua_ue")
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ActivityType).WithMany(e => e.UAs).HasForeignKey(e => e.ActivityTypeId)
                .HasConstraintName("fk_ua_activity_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_ua_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_ua_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t => t.HasCheckConstraint("CK_UA_CreditCount", "credit_count > 0"));
        });

        // Bridge
        modelBuilder.Entity<Bridge>(entity =>
        {
            entity.ToTable("bridge");
            entity.HasKey(e => e.BridgeId);
            entity.Property(e => e.BridgeId).HasColumnName("bridge_id").ValueGeneratedOnAdd();
            entity.Property(e => e.FromDegreeId).HasColumnName("from_degree_id");
            entity.Property(e => e.ToDegreeId).HasColumnName("to_degree_id");
            entity.Property(e => e.AdditionalCreditCount).HasColumnName("additional_credit_count");
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.FromDegree).WithMany(e => e.BridgesAsFrom).HasForeignKey(e => e.FromDegreeId)
                .HasConstraintName("fk_bridge_from_degree").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ToDegree).WithMany(e => e.BridgesAsTo).HasForeignKey(e => e.ToDegreeId)
                .HasConstraintName("fk_bridge_to_degree").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_bridge_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_bridge_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t => t.HasCheckConstraint("CK_Bridge_AdditionalCreditCount",
                "additional_credit_count >= 0 OR additional_credit_count IS NULL"));
        });

        // PrerequisiteType
        modelBuilder.Entity<PrerequisiteType>(entity =>
        {
            entity.ToTable("prerequisite_type");
            entity.HasKey(e => e.PrerequisiteTypeId);
            entity.Property(e => e.PrerequisiteTypeId).HasColumnName("prerequisite_type_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // PrerequisiteSource
        modelBuilder.Entity<PrerequisiteSource>(entity =>
        {
            entity.ToTable("prerequisite_source");
            entity.HasKey(e => e.PrerequisiteSourceId);
            entity.Property(e => e.PrerequisiteSourceId).HasColumnName("prerequisite_source_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(191);
        });

        // DegreePrerequisite
        modelBuilder.Entity<DegreePrerequisite>(entity =>
        {
            entity.ToTable("degree_prerequisite");
            entity.HasKey(e => e.DegreePrerequisiteId);
            entity.Property(e => e.DegreePrerequisiteId).HasColumnName("degree_prerequisite_id").ValueGeneratedOnAdd();
            entity.Property(e => e.DegreeId).HasColumnName("degree_id").IsRequired();
            entity.Property(e => e.PrerequisiteTypeId).HasColumnName("prerequisite_type_id").IsRequired();
            entity.Property(e => e.PrerequisiteSourceId).HasColumnName("prerequisite_source_id").IsRequired()
                .HasDefaultValue(3);
            entity.Property(e => e.Description).HasColumnName("description").IsRequired().HasColumnType("TEXT");
            entity.Property(e => e.RequiredDegreeId).HasColumnName("required_degree_id");
            entity.Property(e => e.MinimumGrade).HasColumnName("minimum_grade").HasColumnType("DECIMAL(4,2)");
            entity.Property(e => e.Mandatory).HasColumnName("mandatory").HasDefaultValue(true);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Degree).WithMany(e => e.DegreePrerequisites).HasForeignKey(e => e.DegreeId)
                .HasConstraintName("fk_degree_prerequisite_degree").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.PrerequisiteType).WithMany(e => e.DegreePrerequisites)
                .HasForeignKey(e => e.PrerequisiteTypeId).HasConstraintName("fk_degree_prerequisite_type")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.PrerequisiteSource).WithMany(e => e.DegreePrerequisites)
                .HasForeignKey(e => e.PrerequisiteSourceId).HasConstraintName("fk_degree_prerequisite_source")
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.RequiredDegree).WithMany().HasForeignKey(e => e.RequiredDegreeId)
                .HasConstraintName("fk_degree_prerequisite_required_degree").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_degree_prerequisite_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_degree_prerequisite_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t => t.HasCheckConstraint("CK_DegreePrerequisite_MinimumGrade",
                "minimum_grade >= 0 AND minimum_grade <= 100 OR minimum_grade IS NULL"));
        });

        // Event
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("event");
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.EventId).HasColumnName("event_id").ValueGeneratedOnAdd();
            entity.Property(e => e.EventOwnerId).HasColumnName("event_owner_id").IsRequired();
            entity.Property(e => e.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnName("description").HasColumnType("TEXT");
            entity.Property(e => e.StartDateTime).HasColumnName("start_date_time").IsRequired();
            entity.Property(e => e.EndDateTime).HasColumnName("end_date_time").IsRequired();
            entity.Property(e => e.LocationId).HasColumnName("location_id").IsRequired();
            entity.Property(e => e.RegistrationRequired).HasColumnName("registration_required").HasDefaultValue(false);
            entity.Property(e => e.RegistrationLink).HasColumnName("registration_link").HasMaxLength(191);
            entity.Property(e => e.IsPublic).HasColumnName("is_public").HasDefaultValue(false);
            entity.Property(e => e.LikeCount).HasColumnName("like_count").HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.HasOne(e => e.EventOwner).WithMany(e => e.Events).HasForeignKey(e => e.EventOwnerId)
                .HasConstraintName("fk_event_owner").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Location).WithMany(e => e.Events).HasForeignKey(e => e.LocationId)
                .HasConstraintName("fk_event_location").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Payment).WithMany(e => e.Events).HasForeignKey(e => e.PaymentId)
                .HasConstraintName("fk_event_payment").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_event_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_event_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Event_Registration",
                    "(registration_required = TRUE AND registration_link IS NOT NULL) OR (registration_required = FALSE AND registration_link IS NULL)");
                t.HasCheckConstraint("CK_Event_LikeCount", "like_count >= 0");
            });
        });

        // EventInterest
        modelBuilder.Entity<EventInterest>(entity =>
        {
            entity.ToTable("event_interest");
            entity.HasKey(e => e.EventInterestId);
            entity.Property(e => e.EventInterestId).HasColumnName("event_interest_id").ValueGeneratedOnAdd();
            entity.Property(e => e.EventId).HasColumnName("event_id").IsRequired();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.InterestDate).HasColumnName("interest_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.Event).WithMany(e => e.EventInterests).HasForeignKey(e => e.EventId)
                .HasConstraintName("fk_event_interest_event").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Student).WithMany(e => e.EventInterests).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_event_interest_student").OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => new { e.EventId, e.StudentId }).IsUnique();
        });

        // Image
        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("image");
            entity.HasKey(e => e.ImageId);
            entity.Property(e => e.ImageId).HasColumnName("image_id").ValueGeneratedOnAdd();
            entity.Property(e => e.EntityType).HasColumnName("entity_type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.ImagePath).HasColumnName("image_path").IsRequired().HasMaxLength(191);
            entity.Property(e => e.MimeTypeId).HasColumnName("mime_type_id").IsRequired();
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary").HasDefaultValue(false);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.MimeType).WithMany(e => e.Images).HasForeignKey(e => e.MimeTypeId)
                .HasConstraintName("fk_image_mime_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_image_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_image_updated_by").OnDelete(DeleteBehavior.SetNull);
        });

        // EntityLike
        modelBuilder.Entity<EntityLike>(entity =>
        {
            entity.ToTable("entity_like");
            entity.HasKey(e => e.LikeId);
            entity.Property(e => e.LikeId).HasColumnName("like_id").ValueGeneratedOnAdd();
            entity.Property(e => e.StudentId).HasColumnName("student_id").IsRequired();
            entity.Property(e => e.EntityType).HasColumnName("entity_type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Student).WithMany(e => e.EntityLikes).HasForeignKey(e => e.StudentId)
                .HasConstraintName("fk_like_student").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_like_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_like_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => new { e.StudentId, e.EntityType, e.EntityId }).IsUnique()
                .HasDatabaseName("unique_like_per_student_entity");
        });

        // PaymentStatus
        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.ToTable("payment_status");
            entity.HasKey(e => e.PaymentStatusId);
            entity.Property(e => e.PaymentStatusId).HasColumnName("payment_status_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description");
        });

        // Currency
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("currency");
            entity.HasKey(e => e.CurrencyId);
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Code).HasColumnName("code").IsRequired().HasMaxLength(3);
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
        });

        // PaymentMethod
        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.ToTable("payment_method");
            entity.HasKey(e => e.PaymentMethodId);
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        });

        // Tax
        modelBuilder.Entity<Tax>(entity =>
        {
            entity.ToTable("tax");
            entity.HasKey(e => e.TaxId);
            entity.Property(e => e.TaxId).HasColumnName("tax_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Rate).HasColumnName("rate").IsRequired().HasColumnType("DECIMAL(5,2)");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.ToTable(t => t.HasCheckConstraint("CK_Tax_Rate", "rate >= 0"));
        });

        // PaymentPlan
        modelBuilder.Entity<PaymentPlan>(entity =>
        {
            entity.ToTable("payment_plan");
            entity.HasKey(e => e.PaymentPlanId);
            entity.Property(e => e.PaymentPlanId).HasColumnName("payment_plan_id").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Price).HasColumnName("price").IsRequired().HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id").IsRequired();
            entity.Property(e => e.DurationDays).HasColumnName("duration_days");
            entity.Property(e => e.IsRecurring).HasColumnName("is_recurring").HasDefaultValue(false);
            entity.Property(e => e.MaxOfferCount).HasColumnName("max_offer_count");
            entity.Property(e => e.MaxHousingCount).HasColumnName("max_housing_count");
            entity.Property(e => e.MaxEventCount).HasColumnName("max_event_count");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.Currency).WithMany(e => e.PaymentPlans).HasForeignKey(e => e.CurrencyId)
                .HasConstraintName("fk_payment_plan_currency").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_payment_plan_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_payment_plan_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_PaymentPlan_Price", "price >= 0");
                t.HasCheckConstraint("CK_PaymentPlan_DurationDays", "duration_days > 0 OR duration_days IS NULL");
                t.HasCheckConstraint("CK_PaymentPlan_MaxOfferCount", "max_offer_count >= 0 OR max_offer_count IS NULL");
                t.HasCheckConstraint("CK_PaymentPlan_MaxHousingCount",
                    "max_housing_count >= 0 OR max_housing_count IS NULL");
                t.HasCheckConstraint("CK_PaymentPlan_MaxEventCount", "max_event_count >= 0 OR max_event_count IS NULL");
            });
        });

        // Payment
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("payment");
            entity.HasKey(e => e.PaymentId);
            entity.Property(e => e.PaymentId).HasColumnName("payment_id").ValueGeneratedOnAdd();
            entity.Property(e => e.PaymentPlanId).HasColumnName("payment_plan_id").IsRequired();
            entity.Property(e => e.OwnerTypeId).HasColumnName("owner_type_id").IsRequired();
            entity.Property(e => e.OwnerId).HasColumnName("owner_id").IsRequired();
            entity.Property(e => e.Amount).HasColumnName("amount").IsRequired().HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.TaxAmount).HasColumnName("tax_amount").HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount").IsRequired()
                .HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.CurrencyId).HasColumnName("currency_id").IsRequired();
            entity.Property(e => e.PaymentDate).HasColumnName("payment_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id").IsRequired();
            entity.Property(e => e.PaymentStatusId).HasColumnName("payment_status_id").IsRequired().HasDefaultValue(1);
            entity.Property(e => e.TransactionReference).HasColumnName("transaction_reference").HasMaxLength(100);
            entity.Property(e => e.ExternalToken).HasColumnName("external_token").HasMaxLength(191);
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.HasOne(e => e.PaymentPlan).WithMany(e => e.Payments).HasForeignKey(e => e.PaymentPlanId)
                .HasConstraintName("fk_payment_plan").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.OwnerType).WithMany(e => e.Payments).HasForeignKey(e => e.OwnerTypeId)
                .HasConstraintName("fk_payment_owner_type").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Tax).WithMany(e => e.Payments).HasForeignKey(e => e.TaxId)
                .HasConstraintName("fk_payment_tax").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Currency).WithMany(e => e.Payments).HasForeignKey(e => e.CurrencyId)
                .HasConstraintName("fk_payment_currency").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.PaymentMethod).WithMany(e => e.Payments).HasForeignKey(e => e.PaymentMethodId)
                .HasConstraintName("fk_payment_method").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.PaymentStatus).WithMany(e => e.Payments).HasForeignKey(e => e.PaymentStatusId)
                .HasConstraintName("fk_payment_status").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_payment_created_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Updater).WithMany().HasForeignKey(e => e.UpdatedBy)
                .HasConstraintName("fk_payment_updated_by").OnDelete(DeleteBehavior.SetNull);
            entity.HasIndex(e => e.TransactionReference).IsUnique().HasFilter("[transaction_reference] IS NOT NULL");
            entity.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Payment_Amount", "amount >= 0");
                t.HasCheckConstraint("CK_Payment_TaxAmount", "tax_amount >= 0 OR tax_amount IS NULL");
                t.HasCheckConstraint("CK_Payment_TotalAmount", "total_amount >= 0");
            });
        });

        // PaymentLog
        modelBuilder.Entity<PaymentLog>(entity =>
        {
            entity.ToTable("payment_log");
            entity.HasKey(e => e.PaymentLogId);
            entity.Property(e => e.PaymentLogId).HasColumnName("payment_log_id").ValueGeneratedOnAdd();
            entity.Property(e => e.PaymentId).HasColumnName("payment_id").IsRequired();
            entity.Property(e => e.PreviousStatusId).HasColumnName("previous_status_id");
            entity.Property(e => e.NewStatusId).HasColumnName("new_status_id").IsRequired();
            entity.Property(e => e.LogDate).HasColumnName("log_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.HasOne(e => e.Payment).WithMany(e => e.PaymentLogs).HasForeignKey(e => e.PaymentId)
                .HasConstraintName("fk_payment_log_payment").OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.PreviousStatus).WithMany(e => e.PaymentLogsAsPrevious)
                .HasForeignKey(e => e.PreviousStatusId).HasConstraintName("fk_payment_log_previous_status")
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.NewStatus).WithMany(e => e.PaymentLogsAsNew).HasForeignKey(e => e.NewStatusId)
                .HasConstraintName("fk_payment_log_new_status").OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Creator).WithMany().HasForeignKey(e => e.CreatedBy)
                .HasConstraintName("fk_payment_log_created_by").OnDelete(DeleteBehavior.SetNull);
        });

        // PaymentItem
        modelBuilder.Entity<PaymentItem>(entity =>
        {
            entity.ToTable("payment_item");
            entity.HasKey(e => e.PaymentItemId);
            entity.Property(e => e.PaymentItemId).HasColumnName("payment_item_id").ValueGeneratedOnAdd();
            entity.Property(e => e.PaymentId).HasColumnName("payment_id").IsRequired();
            entity.Property(e => e.Amount).HasColumnName("amount").IsRequired().HasColumnType("DECIMAL(10,2)");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.Payment).WithMany(e => e.PaymentItems).HasForeignKey(e => e.PaymentId)
                .HasConstraintName("fk_payment_item_payment").OnDelete(DeleteBehavior.Cascade);
            entity.ToTable(t => t.HasCheckConstraint("CK_PaymentItem_Amount", "amount >= 0"));
        });

        // PaymentItemEntity
        modelBuilder.Entity<PaymentItemEntity>(entity =>
        {
            entity.ToTable("payment_item_entity");
            entity.HasKey(e => e.PaymentItemEntityId);
            entity.Property(e => e.PaymentItemEntityId).HasColumnName("payment_item_entity_id").ValueGeneratedOnAdd();
            entity.Property(e => e.PaymentItemId).HasColumnName("payment_item_id").IsRequired();
            entity.Property(e => e.EntityType).HasColumnName("entity_type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.EntityId).HasColumnName("entity_id").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.HasOne(e => e.PaymentItem).WithMany(e => e.PaymentItemEntities).HasForeignKey(e => e.PaymentItemId)
                .HasConstraintName("fk_payment_item_entity_item").OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => new { e.PaymentItemId, e.EntityType, e.EntityId }).IsUnique();
        });
    }
}