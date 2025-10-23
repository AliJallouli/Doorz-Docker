namespace Application;

public static class ErrorCodes
{
   //*******************************   Shared *********************// 
   public const string EmailAlreadyUsed = "EMAIL_ALREADY_USED";
   public const string TokenAlreadyUsed = "TOKEN_ALREADY_USED";
   public const string TokenInvalid = "TOKEN_INVALID";
   public const string TokenGenerationFailed = "AUTH_TOKEN_GENERATION_FAILED";
   public const string InvalidEmailFormat = "INVALID_EMAIL_FORMAT";
   
    //*********************************  Auth  ***************//
    
    // service:
    public const string RefreshTokenAlreadyExists = "RefreshTokenAlreadyExists";
    public const string NoLegalConsentsProvided = "NoLegalConsentsProvided";
    public const string LegalDocumentNotFound = "LegalDocumentNotFound";
    public const string TooManyOtpResends = "TOO_MANY_OTP_RESENDS";
    public const string MissingEmail = "AUTH_MISSING_EMAIL";
    public const string UnexpectedError = "AUTH_UNEXPECTED";
    public const string TooManyTokenRequests = "TOO_MANY_TOKEN_REQUESTS";
    public const string TokenRevoked = "TOKEN_REVOKED";
    public const string TokenExpired = "TOKEN_EXPIRED";
    public const string TokenTypeNotFound = "TOKEN_TYPE_NOT_FOUND";
    public const string UserNull = "AUTH_USER_NULL";
    public const string TooSoonForNewToken = "TOO_SOON_FOR_NEW_TOKEN";
    public const string OtpTooManyAttempts = "OTP_TOO_MANY_ATTEMPTS";
    public const string OtpInvalid = "OTP_INVALID";
    public  const string OtpNotRequiredForThisTokenType = "OTP_NOT_REQUIRED_FOR_THIS_TOKEN_TYPE";
    public const string SessionEventNotFound ="SESSION_EVENT_NOT_FOUND";
    public const string NoSessionToRevock ="NO_SESSION_TO_REVOCK";
    
    //Password:
    public const string PasswordResetSameAsOld = "PASSWORD_RESET_SAME_AS_OLD";
    public const string PasswordResetFailed = "PASSWORD_RESET_FAILED";
    public const string NewPasswordInvalid ="NEW_PASSWORD_INVALID";
    public const string PasswordUpdateFailed = "PASSWORD_UPDATE_FAILED";
    
    // Email confirmation
    public const string EmailAlreadyConfirmed = "EMAIL_ALREADY_CONFIRMED";
    public const string EmailConfirmationFailed = "EMAIL_CONFIRMATION_FAILED";
    public const string OtpNotAvailableForThisToken = "TOKEN_NOT_AVAILABLE";
    public const  string InvalidNewEmail ="INVALID_NEW_EMAIL";
    public const string EmailUnchanged ="EMAIL_UNCHANGED";
    public const string EmailUpdateFailed = "EMAIL_UPDATE_FAILED";
    
    // Shared:
    public const string RoleNotFound = "ROLE_NOT_FOUND";
    public const string RefreshTokenRequired = "REFRESH_TOKEN_REQUIRED";
    public const string RefreshTokenInvalid = "REFRESH_TOKEN_INVALID";
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string MissingConfirmationTokenOrOtp = "MISSING_CONFIRMATION_TOKEN_OR_OTP";
    public const string TokenNotFound = "TOKEN_NOT_FOUND";
    public const string RateLimitExceeded = "RATE_LIMIT_EXCEEDED";
    public const string InvalidNameFields = "INVALID_NAME_FIELDS";
    public const string NameUpdateFailed ="NAME_UPDATE_FAILED";
    public const string NamesUnchanged ="NAMES_UNCHANGED";
    public const string ActualPasswordInvalid ="ACTUAL_PASSWORD_INVALID";
    
