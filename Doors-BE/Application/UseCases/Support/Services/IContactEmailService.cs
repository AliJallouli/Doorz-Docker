using Domain.Entities.Support;

namespace Application.UseCases.Support.Services;

public interface IContactEmailService
{ 
    Task NotifySupportAsync(ContactMessage message, string languageCode,string userAgent);


    Task AcknowledgeUserAsync(ContactMessage message, string languageCode);
}