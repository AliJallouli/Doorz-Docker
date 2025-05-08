namespace Infrastructure.Options;

public class EmailSenderDetail
{
    public string Address { get; set; } = string.Empty;
    public Dictionary<string, string> SenderName { get; set; } = new();
}