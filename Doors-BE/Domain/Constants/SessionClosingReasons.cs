namespace Domain.Constants;

public static class SessionClosingReasons
{
    
    public const string Logout = "Logout";
    public const string PasswordReset = "PasswordReset";
    public const string PasswordChanged = "PasswordChanged";
    public const string EmailChanged = "EmailChanged";
    public const string TokenCompromised = "TokenCompromised";
    public const string AdminForcedLogout = "AdminForcedLogout";
}