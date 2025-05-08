namespace Application;

public static class ErrorCodes
{
    public const string RefreshTokenLockTimeout = "RefreshTokenLockTimeout";
    public const string RefreshTokenAlreadyUsed = "RefreshTokenAlreadyUsed";
    public const string RefreshTokenAlreadyExists = "RefreshTokenAlreadyExists";
    public const string NoLegalConsentsProvided = "NoLegalConsentsProvided";
    public const string LegalDocumentNotFound = "LegalDocumentNotFound";
    public const string TooManyOtpResends = "TOO_MANY_OTP_RESENDS";
    public  const string OtpNotRequiredForThisTokenType = "OTP_NOT_REQUIRED_FOR_THIS_TOKEN_TYPE";
    public const string OtpNotAvailableForThisToken = "TOKEN_NOT_AVAILABLE";
    public const string TokenNotFound = "TOKEN_NOT_FOUND";
    public const string MissingConfirmationTokenOrOtp = "MISSING_CONFIRMATION_TOKEN_OR_OTP";
    public const string OtpTooManyAttempts = "OTP_TOO_MANY_ATTEMPTS";
    public const string OtpInvalid = "OTP_INVALID";
    public const string TooSoonForNewToken = "TOO_SOON_FOR_NEW_TOKEN";
    public const string UserNull = "AUTH.USER_NULL";
    public const string MissingEmail = "AUTH.MISSING_EMAIL";
    public const string TokenGenerationFailed = "AUTH.TOKEN_GENERATION_FAILED";
    public const string TokenInvalid = "TOKEN_INVALID";
    public const string UnexpectedError = "AUTH.UNEXPECTED";
    public const string TooManyTokenRequests = "TOO_MANY_TOKEN_REQUESTS";
    public const string TokenAlreadyUsed = "TOKEN_ALREADY_USED";
    public const string TokenRevoked = "TOKEN_REVOKED";
    public const string TokenExpired = "TOKEN_EXPIRED";
    public const string TokenTypeNotFound = "TOKEN_TYPE_NOT_FOUND";

    public const string PasswordResetSameAsOld = "PASSWORD_RESET_SAME_AS_OLD";
    public const string PasswordResetFailed = "PASSWORD_RESET_FAILED";

    public const string EmailAlreadyConfirmed = "EMAIL_ALREADY_CONFIRMED";
    public const string EmailConfirmationFailed = "EMAIL_CONFIRMATION_FAILED";
    public const string AdminRoleNotFound = "ADMIN_ROLE_NOT_FOUND";
    public const string EntityNameMissing = "ENTITY_NAME_MISSING";

    public const string InstitutionNameAlreadyUsed = "INSTITUTION_NAME_ALREADY_USED";
    public const string InstitutionTypeNotFound = "INSTITUTION_TYPE_NOT_FOUND";
    public const string InvalidInstitutionInput = "INVALID_INSTITUTION_INPUT";
    public const string InstitutionCreationFailed = "INSTITUTION_CREATION_FAILED";

    public const string CaCompanyNameAlreadyUsed = "COMPANY_NAME_ALREADY_USED";
    public const string CompanyCreationFailed = "COMPANY_CREATION_FAILED";
    public const string InvalidCompanyInput = "INVALID_COMPANY_INPUT";

    public const string EntityTypeNotFound = "ENTITY_TYPE_NOT_FOUND";
    public const string InviterNotLinkedToEntity = "INVITER_NOT_LINKED_TO_ENTITY";
    public const string InviterNotAdmin = "INVITER_NOT_ADMIN";
    public const string RoleNotValidForEntityType = "ROLE_NOT_VALID_FOR_ENTITY_TYPE";
    public const string EmailAlreadyUsed = "EMAIL_ALREADY_USED";
    public const string RoleNotFound = "ROLE_NOT_FOUND";
    public const string PublicEntityTypeNotFound = "PUBLIC_ENTITY_TYPE_NOT_FOUND";
    public const string UnsupportedPublicRole = "UNSUPPORTED_PUBLIC_ROLE";
    public const string UserNotFoundAfterEntityCreation = "USER_NOT_FOUND_AFTER_ENTITY_CREATION";
    public const string UserRegistrationFailed = "USER_REGISTRATION_FAILED";

    public const string InvitationInvalid = "INVITATION_INVALID";
    public const string InvitationDetailsMismatch = "INVITATION_DETAILS_MISMATCH";
    public const string UserAlreadyExists = "USER_ALREADY_EXISTS";
    public const string EntityTypeInvalid = "ENTITY_TYPE_INVALID";
    public const string EntityTypeUnsupported = "ENTITY_TYPE_UNSUPPORTED";
    public const string UserCreationFailed = "USER_CREATION_FAILED";

    public const string ColleagueRegistrationFailed = "COLLEAGUE_REGISTRATION_FAILED";

    // Register Admin
    public const string InvalidAdminRole = "INVALID_ADMIN_ROLE";
    public const string RegistrationFailed = "REGISTRATION_FAILED";

    public const string EmailSendingFailed = "EMAIL_SENDING_FAILED";

    // RefreshToken
    public const string RefreshTokenInvalidOrExpired = "REFRESH_TOKEN_INVALID_OR_EXPIRED";

    public const string RefreshTokenProcessFailed = "REFRESH_TOKEN_PROCESS_FAILED";

    // Logout
    public const string RefreshTokenRequired = "REFRESH_TOKEN_REQUIRED";
    public const string RefreshTokenInvalid = "REFRESH_TOKEN_INVALID";
    public const string UserNotFound = "USER_NOT_FOUND";

    public const string LogoutFailed = "LOGOUT_FAILED";

    // Login
    public const string TooManyFailedAttempts = "TOO_MANY_FAILED_ATTEMPTS";
    public const string InvalidLogin = "INVALID_LOGIN";
    public const string LoginAttemptSaveFailed = "LOGIN_ATTEMPT_SAVE_FAILED";
    public const string LoginFinalizationFailed = "LOGIN_FINALIZATION_FAILED";
    public const string InvitationDetailsMissing = "INVITATION_DETAILS_MISSING";
    public const string InstitutionDtoNull = "INSTITUTION_DTO_NULL";
    public const string InstitutionTypeIdInvalid = "INSTITUTION_TYPE_ID_INVALID";

    public const string CompanyDtoNull = "COMPANY_DTO_NULL";
    public const string CompanyNumberRequired = "COMPANY_NUMBER_REQUIRED";
    public const string CompanyNumberInvalidFormat = "COMPANY_NUMBER_INVALID_FORMAT";
    public const string CompanyNumberInvalidDigits = "COMPANY_NUMBER_INVALID_DIGITS";
    public const string CompanyNumberInvalid = "COMPANY_NUMBER_INVALID";

    public const string InvalidName = "INVALID_NAME";
    public const string EmailEmpty = "EMAIL_EMPTY";
    public const string InvalidEmailFormat = "INVALID_EMAIL_FORMAT";
    public const string EmailLengthOutOfRange = "EMAIL_LENGTH_OUT_OF_RANGE";
    public const string PasswordEmpty = "PASSWORD_EMPTY";
    public const string PasswordLengthInvalid = "PASSWORD_LENGTH_INVALID";
    public const string PasswordComplexityFailed = "PASSWORD_COMPLEXITY_FAILED";
}