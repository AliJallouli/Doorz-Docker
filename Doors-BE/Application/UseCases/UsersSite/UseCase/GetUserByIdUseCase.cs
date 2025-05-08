using Application.UseCases.UsersSite.DTOs;
using AutoMapper;
using Domain.Interfaces;

namespace Application.UseCases.UsersSite.UseCase;

public class GetUserByIdUseCase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserByIdUseCase(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserResponseDTO?> ExecuteAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user == null ? null : _mapper.Map<UserResponseDTO>(user);
    }
}