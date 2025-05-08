/// <summary>
/// Message transporté via RabbitMQ pour déclencher l'envoi d'un e-mail avec template.
/// Ce contrat est indépendant de l'application et utilisé par le publisher (Application) et le consumer (Infrastructure).
/// </summary>
public class SendTemplatedEmailMessage
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string TemplateName { get; set; }
    public Dictionary<string, string> TemplateData { get; set; }
    public string Language { get; set; }
    public string From { get; set; }
    public string FromName { get; set; }

    public SendTemplatedEmailMessage()
    {
        To = string.Empty;
        Subject = string.Empty;
        TemplateName = string.Empty;
        TemplateData = new Dictionary<string, string>();
        Language = string.Empty;
        From = string.Empty;
        FromName = string.Empty;
    }
}