namespace Domain.Entities;

public class EmailSettings
{
    public string SmtpServer { get; set; } = null!;
    public int SmtpPort { get; set; } = 1025;
    public string SenderName { get; set; } = null!;
    public string SenderEmail { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string BaseUrl { get; set; } = string.Empty; // Ajoutez cette ligne
    public bool EnableSsl { get; set; } // Ajoutez ceci pour "EnableSsl"
}