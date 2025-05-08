namespace Application.UseCases.Invitation.Service;

public interface IAddEntityUseCase<TCreateDto, TDto>
{
    Task<TDto> ExecuteAsync(TCreateDto dto, int createdBy,string languageCode);
}