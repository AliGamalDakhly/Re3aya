using static _02_BusinessLogicLayer.DTOs.AiDTO.RagDTO;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IRagService
    {
        Task<List<DoctorResult>> GetRagResponseAsync(string question);
    }
}
