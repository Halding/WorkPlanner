using Workplanner_Core.IServices;
using Workplanner_Core.Models.Utils;
using Workplanner_Domain.IRepositories;

namespace Workplanner_Domain.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository  authRepository)
    {
        _authRepository = authRepository;
    }
    
    public async Task<ServiceResponse<string>> Login(int employeeNumber, string password)
    {
        return await _authRepository.Login(employeeNumber, password);
    }
}