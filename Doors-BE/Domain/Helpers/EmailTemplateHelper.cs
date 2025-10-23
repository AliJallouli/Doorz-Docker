namespace Domain.Helpers;

public static class EmailTemplateHelper
{
    public static Dictionary<string, string> LoadEmailTranslations(string templateName, string languageCode)
    {
        var common = new Dictionary<string, string>
        {
            {
                "Greeting", languageCode switch
                {
                    "fr" => "Bonjour,",
                    "en" => "Hello,",
                    "nl" => "Hallo,",
                    "de" => "Hallo,",
                    _ => "Hello," // Fallback en anglais pour éviter une exception
                }
            },
            { "Lang", languageCode } // Ajouter pour [Lang]
        };

        Dictionary<string, string> templateSpecific = templateName switch
        {
            "InvitationEmailTemplate" => GetInvitationEmailTemplateTranslations(languageCode),
            "ResetPasswordEmail" => GetResetPasswordTranslations(languageCode),
            "ConfirmationEmail" => GetConfirmationTranslations(languageCode),
            "ResentOtpEmail" => GetResentOtpTranslations(languageCode),
            "PasswordChangedConfirmation" => GetPasswordChangedConfirmationTranslations(languageCode),
            "ColleagueInvitationEmail" => GetColleagueInvitationEmailTranslations(languageCode),
            "ContactUserConfirmationEmail" => GetContactUserConfirmationTranslations(languageCode),
            "ContactSupportNotificationEmail" => GetContactSupportNotificationTranslations(languageCode),
            "EmailUpdatedConfirmation" => GetEmailUpdatedConfirmationTranslations(languageCode),
            _ => throw new Exception("Template non pris en charge.")
        };

        foreach (var kv in common)
        {
            templateSpecific[kv.Key] = kv.Value;
        }

        return templateSpecific;
    }
    // Confirmation de changement de l'email
    private static Dictionary<string, string> GetEmailUpdatedConfirmationTranslations(string lang) =>
    lang switch
    {
        "fr" => new()
        {
            { "Subject", "Votre adresse email a été modifiée" },
            { "EmailTitle", "Confirmation de changement d’adresse email" },
            { "Greeting", "Bonjour," },
            { "Intro", "Votre adresse email associée à <strong>[PlatformName]</strong> a été mise à jour avec succès." },
            { "Instructions", "Pour confirmer ce changement, cliquez sur le bouton ci-dessous :" },
            { "ValidateButton", "Confirmer mon adresse email" },
            { "ValidationLink", "[ValidationLink]" }, // remplacé dynamiquement
            { "OtpInstruction", "Puis, entrez ce code de vérification sur la page de confirmation :" },
            { "OtpCode", "[OtpCode]" }, // remplacé dynamiquement
            { "FooterNote", "Ce message a été envoyé automatiquement par <strong>[PlatformName]</strong>. Si vous n’êtes pas à l’origine de ce changement, contactez notre support." },
            { "Lang", "fr" }
        },
        "en" => new()
        {
            { "Subject", "Your email address has been updated" },
            { "EmailTitle", "Email Address Update Confirmation" },
            { "Greeting", "Hello," },
            { "Intro", "Your email address associated with <strong>[PlatformName]</strong> has been successfully updated." },
            { "Instructions", "To confirm this change, click the button below:" },
            { "ValidateButton", "Confirm my email address" },
            { "ValidationLink", "[ValidationLink]" },
            { "OtpInstruction", "Then enter this verification code on the confirmation page:" },
            { "OtpCode", "[OtpCode]" },
            { "FooterNote", "This message was sent automatically by <strong>[PlatformName]</strong>. If you did not make this change, please contact our support." },
            { "Lang", "en" }
        },
        "nl" => new()
        {
            { "Subject", "Uw e-mailadres is gewijzigd" },
            { "EmailTitle", "Bevestiging van e-mailadreswijziging" },
            { "Greeting", "Hallo," },
            { "Intro", "Uw e-mailadres gekoppeld aan <strong>[PlatformName]</strong> is succesvol bijgewerkt." },
            { "Instructions", "Klik op de onderstaande knop om deze wijziging te bevestigen:" },
            { "ValidateButton", "Bevestig mijn e-mailadres" },
            { "ValidationLink", "[ValidationLink]" },
            { "OtpInstruction", "Voer vervolgens deze verificatiecode in op de bevestigingspagina:" },
            { "OtpCode", "[OtpCode]" },
            { "FooterNote", "Dit bericht is automatisch verzonden door <strong>[PlatformName]</strong>. Als u deze wijziging niet heeft aangebracht, neem dan contact op met onze support." },
            { "Lang", "nl" }
        },
        "de" => new()
        {
            { "Subject", "Ihre E-Mail-Adresse wurde geändert" },
            { "EmailTitle", "Bestätigung der E-Mail-Änderung" },
            { "Greeting", "Hallo," },
            { "Intro", "Ihre mit <strong>[PlatformName]</strong> verknüpfte E-Mail-Adresse wurde erfolgreich aktualisiert." },
            { "Instructions", "Klicken Sie auf die Schaltfläche unten, um diese Änderung zu bestätigen:" },
            { "ValidateButton", "Meine E-Mail-Adresse bestätigen" },
            { "ValidationLink", "[ValidationLink]" },
            { "OtpInstruction", "Geben Sie dann diesen Bestätigungscode auf der Seite ein:" },
            { "OtpCode", "[OtpCode]" },
            { "FooterNote", "Diese Nachricht wurde automatisch von <strong>[PlatformName]</strong> gesendet. Wenn Sie diese Änderung nicht vorgenommen haben, kontaktieren Sie bitte unseren Support." },
            { "Lang", "de" }
        },
        _ => throw new Exception("Langue non supportée pour EmailUpdatedConfirmation.")
    };

