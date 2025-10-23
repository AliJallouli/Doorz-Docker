namespace WebApi.Constants;

public static class ResponseKeys
{
    public const string SessionNotFound = "SESSION_NOT_FOUND";
    public const string NotAuthenticated="NOT_AUTHENTICATED";
    public const string Authenticated ="AUTHENTICATED";

    public const string RefreshSuccess = "REFRESH_SUCCESS";
    // Global
    public const string Success = "SUCCESS";
    public const string InvalidModelState = "INVALID_MODEL_STATE";

    // Auth
    public const string LoginSuccess = "LOGIN_SUCCESS";
    public const string LogoutSuccess = "LOGOUT_SUCCESS";
    public const string RegistrationSuccess = "REGISTRATION_SUCCESS";

    // Invitation
    public const string ColleagueInvited = "COLLEAGUE_INVITED";
    public const string CompanyAdminInvited = "COMPANY_ADMIN_INVITED";
    public const string InstitutionAdminInvited = "INSTITUTION_ADMIN_INVITED";
    public const string AssociationAdminInvited = "ASSOCIATION_ADMIN_INVITED";
    public const string StudentMovementAdminInvited = "STUDENT_MOVEMENT_ADMIN_INVITED";
    public const string PublicOrganizationAdminInvited = "PUBLIC_ORGANIZATION_ADMIN_INVITED";
    
    public const string AuthTestSuccess = "AUTH_TEST_SUCCESS";
    public const string UserNotAuthenticated = "USER_NOT_AUTHENTICATED";
    
    public const string PasswordResetEmailSent = "PASSWORD_RESET_EMAIL_SENT";
}