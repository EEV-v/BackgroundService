using System.Threading.Tasks;
using BackgroundService.BusinessLogic.Contract.Models;

namespace BackgroundService.BusinessLogic.Contract.Services
{
    public interface IAuthorizationClient
    {
        Task<Token> GetToken();
    }
}
