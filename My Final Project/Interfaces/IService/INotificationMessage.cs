using My_Final_Project.Models.DTOs;

namespace My_Final_Project.Interfaces.IService
{
    public interface INotificationMessage
    {
        Task SendWhatsappMessageAsync(WhatsappMessageSenderRequestModel model);
    }
}