    // authentication
    public const string RefreshTokenLockTimeout = "RefreshTokenLockTimeout";
    public const string RefreshTokenAlreadyUsed = "RefreshTokenAlreadyUsed";
    public const string TooManyFailedAttempts = "TOO_MANY_FAILED_ATTEMPTS";
    public const string InvalidLogin = "INVALID_LOGIN";
    public const string LoginAttemptSaveFailed = "LOGIN_ATTEMPT_SAVE_FAILED";
    public const string LoginFinalizationFailed = "LOGIN_FINALIZATION_FAILED";
    public const string RefreshTokenInvalidOrExpired = "REFRESH_TOKEN_INVALID_OR_EXPIRED";
    public const string RefreshTokenProcessFailed = "REFRESH_TOKEN_PROCESS_FAILED";
    public const string LogoutFailed = "LOGOUT_FAILED";
    
    // Register
    public const string InvitationDetailsMissing = "INVITATION_DETAILS_MISSING";
    public const string RegistrationFailed = "REGISTRATION_FAILED";
    public const string InvalidAdminRole = "INVALID_ADMIN_ROLE";
    public const string ColleagueRegistrationFailed = "COLLEAGUE_REGISTRATION_FAILED";
    public const string UserCreationFailed = "USER_CREATION_FAILED";
    public const string InvitationInvalid = "INVITATION_INVALID";
    public const string InvitationDetailsMismatch = "INVITATION_DETAILS_MISMATCH";
    public const string UserAlreadyExists = "USER_ALREADY_EXISTS";
    public const string EntityTypeInvalid = "ENTITY_TYPE_INVALID";
    public const string EntityTypeUnsupported = "ENTITY_TYPE_UNSUPPORTED";
    public const string UnsupportedPublicRole = "UNSUPPORTED_PUBLIC_ROLE";
    public const string UserNotFoundAfterEntityCreation = "USER_NOT_FOUND_AFTER_ENTITY_CREATION";
    public const string UserRegistrationFailed = "USER_REGISTRATION_FAILED";
    public const string PublicEntityTypeNotFound = "PUBLIC_ENTITY_TYPE_NOT_FOUND";
    public const string PasswordInvalid ="PASSWORD_INVALID";
    

    // ************************   Invitation     ***********************//
    
    // Shared
    public const string InviterNotLinkedToEntity = "INVITER_NOT_LINKED_TO_ENTITY";
    public const string EntityTypeNotFound = "ENTITY_TYPE_NOT_FOUND";

   // Service:
   public const string EmailSendingFailed = "EMAIL_SENDING_FAILED";
   public const string RoleNotValidForEntityType = "ROLE_NOT_VALID_FOR_ENTITY_TYPE";
   public const string InviterNotAdmin = "INVITER_NOT_ADMIN";

    // Request
    public const string InvitationRequestInvalid ="INVITATION_REQUEST_INVALID";
    public const string InvitationRequestStatusInvalid="INVITATION_REQUEST_STATUS_INVALID";
    public const string InvitationRequestFailed = "INVITATION_REQUEST_FAILED";
    public const string InvitationRequestEntityTypeMissing = "INVITATION_REQUEST_ENTITY_TYPE_MISSING";
    
    // Superadmin
    public const string InvalidInput = "INVALID_INPUT";
    public const string InvalidEntityNameFormat = "INVALID_ENTITY_NAME_FORMAT";
    public const string InvalidCompanyNumberFormat = "INVALID_COMPANY_NUMBER_FORMAT";
    public const string EntityNameAlreadyUsed = "ENTITY_NAME_ALREADY_USED";
    public const string CompanyNumberAlreadyUsed ="COMPANY_NUMBER_ALREADY_USED";
    public const string UnsupportedEntityType = "UNSUPPORTED_ENTITY_TYPE";
    public const string InvitationRequestNotFound ="INVITATION_REQUEST_NOT_FOUND";
    public const string AssociationCreationFailed  ="ASSOCIATION_CREATION_FAILED";
    public const string StudentMovementCreationFailed = "STUDENT_MOVEMENT_CREATION_FAILED";
    public const string PublicOrganizationCreationFailed = "PUBLIC_ORGANIZATION_CREATION_FAILED";
    public const string InstitutionCreationFailed = "INSTITUTION_CREATION_FAILED";
    public const string CompanyCreationFailed = "COMPANY_CREATION_FAILED";
    public const string InstitutionTypeNotFound = "INSTITUTION_TYPE_NOT_FOUND";
    public const string AdminRoleNotFound = "ADMIN_ROLE_NOT_FOUND";
    public const string EntityNameMissing = "ENTITY_NAME_MISSING";
    
    public const string InvalidName = "COMPANY_NAME_MISSING";
}