    // 1️⃣ InvitationEmail
    private static Dictionary<string, string> GetInvitationEmailTemplateTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                { "Subject", "Invitation à administrer [EntityName] ([EntityType])" },
                { "EmailTitle", "Invitation à administrer [EntityName]" },
                { "Greeting", "Bonjour," },
                { "InvitationMessage", "Vous avez été invité à administrer <strong>[EntityName]</strong> en tant que <strong>[RoleName]</strong> sur <strong>[PlatformName]</strong>." },
                { "RoleAssigned", "Vous aurez le rôle de <strong>[RoleName]</strong> pour gérer cette entité." },
                { "Instructions", "Pour accepter l'invitation, cliquez sur le bouton ci-dessous :" },
                { "InviteLinkText", "Accepter l'invitation" },
                { "LinkNote", "Si le bouton ne fonctionne pas, vous pouvez copier et coller ce lien dans votre navigateur :"},
                { "FooterNote", "Cet email a été envoyé automatiquement par <strong>[PlatformName]</strong>. Pour toute question, contactez notre support." },
                { "Lang", "fr" }
            },
            "en" => new()
            {
                { "Subject", "Invitation to administer [EntityName] ([EntityType])" },
                { "EmailTitle", "Invitation to Administer [EntityName]" },
                { "Greeting", "Hello," },
                { "InvitationMessage", "You have been invited to administer <strong>[EntityName]</strong> as a <strong>[RoleName]</strong> on <strong>[PlatformName]</strong>." },
                { "RoleAssigned", "You will have the role of <strong>[RoleName]</strong> to manage this entity." },
                { "Instructions", "To accept the invitation, click the button below:" },
                { "InviteLinkText", "Accept Invitation" },
                { "LinkNote", "If the button does not work, you can copy and paste this link into your browser:"},
                { "FooterNote", "This email was sent automatically by <strong>[PlatformName]</strong>. For any questions, contact our support." },
                { "Lang", "en" }
            },
            "de" => new()
            {
                { "Subject", "Einladung zur Verwaltung von [EntityName] ([EntityType])" },
                { "EmailTitle", "Einladung zur Verwaltung von [EntityName]" },
                { "Greeting", "Hallo," },
                { "InvitationMessage", "Sie wurden eingeladen, <strong>[EntityName]</strong> als <strong>[RoleName]</strong> auf <strong>[PlatformName]</strong> zu verwalten." },
                { "RoleAssigned", "Sie haben die Rolle <strong>[RoleName]</strong>, um diese Entität zu verwalten." },
                { "Instructions", "Um die Einladung anzunehmen, klicken Sie auf die Schaltfläche unten:" },
                { "InviteLinkText", "Einladung annehmen" },
                { "LinkNote", "Falls der Button nicht funktioniert, können Sie diesen Link in Ihren Browser kopieren und einfügen:"},
                { "FooterNote", "Diese E-Mail wurde automatisch von <strong>[PlatformName]</strong> gesendet. Bei Fragen wenden Sie sich an unseren Support." },
                { "Lang", "de" }
            },
            "nl" => new()
            {
                { "Subject", "Uitnodiging om [EntityName] te beheren ([EntityType])" },
                { "EmailTitle", "Uitnodiging om [EntityName] te beheren" },
                { "Greeting", "Hallo," },
                { "InvitationMessage", "U bent uitgenodigd om <strong>[EntityName]</strong> te beheren als <strong>[RoleName]</strong> op <strong>[PlatformName]</strong>." },
                { "RoleAssigned", "U krijgt de rol van <strong>[RoleName]</strong> om deze entiteit te beheren." },
                { "Instructions", "Om de uitnodiging te accepteren, klik op de knop hieronder:" },
                { "InviteLinkText", "Uitnodiging accepteren" },
                { "LinkNote", "Als de knop niet werkt, kunt u deze link kopiëren en plakken in uw browser:"},
                { "FooterNote", "Deze e-mail is automatisch verzonden door <strong>[PlatformName]</strong>. Neem bij vragen contact op met onze support." },
                { "Lang", "nl" }
            },
            _ => throw new Exception("Langue non supportée pour InvitationEmailTemplate.")
        };
    // 2️⃣ PasswordResetEmail
    private static Dictionary<string, string> GetResetPasswordTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                { "Subject", "Réinitialisation de votre mot de passe" },
                { "EmailTitle", "Réinitialisation de votre mot de passe" },
                { "Intro", "Vous avez demandé la réinitialisation du mot de passe pour votre compte sur <strong>[PlatformName]</strong>." },
                { "IpInfo", "Cette demande a été effectuée depuis :" },
                { "IpAddressLabel", "Adresse IP" },
                { "UserAgentLabel", "Navigateur/Appareil" },
                { "Instructions", "Pour réinitialiser votre mot de passe, cliquez sur le bouton ci-dessous et suivez les instructions :" },
                { "ResetLinkText", "Réinitialiser mon mot de passe" },
                { "ExpirationNotice", "Ce lien expire dans 24 heures. Si vous n'avez pas demandé cette réinitialisation, ignorez cet email." },
                { "OtpCodeLabel", "Code OTP :" },
                { "FooterNote", "Cet email a été envoyé automatiquement par <strong>[PlatformName]</strong>. Si vous avez des questions, contactez notre support." },
                { "Greeting", "Bonjour," },
                { "Lang", "fr" }
            },
            "en" => new()
            {
                { "Subject", "Reset your password" },
                { "EmailTitle", "Password Reset" },
                { "Intro", "You have requested a password reset for your account on <strong>[PlatformName]</strong>." },
                { "IpInfo", "This request was made from:" },
                { "IpAddressLabel", "IP Address" },
                { "UserAgentLabel", "Browser/Device" },
                { "Instructions", "To reset your password, click the button below and follow the instructions:" },
                { "ResetLinkText", "Reset my password" },
                { "ExpirationNotice", "This link expires in 24 hours. If you did not request this reset, please ignore this email." },
                { "OtpCodeLabel", "OTP Code:" },
                { "FooterNote", "This email was sent automatically by <strong>[PlatformName]</strong>. If you have any questions, contact our support." },
                { "Greeting", "Hello," },
                { "Lang", "en" }
            },
            "de" => new()
            {
                { "Subject", "Zurücksetzen Ihres Passworts" },
                { "EmailTitle", "Passwort zurücksetzen" },
                { "Intro", "Sie haben eine Passwortzurücksetzung für Ihr Konto bei <strong>[PlatformName]</strong> angefordert." },
                { "IpInfo", "Diese Anfrage wurde von folgendem Gerät gesendet:" },
                { "IpAddressLabel", "IP-Adresse" },
                { "UserAgentLabel", "Browser/Gerät" },
                { "Instructions", "Um Ihr Passwort zurückzusetzen, klicken Sie auf die Schaltfläche unten und folgen Sie den Anweisungen:" },
                { "ResetLinkText", "Mein Passwort zurücksetzen" },
                { "ExpirationNotice", "Dieser Link läuft in 24 Stunden ab. Wenn Sie diese Zurücksetzung nicht angefordert haben, ignorieren Sie diese E-Mail." },
                { "OtpCodeLabel", "OTP-Code:" },
                { "FooterNote", "Diese E-Mail wurde automatisch von <strong>[PlatformName]</strong> gesendet. Bei Fragen wenden Sie sich an unseren Support." },
                { "Greeting", "Hallo," },
                { "Lang", "de" }
            },
            "nl" => new()
            {
                { "Subject", "Uw wachtwoord opnieuw instellen" },
                { "EmailTitle", "Wachtwoord opnieuw instellen" },
                { "Intro", "U heeft een verzoek ingediend om het wachtwoord voor uw account op <strong>[PlatformName]</strong> opnieuw in te stellen." },
                { "IpInfo", "Dit verzoek werd gedaan vanaf:" },
                { "IpAddressLabel", "IP-adres" },
                { "UserAgentLabel", "Browser/Apparaat" },
                { "Instructions", "Om uw wachtwoord opnieuw in te stellen, klik op de knop hieronder en volg de instructies:" },
                { "ResetLinkText", "Mijn wachtwoord opnieuw instellen" },
                { "ExpirationNotice", "Deze link vervalt binnen 24 uur. Als u deze reset niet heeft aangevraagd, negeer deze e-mail." },
                { "OtpCodeLabel", "OTP-code:" },
                { "FooterNote", "Deze e-mail is automatisch verzonden door <strong>[PlatformName]</strong>. Neem bij vragen contact op met onze support." },
                { "Greeting", "Hallo," },
                { "Lang", "nl" }
            },
            _ => throw new Exception("Langue non supportée pour ResetPasswordEmail.")
        };
    // 3️⃣ ConfirmationEmail
    private static Dictionary<string, string> GetConfirmationTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                { "Subject", "Confirmez votre adresse email" },
                { "EmailTitle", "Vérification de votre adresse email" },
                { "Intro", "Merci de vous être inscrit sur <strong>[PlatformName]</strong> !" },
                { "Instructions", "Pour activer votre compte, veuillez :" },
                { "ValidateButton", "Cliquer ici pour valider votre email" },
                { "OtpInstruction", "Puis entrez ce code de vérification (OTP) sur la page :" },
                { "FooterNote", "Ce message a été envoyé automatiquement par <strong>[PlatformName]</strong>. Si vous n'avez pas créé de compte, vous pouvez ignorer cet email." },
                { "Greeting", "Bonjour," },
                { "Lang", "fr" }
            },
            "en" => new()
            {
                { "Subject", "Confirm your email address" },
                { "EmailTitle", "Verify your email address" },
                { "Intro", "Thank you for signing up on <strong>[PlatformName]</strong>!" },
                { "Instructions", "To activate your account, please:" },
                { "ValidateButton", "Click here to verify your email" },
                { "OtpInstruction", "Then enter this verification code (OTP) on the page:" },
                { "FooterNote", "This message was sent automatically by <strong>[PlatformName]</strong>. If you did not create an account, you can ignore this email." },
                { "Greeting", "Hello," },
                { "Lang", "en" }
            },
            "de" => new()
            {
                { "Subject", "Bestätigen Sie Ihre E-Mail-Adresse" },
                { "EmailTitle", "Überprüfung Ihrer E-Mail-Adresse" },
                { "Intro", "Vielen Dank, dass Sie sich bei <strong>[PlatformName]</strong> angemeldet haben!" },
                { "Instructions", "Um Ihr Konto zu aktivieren, bitte:" },
                { "ValidateButton", "Hier klicken, um Ihre E-Mail zu verifizieren" },
                { "OtpInstruction", "Geben Sie dann diesen Verifizierungscode (OTP) auf der Seite ein:" },
                { "FooterNote", "Diese Nachricht wurde automatisch von <strong>[PlatformName]</strong> gesendet. Wenn Sie kein Konto erstellt haben, können Sie diese E-Mail ignorieren." },
                { "Greeting", "Hallo," },
                { "Lang", "de" }
            },
            "nl" => new()
            {
                { "Subject", "Bevestig uw e-mailadres" },
                { "EmailTitle", "Verifieer uw e-mailadres" },
                { "Intro", "Bedankt voor uw aanmelding bij <strong>[PlatformName]</strong>!" },
                { "Instructions", "Om uw account te activeren, gelieve:" },
                { "ValidateButton", "Klik hier om uw e-mail te verifiëren" },
                { "OtpInstruction", "Voer vervolgens deze verificatiecode (OTP) in op de pagina:" },
                { "FooterNote", "Dit bericht is automatisch verzonden door <strong>[PlatformName]</strong>. Als u geen account heeft aangemaakt, kunt u deze e-mail negeren." },
                { "Greeting", "Hallo," },
                { "Lang", "nl" }
            },
            _ => throw new Exception("Langue non supportée pour ConfirmationEmail.")
        };
    // 4️⃣ ResentOtpEmail
    private static Dictionary<string, string> GetResentOtpTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                { "Subject", "Votre nouveau code OTP" },
                { "EmailTitle", "Votre nouveau code de vérification" },
                { "OtpInstruction", "Voici votre nouveau code de vérification pour confirmer votre adresse email :" },
                { "OtpCodeLabel", "Code OTP :" },
                { "FooterNote", "Ce code est valable pour une durée limitée. Si vous n'avez pas demandé ce code, ignorez cet email." },
                { "Greeting", "Bonjour," },
                { "Lang", "fr" }
            },
            "en" => new()
            {
                { "Subject", "Your new OTP code" },
                { "EmailTitle", "Your new verification code" },
                { "OtpInstruction", "Here is your new verification code to confirm your email address:" },
                { "OtpCodeLabel", "OTP Code:" },
                { "FooterNote", "This code is valid for a limited time. If you did not request this code, please ignore this email." },
                { "Greeting", "Hello," },
                { "Lang", "en" }
            },
            "de" => new()
            {
                { "Subject", "Ihr neuer OTP-Code" },
                { "EmailTitle", "Ihr neuer Verifizierungscode" },
                { "OtpInstruction", "Hier ist Ihr neuer Verifizierungscode, um Ihre E-Mail-Adresse zu bestätigen:" },
                { "OtpCodeLabel", "OTP-Code:" },
                { "FooterNote", "Dieser Code ist für eine begrenzte Zeit gültig. Wenn Sie diesen Code nicht angefordert haben, ignorieren Sie diese E-Mail." },
                { "Greeting", "Hallo," },
                { "Lang", "de" }
            },
            "nl" => new()
            {
                { "Subject", "Uw nieuwe OTP-code" },
                { "EmailTitle", "Uw nieuwe verificatiecode" },
                { "OtpInstruction", "Hier is uw nieuwe verificatiecode om uw e-mailadres te bevestigen:" },
                { "OtpCodeLabel", "OTP-code:" },
                { "FooterNote", "Deze code is slechts een beperkte tijd geldig. Als u deze code niet heeft aangevraagd, negeer deze e-mail." },
                { "Greeting", "Hallo," },
                { "Lang", "nl" }
            },
            _ => throw new Exception("Langue non supportée pour ResentOtpEmail.")
        };
    // 5️⃣ PasswordChangedConfirmation
    private static Dictionary<string, string> GetPasswordChangedConfirmationTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                { "Subject", "Confirmation de changement de mot de passe" },
                { "EmailTitle", "Votre mot de passe a été modifié" },
                { "Intro", "Votre mot de passe pour votre compte sur <strong>[PlatformName]</strong> a été modifié avec succès." },
                { "IpInfo", "Ce changement a été effectué depuis :" },
                { "IpAddressLabel", "Adresse IP" },
                { "UserAgentLabel", "Navigateur/Appareil" },
                { "ConfirmationMessage", "Si vous avez effectué ce changement, aucune action n'est requise." },
                { "SecurityNotice", "Si vous n'avez pas modifié votre mot de passe, veuillez contacter notre support immédiatement." },
                { "FooterNote", "Cet email a été envoyé automatiquement par <strong>[PlatformName]</strong>. Pour toute question, contactez notre support." },
                { "Greeting", "Bonjour," },
                { "Lang", "fr" }
            },
            "en" => new()
            {
                { "Subject", "Password Change Confirmation" },
                { "EmailTitle", "Your Password Has Been Changed" },
                { "Intro", "Your password for your account on <strong>[PlatformName]</strong> has been successfully changed." },
                { "IpInfo", "This change was made from:" },
                { "IpAddressLabel", "IP Address" },
                { "UserAgentLabel", "Browser/Device" },
                { "ConfirmationMessage", "If you made this change, no further action is required." },
                { "SecurityNotice", "If you did not change your password, please contact our support immediately." },
                { "FooterNote", "This email was sent automatically by <strong>[PlatformName]</strong>. For any questions, contact our support." },
                { "Greeting", "Hello," },
                { "Lang", "en" }
            },
            "de" => new()
            {
                { "Subject", "Bestätigung der Passwortänderung" },
                { "EmailTitle", "Ihr Passwort wurde geändert" },
                { "Intro", "Ihr Passwort für Ihr Konto bei <strong>[PlatformName]</strong> wurde erfolgreich geändert." },
                { "IpInfo", "Diese Änderung wurde von folgendem Gerät vorgenommen:" },
                { "IpAddressLabel", "IP-Adresse" },
                { "UserAgentLabel", "Browser/Gerät" },
                { "ConfirmationMessage", "Wenn Sie diese Änderung vorgenommen haben, ist keine weitere Aktion erforderlich." },
                { "SecurityNotice", "Wenn Sie Ihr Passwort nicht geändert haben, kontaktieren Sie bitte sofort unseren Support." },
                { "FooterNote", "Diese E-Mail wurde automatisch von <strong>[PlatformName]</strong> gesendet. Bei Fragen wenden Sie sich an unseren Support." },
                { "Greeting", "Hallo," },
                { "Lang", "de" }
            },
            "nl" => new()
            {
                { "Subject", "Bevestiging van wachtwoordwijziging" },
                { "EmailTitle", "Uw wachtwoord is gewijzigd" },
                { "Intro", "Uw wachtwoord voor uw account op <strong>[PlatformName]</strong> is succesvol gewijzigd." },
                { "IpInfo", "Deze wijziging werd gedaan vanaf:" },
                { "IpAddressLabel", "IP-adres" },
                { "UserAgentLabel", "Browser/Apparaat" },
                { "ConfirmationMessage", "Als u deze wijziging heeft aangebracht, is geen verdere actie vereist." },
                { "SecurityNotice", "Als u uw wachtwoord niet heeft gewijzigd, neem dan onmiddellijk contact op met onze support." },
                { "FooterNote", "Deze e-mail is automatisch verzonden door <strong>[PlatformName]</strong>. Neem bij vragen contact op met onze support." },
                { "Greeting", "Hallo," },
                { "Lang", "nl" }
            },
            _ => throw new Exception("Langue non supportée pour PasswordChangedConfirmation.")
        };
    // 6️⃣ AdminInvitationEmail
    private static Dictionary<string, string> GetColleagueInvitationEmailTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                { "Subject", "Invitation à rejoindre l'équipe de [EntityName] ([EntityType])" },
                { "EmailTitle", "Invitation à rejoindre l'équipe de [EntityName]" },
                { "Greeting", "Bonjour," },
                { "InvitationMessage", "Vous avez été invité à rejoindre l'équipe de <strong>[EntityName]</strong> en tant que <strong>[RoleName]</strong> sur <strong>[PlatformName]</strong>." },
                { "RoleAssigned", "Vous aurez le rôle de <strong>[RoleName]</strong> au sein de cette équipe." },
                { "Instructions", "Pour accepter l'invitation, cliquez sur le bouton ci-dessous :" },
                { "InviteLinkText", "Accepter l'invitation" },
                { "LinkNote", "Si le bouton ne fonctionne pas, vous pouvez copier et coller ce lien dans votre navigateur :"},
                { "ExpirationNotice", "Cette invitation expire dans 7 jours. Si vous ne souhaitez pas rejoindre, ignorez cet email." },
                { "Closing", "Nous avons hâte de vous accueillir dans l'équipe !" },
                { "FooterNote", "Cet email a été envoyé automatiquement par <strong>[PlatformName]</strong>. Pour toute question, contactez notre support." },
                { "Lang", "fr" }
            },
            "en" => new()
            {
                { "Subject", "Invitation to join the team at [EntityName] ([EntityType])" },
                { "EmailTitle", "Invitation to Join the Team at [EntityName]" },
                { "Greeting", "Hello," },
                { "InvitationMessage", "You have been invited to join the team at <strong>[EntityName]</strong> as a <strong>[RoleName]</strong> on <strong>[PlatformName]</strong>." },
                { "RoleAssigned", "You will have the role of <strong>[RoleName]</strong> within this team." },
                { "Instructions", "To accept the invitation, click the button below:" },
                { "InviteLinkText", "Accept Invitation" },
                { "LinkNote", "If the button does not work, you can copy and paste this link into your browser:"},
                { "ExpirationNotice", "This invitation expires in 7 days. If you do not wish to join, please ignore this email." },
                { "Closing", "We look forward to welcoming you to the team!" },
                { "FooterNote", "This email was sent automatically by <strong>[PlatformName]</strong>. For any questions, contact our support." },
                { "Lang", "en" }
            },
            "de" => new()
            {
                { "Subject", "Einladung zum Team von [EntityName] ([EntityType])" },
                { "EmailTitle", "Einladung zum Team von [EntityName]" },
                { "Greeting", "Hallo," },
                { "InvitationMessage", "Sie wurden eingeladen, dem Team von <strong>[EntityName]</strong> als <strong>[RoleName]</strong> auf <strong>[PlatformName]</strong> beizutreten." },
                { "RoleAssigned", "Sie haben die Rolle <strong>[RoleName]</strong> innerhalb dieses Teams." },
                { "Instructions", "Um die Einladung anzunehmen, klicken Sie auf die Schaltfläche unten:" },
                { "InviteLinkText", "Einladung annehmen" },
                { "LinkNote", "Falls der Button nicht funktioniert, können Sie diesen Link in Ihren Browser kopieren und einfügen:"},
                { "ExpirationNotice", "Diese Einladung läuft in 7 Tagen ab. Wenn Sie nicht beitreten möchten, ignorieren Sie diese E-Mail." },
                { "Closing", "Wir freuen uns darauf, Sie im Team willkommen zu heißen!" },
                { "FooterNote", "Diese E-Mail wurde automatisch von <strong>[PlatformName]</strong> gesendet. Bei Fragen wenden Sie sich an unseren Support." },
                { "Lang", "de" }
            },
            "nl" => new()
            {
                { "Subject", "Uitnodiging om deel te nemen aan het team van [EntityName] ([EntityType])" },
                { "EmailTitle", "Uitnodiging om deel te nemen aan het team van [EntityName]" },
                { "Greeting", "Hallo," },
                { "InvitationMessage", "U bent uitgenodigd om deel te nemen aan het team van <strong>[EntityName]</strong> als <strong>[RoleName]</strong> op <strong>[PlatformName]</strong>." },
                { "RoleAssigned", "U krijgt de rol van <strong>[RoleName]</strong> binnen dit team." },
                { "Instructions", "Om de uitnodiging te accepteren, klik op de knop hieronder:" },
                { "InviteLinkText", "Uitnodiging accepteren" },
                { "LinkNote", "Als de knop niet werkt, kunt u deze link kopiëren en plakken in uw browser:"},
                { "ExpirationNotice", "Deze uitnodiging verloopt binnen 7 dagen. Als u niet wilt deelnemen, negeer deze e-mail." },
                { "Closing", "We kijken ernaar uit om u in het team te verwelkomen!" },
                { "FooterNote", "Deze e-mail is automatisch verzonden door <strong>[PlatformName]</strong>. Neem bij vragen contact op met onze support." },
                { "Lang", "nl" }
            },
            _ => throw new Exception("Langue non supportée pour ColleagueInvitationEmail.")
        };
    private static Dictionary<string, string> GetContactUserConfirmationTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                { "Subject", "Merci pour votre message" },
                { "EmailTitle", "Confirmation de réception de votre message" },
                {
                    "ConfirmationMessage",
                    "Nous avons bien reçu votre message. Notre équipe vous répondra dans les plus brefs délais."
                },
                { "ThankYou", "Merci de votre confiance." },
                { "FooterNote", "Cet email est généré automatiquement. Merci de ne pas y répondre." },
                { "Greeting", "Bonjour," },
                { "Lang", "fr" },
                { "SubjectLabel", "Sujet" }, 
                { "MessageLabel", "Message" }
            },
            "en" => new()
            {
                { "Subject", "Thank you for your message" },
                { "EmailTitle", "We have received your message" },
                {
                    "ConfirmationMessage",
                    "We have successfully received your message. Our team will get back to you shortly."
                },
                { "ThankYou", "Thank you for contacting us." },
                { "FooterNote", "This email was generated automatically. Please do not reply." },
                { "Greeting", "Hello," },
                { "Lang", "en" },
                { "SubjectLabel", "Subject" },
                { "MessageLabel", "Message" }
            },
            "nl" => new()
            {
                { "Subject", "Bedankt voor uw bericht" },
                { "EmailTitle", "We hebben uw bericht ontvangen" },
                {
                    "ConfirmationMessage",
                    "We hebben uw bericht goed ontvangen. Ons team neemt spoedig contact met u op."
                },
                { "ThankYou", "Dank u voor uw vertrouwen." },
                { "FooterNote", "Deze e-mail is automatisch gegenereerd. Gelieve niet te antwoorden." },
                { "Greeting", "Hallo," },
                { "Lang", "nl" },
                { "SubjectLabel", "Onderwerp" },
                { "MessageLabel", "Bericht" } 
            },
            "de" => new()
            {
                { "Subject", "Vielen Dank für Ihre Nachricht" },
                { "EmailTitle", "Wir haben Ihre Nachricht erhalten" },
                {
                    "ConfirmationMessage",
                    "Wir haben Ihre Nachricht erhalten. Unser Team wird sich in Kürze bei Ihnen melden."
                },
                { "ThankYou", "Vielen Dank für Ihr Vertrauen." },
                { "FooterNote", "Diese E-Mail wurde automatisch generiert. Bitte nicht antworten." },
                { "Greeting", "Hallo," },
                { "Lang", "de" },
                { "SubjectLabel", "Betreff" }, 
                { "MessageLabel", "Nachricht" } 
            },
            _ => throw new Exception("Langue non supportée pour ContactUserConfirmationEmail.")
        };

    private static Dictionary<string, string> GetContactSupportNotificationTranslations(string lang) =>
        lang switch
        {
            "fr" => new()
            {
                
                { "Subject", "Nous avons été contacté" },
                { "EmailTitle", "Nouveau message de contact reçu" },
                { "Intro", "Vous avez reçu un nouveau message depuis le formulaire de contact." },
                { "Details", "Détails du message :" },
                { "UtilisateurLabel", "Utilisateur" },
                { "FullNameLabel", "Nom complet" },
                { "EmailLabel", "Email" },
                { "SubjectLabel", "Sujet" },
                { "MessageLabel", "Message" },
                { "PhoneLabel", "GSM" },
                { "IpAddressLabel", "Adresse IP" },
                { "UserAgentLabel", "Navigateur/Appareil" },
                { "FooterNote", "Merci de traiter cette demande dans les plus brefs délais." },
                { "Greeting", "Bonjour," },
                { "Lang", "fr" }
            },
            "en" => new()
            {
                { "Subject", "New contact message received" },
                { "EmailTitle", "New contact message received" },
                { "Intro", "You have received a new message from the contact form." },
                { "Details", "Message details:" },
                { "FullNameLabel", "Full Name" },
                { "EmailLabel", "Email" },
                { "SubjectLabel", "Subject" },
                { "MessageLabel", "Message" },
                { "IpAddressLabel", "IP Address" },
                { "UserAgentLabel", "Browser/Device" },
                { "FooterNote", "Please handle this request as soon as possible." },
                { "Greeting", "Hello," },
                { "Lang", "en" }
            },
            "nl" => new()
            {
                { "Subject", "Nieuw contactbericht ontvangen" },
                { "EmailTitle", "Nieuw contactbericht ontvangen" },
                { "Intro", "U heeft een nieuw bericht ontvangen via het contactformulier." },
                { "Details", "Berichtdetails:" },
                { "FullNameLabel", "Volledige naam" },
                { "EmailLabel", "E-mail" },
                { "SubjectLabel", "Onderwerp" },
                { "MessageLabel", "Bericht" },
                { "IpAddressLabel", "IP-adres" },
                { "UserAgentLabel", "Browser/Apparaat" },
                { "FooterNote", "Behandel dit verzoek zo snel mogelijk." },
                { "Greeting", "Hallo," },
                { "Lang", "nl" }
            },
            "de" => new()
            {
                { "Subject", "Neue Kontaktanfrage erhalten" },
                { "EmailTitle", "Neue Kontaktanfrage erhalten" },
                { "Intro", "Sie haben eine neue Nachricht über das Kontaktformular erhalten." },
                { "Details", "Nachrichtendetails:" },
                { "FullNameLabel", "Vollständiger Name" },
                { "EmailLabel", "E-Mail" },
                { "SubjectLabel", "Betreff" },
                { "MessageLabel", "Nachricht" },
                { "IpAddressLabel", "IP-Adresse" },
                { "UserAgentLabel", "Browser/Gerät" },
                { "FooterNote", "Bitte bearbeiten Sie diese Anfrage so schnell wie möglich." },
                { "Greeting", "Hallo," },
                { "Lang", "de" }
            },
            _ => throw new Exception("Langue non supportée pour ContactSupportNotificationEmail.")
        };
}