namespace WebApi.Constants;

public static class ResponseKeys
{
    public const string NOT_AUTHENTICATED="NOT_AUTHENTICATED";
    public const string AUTHENTICATED ="AUTHENTICATED";

    public const string REFRESH_SUCCESS = "REFRESH_SUCCESS";
    // Global
    public const string SUCCESS = "SUCCESS";
    public const string INVALID_MODEL_STATE = "INVALID_MODEL_STATE";
    public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";

    // Auth
    public const string ALREADY_AUTHENTICATED = "ALREADY_AUTHENTICATED";
    public const string LOGIN_SUCCESS = "LOGIN_SUCCESS";
    public const string LOGOUT_SUCCESS = "LOGOUT_SUCCESS";
    public const string REGISTRATION_SUCCESS = "REGISTRATION_SUCCESS";
    public const string EMAIL_CONFIRMATION_SENT = "EMAIL_CONFIRMATION_SENT";

    // Invitation
    public const string INVITATION_SENT = "INVITATION_SENT";
    public const string INVITATION_ACCEPTED = "INVITATION_ACCEPTED";

    // Validation
    public const string FIELD_REQUIRED = "FIELD_REQUIRED";
    public const string ENTITY_NAME_ALREADY_USED = "ENTITY_NAME_ALREADY_USED";

    // Fallback
    public const string UNKNOWN_ERROR = "UNKNOWN_ERROR";
    public const string EMAIL_CONFIRMATION_SUCCESS = "EMAIL_CONFIRMATION_SUCCESS";
    public const string AUTH_TEST_SUCCESS = "AUTH_TEST_SUCCESS";
    public const string USER_NOT_AUTHENTICATED = "USER_NOT_AUTHENTICATED";
    public const string COLLEAGUE_INVITED = "COLLEAGUE_INVITED";
    public const string COMPANY_ADMIN_INVITED = "COMPANY_ADMIN_INVITED";
    public const string INSTITUTION_ADMIN_INVITED = "INSTITUTION_ADMIN_INVITED";

    public const string PASSWORD_RESET_EMAIL_SENT = "PASSWORD_RESET_EMAIL_SENT";
}