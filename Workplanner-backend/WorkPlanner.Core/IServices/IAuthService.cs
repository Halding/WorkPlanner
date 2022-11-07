using Workplanner_Core.Models.Utils;

namespace Workplanner_Core.IServices;

public interface IAuthService
{
    Task<ServiceResponse<string>> Login(int employeeNumber, string password);
    
    
    
    
}