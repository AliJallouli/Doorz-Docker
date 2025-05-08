namespace Application.UseCases.Auth.DTOs;

public class RoleIdRequestDto
{
    public string RoleName { get; set; }= string.Empty;
    public string EntityTypeName { get; set; }= string.Empty;
}