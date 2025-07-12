using Railwaybackproject.DTO.Authentication;
using Railwaybackproject.Models;
using System.Threading.Tasks;

namespace Railwaybackproject.Services.abstractions;

public interface IUserservice
{
    Task<ApiResponse<string>> RegisterUser(RegisterRequest request);
    Task<ApiResponse<string>> LoginUser(LoginRequest request);
}
