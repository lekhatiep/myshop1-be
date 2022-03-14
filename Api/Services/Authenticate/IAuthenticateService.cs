using Api.Dtos.Identity;
using Api.Dtos.TokenDto;
using System.Threading.Tasks;

namespace Api.Services.Authenticate
{
    public interface IAuthenticateService
    {
        Task<AuthReponseDto> Authenticate(LoginDto loginDto);
    }
}
