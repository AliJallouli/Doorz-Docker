
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.UseCases.Auth.UseCases.EmailConfirmation;

public class EmailConfirmedUseCase
{
    private readonly IUserRepository _userRepository;

    public EmailConfirmedUseCase( IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task ExecuteAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId)
                   ?? throw new BusinessException(ErrorCodes.UserNotFound, "userId");

        if (user.IsVerified)
        {
            throw new BusinessException(ErrorCodes.EmailAlreadyConfirmed, "userId");
        }

    }

}