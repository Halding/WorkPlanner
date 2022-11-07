using Workplanner_Core.Models.Utils;

namespace Workplanner_Domain.IRepositories;

public interface IAuthRepository
{
    Task<ServiceResponse<string>> Login(int employeeNumber, string password);
    
    
}