using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundService.BusinessLogic.Contract.Services
{
    public interface IApiService
    {
        Task CheckHealth();
    }
}
