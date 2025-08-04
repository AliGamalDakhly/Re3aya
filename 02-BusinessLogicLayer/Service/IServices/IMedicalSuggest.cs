using _02_BusinessLogicLayer.DTOs.AiDTO;

namespace _02_BusinessLogicLayer.Service.IServices
{
    public interface IMedicalSuggest
    {
        Task<SuggestResponsesDTO> GetMedicalResponseAsync(string userInput);
    }
}
