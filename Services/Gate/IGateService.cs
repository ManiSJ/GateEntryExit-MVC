using GateEntryExit_MVC.Models.Gate;

namespace GateEntryExit_MVC.Services.Gate
{
    public interface IGateService
    {
        Task<GetAllGatesDto> GetAllAsync(int pageNumber = 1);
    }
}
