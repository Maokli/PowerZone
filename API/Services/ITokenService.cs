using API.Models;

namespace API.Services
{
    public interface ITokenService
    {
         string CreateToken(Client client);
    }
}