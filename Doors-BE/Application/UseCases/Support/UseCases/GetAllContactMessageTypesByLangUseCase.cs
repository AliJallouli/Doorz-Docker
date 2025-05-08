using Application.UseCases.Support.DTOs;
using AutoMapper;
using Domain.Interfaces.Support;

namespace Application.UseCases.Support.UseCases;

public class GetAllContactMessageTypesByLangUseCase
{
    private readonly IContactMessageTypeRepository _contactMessageTypeRepository;
    private readonly IMapper _mapper;

    public GetAllContactMessageTypesByLangUseCase(
        IContactMessageTypeRepository contactMessageTypeRepository,
        IMapper mapper)
    {
        _contactMessageTypeRepository = contactMessageTypeRepository ?? throw new ArgumentNullException(nameof(contactMessageTypeRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<ContactMessageTypeDto>> ExecuteAsync(string languageCode)
    {
        var types = await _contactMessageTypeRepository.GetAllAsync(languageCode);
        var typesDto = _mapper.Map<List<ContactMessageTypeDto>>(types);
        return typesDto;
    }
}