namespace Infrastructure.Options;

public class EmailSenderOptions
{
    public EmailSenderDetail Support { get; set; } = new();
    public EmailSenderDetail Contact { get; set; } = new();
    public EmailSenderDetail NoReply { get; set; } = new();
}