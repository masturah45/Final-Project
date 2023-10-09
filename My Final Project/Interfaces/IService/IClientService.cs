using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface IClientService
    {
        Task<BaseResponse<ClientDto>> Create(CreateClientRequestModel model);
        Task<BaseResponse<ClientDto>> Update(Guid id, UpdateClientRequestModel model);
        Task<BaseResponse<ClientDto>> GetClient(Guid id);
        Task<BaseResponse<ClientDto>> Delete(Guid id);
        Task<IEnumerable<ClientDto>> GetAll();
        Task<List<UserDto>> GetAllClientByChat();
    }
}
